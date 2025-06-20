using GpuScript;
using System.Linq;
using UnityEngine;

public class gsRayTrace : gsRayTrace_
{
	public Texture2D Skybox;
	Light directionalLightObj = null;
	public override void InitBuffers0_GS()
	{
		depthColorN = product(screenSize = ScreenSize()) * 2;
		if (skyboxSize.x != Skybox.width) { skyboxSize = uint2(Skybox.width, Skybox.height); AddComputeBufferData(ref skyboxBuffer, nameof(skyboxBuffer), Skybox.GetPixels32()); }
		boxN = 1;
		useGroundPlane = true;
		shapeN = sphereN + boxN + Is(useGroundPlane);
		directionalLightObj ??= FindGameObject("Directional Light").GetComponent<Light>();
		if (directionalLightObj != null) directionalLight = float4(directionalLightObj.transform.forward, directionalLightObj.intensity);
	}
	public override void TraceRays()
	{
		var cam = mainCam;
		screenSize = ScreenSize();
		(maxDist, camToWorld, camInvProjection, isOrtho, orthoSize) = (cam.farClipPlane, cam.cameraToWorldMatrix, cam.projectionMatrix.inverse, cam.orthographic, cam.orthographicSize);
    float time = For(1).Select(a => Secs(() => Gpu_traceRay())).Min();
    float cpuTime = 69.6f;// Secs(() => Cpu_traceRay());
		status = $"frame = {time * 1e6f:#,##0} μs, CPU = {cpuTime} secs, {cpuTime / time:#,##0} times faster";
	}
	public override void Views_Lib_CamViews_Lib_SaveView(int row)
	{
		var view = Views_Lib_CamViews_Lib[row];
		view.view_sphereRadius = sphereRadius; view.view_sphereN = sphereN; view.view_objectColor = objectColor; view.view_diffuseLight = diffuseLight; view.view_shadows = shadows;
		Views_Lib_CamViews_Lib[row] = view;
		base.Views_Lib_CamViews_Lib_SaveView(row);
	}
	public override void Views_Lib_CamViews_Lib_LoadView(int row)
	{
		var view = Views_Lib_CamViews_Lib[row];
		(sphereRadius, sphereN, objectColor, diffuseLight, shadows) = (view.view_sphereRadius, view.view_sphereN, view.view_objectColor, view.view_diffuseLight, view.view_shadows);
		base.Views_Lib_CamViews_Lib_LoadView(row);
		InitBuffers();
		retrace = true;
	}
	public override void LateUpdate1_GS()
	{
		base.LateUpdate1_GS();
		if (!isInitBuffers) return;
		if (mainCam.transform.hasChanged) { retrace = true; mainCam.transform.hasChanged = false; }
		if (retrace) TraceRays();
		retrace = ValuesChanged = gChanged = false;
	}
	public TSphere GetSphere(float radius, float3 position, float diffuse, float3 color) { TSphere sphere; sphere.radius = radius; sphere.position = position; sphere.diffuse = diffuse; sphere.color = color; return sphere; }
	public TSphere GetSphere(uint i)
	{
		uint n = roundu(sqrt(sphereN)); uint2 id = i_to_id(i, u11 * n); float2 d = f11 * 3, p1 = n / 2u * d, p0 = -p1, p = p0 + id * d;
		return GetSphere(sphereRadius, float3(p.x, sphereRadius, p.y), diffuseLight, objectColor);
	}
	public TBox GetBox(float3 mx, float3 mn, float diffuse, float3 color) { TBox box; box.mx = mx; box.mn = mn; box.diffuse = diffuse; box.color = color; return box; }
	public TBox GetBox(uint i) => GetBox(float3(4, 6, 4), float3(-4, 0, -4), diffuseLight, objectColor);
	public TTriangle GetTriangle(float3 a, float3 b, float3 c, float diffuse, float3 color) { TTriangle tri; tri.a = a; tri.b = b; tri.c = c; tri.diffuse = diffuse; tri.color = color; return tri; }
	public TTriangle GetTriangle(uint i) { float3 a = f111 + 3, b = a + f100 * 2, c = (a + b) / 2 + f010 * 2; return GetTriangle(a, b, c, diffuseLight, objectColor); }
	public TRay CreateRay(float3 origin, float3 direction) { TRay ray; ray.origin = origin; ray.direction = direction; ray.energy = f111; ray.specular = f000; ray.diffuse = 0; return ray; }
	public TRayHit CreateRayHit() { TRayHit hit; hit.position = f000; hit.distance = fPosInf; hit.normal = hit.specular = f000; hit.diffuse = 0; return hit; }
	public TRayHit IntersectGroundPlane(TRay ray, TRayHit bestHit)// Calculate distance along the ray where the ground plane is intersected
	{
		float t = ray.direction.y == 0 ? fPosInf : -ray.origin.y / ray.direction.y;
		if (t > 0 && t < bestHit.distance)
		{
			bestHit.distance = t; bestHit.position = t * ray.direction + ray.origin; bestHit.normal = f010;
			bestHit.specular = g.objectColor; bestHit.diffuse = diffuseLight;
		}
		return bestHit;
	}
	public TRayHit IntersectSphere(TRay ray, TRayHit bestHit, uint sphereI)
	{
		TSphere sphere = GetSphere(sphereI);
		float3 p = sphere.position, d = ray.origin - p;
		float b = -dot(ray.direction, d), h = b * b - dot(d, d) + sqr(sphere.radius);
		if (h > 0)
		{
			h = sqrt(h); h = b + (b > h ? -h : h);
			if (h > 0 && h < bestHit.distance)
			{
				bestHit.distance = h; bestHit.position = h * ray.direction + ray.origin; bestHit.normal = normalize(bestHit.position - p);
				bestHit.specular = sphere.color;
				bestHit.diffuse = sphere.diffuse;
			}
		}
		return bestHit;
	}
	public TRayHit IntersectBox(TRay ray, TRayHit bestHit, uint i)
	{
		TBox box = GetBox(i);
		float3 t0 = (box.mn - ray.origin) / ray.direction, t1 = (box.mx - ray.origin) / ray.direction;
		float dstA = cmax(min(t0, t1)), dstB = cmin(max(t0, t1)), dstToBox = max(0, dstA), dstInsideBox = max(0, dstB - dstToBox), eps = 0.00001f;
		if (dstA >= 0 && dstA <= dstB) //ray intersects box from outside
		{
			if (dstToBox < bestHit.distance)
			{
				bestHit.distance = dstToBox;
				float3 p = bestHit.position = dstToBox * ray.direction + ray.origin;
				bestHit.normal = (abs(p - box.mx) < eps) - (abs(p - box.mn) < eps); bestHit.specular = box.color; bestHit.diffuse = box.diffuse;
			}
		}
		else if (dstA < 0 && dstB > 0) //ray intersects box from inside
		{
			if (dstToBox < bestHit.distance)
			{
				float3 p = dstToBox * ray.direction + ray.origin, normal = (abs(p - box.mx) < eps) - (abs(p - box.mn) < eps);
				if (all(normal != f010))
				{
					bestHit.distance = dstToBox; bestHit.position = p; bestHit.normal = -normal;
					bestHit.specular = box.color; bestHit.diffuse = box.diffuse;
				}//remove box top so reflections can see sky
			}
		}
		return bestHit;
	}
	public TRayHit Trace(TRay ray)
	{
		TRayHit bestHit = CreateRayHit();
		uint i;
		for (i = 0; i < g.sphereN; i++) bestHit = IntersectSphere(ray, bestHit, i);
		for (i = 0; i < boxN; i++) bestHit = IntersectBox(ray, bestHit, i);
		if (useGroundPlane) bestHit = IntersectGroundPlane(ray, bestHit);
		return bestHit;
	}
	public float3 Shade(ref TRay ray, TRayHit hit)
	{
		float3 color = f000;
		if (hit.distance < fPosInf)
		{
			float3 specular = hit.specular, albedo = specular * 0.8f;// 1.0f, 0.78f, 0.34f Gold color
			float hit_diffuse = hit.diffuse;
			ray.origin = hit.position + hit.normal * 0.001f; // Reflect the ray and multiply energy with specular reflection
			ray.direction = reflect(ray.direction, hit.normal);
			ray.energy *= specular;
			TRay shadowRay = CreateRay(hit.position + hit.normal * 0.001f, -directionalLight.xyz);
			TRayHit shadowHit = Trace(shadowRay);
			ray.energy *= (1 - hit_diffuse);
			if (shadowHit.distance != fPosInf) { albedo *= 1 - shadows; ray.energy *= 1 - shadows; }
			color = -dot(hit.normal, g.directionalLight.xyz) * directionalLight.w * hit_diffuse * albedo;
		}
		else
		{
			ray.energy = f000;
			float phi = atan2(ray.direction.x, -ray.direction.z) * 0.5f, theta = acos(ray.direction.y);
			float2 p = float2(phi, theta) * -rcp(PI) + 1;
			int2 pixId = floori(p * skyboxSize);
			uint pixI = id_to_i(pixId, skyboxSize);
			color = c32_f4(skyboxBuffer[pixI]).xyz;
		}
		return color;
	}
	public TRay CreateCameraRay(float2 uv) => CreateRay(mul(camToWorld, isOrtho ? float4(orthoSize * uv * float2(raspect(screenSize), 1), 0, 1) : f0001).xyz, normalize(mul(camToWorld, float4(mul(camInvProjection, float4(uv, 0, 1)).xyz, 0)).xyz));
	public uint pixDepth_u(uint2 id) => depthColor[id_to_i(id, g.screenSize)];
	public float pixDepth_f(uint2 id) => pixDepth_u(id) / (float)uint_max * (g.maxDist - 2);  // - 2: get rid of annoying circle in distance
	public uint pix_u(uint2 id) => depthColor[id_to_i(id, g.screenSize) + product(g.screenSize)];
	public Color32 pix_c32(uint2 id) => u_c32(pix_u(id));
	public void pixDepth_u(uint2 id, uint d) => depthColor[id_to_i(id, g.screenSize)] = d;
	public void pixDepth_f(uint2 id, float d) => pixDepth_u(id, (uint)(d / g.maxDist * uint_max));
	public void pix_u(uint2 id, uint c) => depthColor[id_to_i(id, g.screenSize) + product(g.screenSize)] = c;
	public void pix_c32(uint2 id, Color32 c) => pix_u(id, c32_u(c));
	public void pix_f3(uint2 id, float3 c) => pix_c32(id, f4_c32(float4(c, 1)));

	public override void traceRay_GS(uint3 id)
	{
		float2 uv = (id.xy + 0.5f) / screenSize * 2 - 1;
		TRay ray = CreateCameraRay(uv);
		float3 pixColor = f000;
		float minDist = maxDist;
		for (uint traceI = 0; traceI < 8; traceI++)
		{
			TRayHit hit = Trace(ray);
			if (traceI == 0) minDist = hit.distance;
			pixColor += ray.energy * Shade(ref ray, hit);
			if (all(ray.energy == 0)) break;
		}
		pixColor = max(pixColor, objectColor * 0.1f);
		pix_f3(id.xy, pixColor);
		pixDepth_f(id.xy, minDist);
	}
	public override v2f vert_DrawScreen(uint i, uint j, v2f o)
	{
		uint2 id = i_to_id(i, screenSize);
		float2 uv = (id + f11 * 0.5f) / screenSize * 2 - 1;
		TRay ray = CreateCameraRay(uv);
		o = vert_BDraw_Point(ray.origin + pixDepth_f(id) * ray.direction, c32_f4(pix_c32(id)), i, o);
		return o;
	}
}