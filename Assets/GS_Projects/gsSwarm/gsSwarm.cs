using GpuScript;
using UnityEngine;

public class gsSwarm : gsSwarm_
{
  public override BDraw_TextInfo BDraw_textInfo(uint i) => BDraw_textInfos[i % BDraw_textN];
  public override void BDraw_textInfo(uint i, BDraw_TextInfo t) => BDraw_textInfos[i % BDraw_textN] = t;
  void AddText(string t, float textHeight) => BDraw_AddText(t, f000, f100, f011, BLUE, f0000, textHeight, textQuadType, BDraw_TextAlignment.CenterCenter);
  public void BuildTexts()
  {
    if (gSwarm == null) return;
    BDraw_ClearTexts();
    float textHeight = 0.03f;
    switch (particleShape)
    {
      case ParticleShape.Text: text.Split("|").For(t => AddText(t, textHeight)); BDraw_BuildTexts_Default(); break;
      case ParticleShape.Numbers: For(particleN, i => AddText(i.ToString(), textHeight)); BDraw_BuildTexts(); break;
      case ParticleShape.Letters:
        For(26, i => AddText($"{(char)(i + 'A')}", textHeight));
        For(26, i => AddText($"{(char)(i + 'a')}", textHeight));
        BDraw_BuildTexts_Default();
        StartCoroutine(BDraw_BuildTexts_Coroutine());
        break;
    }
  }
  public override void InitBuffers0_GS() { Rand_N = 10000; base.InitBuffers0_GS(); }
  public override void InitBuffers1_GS() { base.InitBuffers1_GS(); Rand_Init(Rand_N, 7); initParticles(); BuildTexts(); }
  public override void LateUpdate0_GS()
  {
    base.LateUpdate0_GS();
    if (gSwarm != null && !mouseInUI)
    {
      float3 p = MouseIntersectsPlane(f000, f00_);
      if (IsNotNegInf(p)) mousePosition = p;
      if (MouseRightButton) initParticles();
      deltaTime = Time.deltaTime;
      Gpu_moveParticles();
      initialSpeed = 0;
    }
  }
  void initParticles() { for (randomArrayI = 0; randomArrayI < particleN; randomArrayI += Rand_N) Gpu_initParticles(); }
  public Particle GetParticle(uint i) => particles[i];
  public float3 GetDirection(Particle p) => 0.01f * normalize(p.velocity);
  public float4 GetColor(Particle p) => palette(length(p.velocity) / 2);
  public override void Restart() => initParticles();
  public override void initParticles_GS(uint3 id)
  {
    uint i = id.x + randomArrayI;
    if (i < particleN)
    {
      Particle p = particles[i];
      p.position = particleDistribution == ParticleDistribution.inSphere ? Rand_inSphere(id.x) : particleDistribution == ParticleDistribution.onSphere ? Rand_onSphere(id.x) : particleDistribution == ParticleDistribution.inCube ? Rand_inCube(id.x) : particleDistribution == ParticleDistribution.inCircle ? Rand_inCircle(id.x) : particleDistribution == ParticleDistribution.onCircle ? Rand_onCircle(id.x) : f100 * Rand_Float(id.x);
      p.velocity = p.position * initialSpeed;
      p.position *= initialSpread;
      particles[i] = p;
    }
  }
  public override void moveParticles_GS(uint3 id)
  {
    uint i = id.x;
    Particle p = particles[i];
    p.velocity += mouseStrength * normalize(mousePosition - p.position) * deltaTime / max(10.0f, distance(mousePosition, p.position));
    p.position += p.velocity * deltaTime;
    particles[i] = p;
  }
  public override v2f vert_BDraw_Text(BDraw_TextInfo t, uint i, uint j, v2f o)
  {
    Particle p = particles[i];
    t.p = p.position;
    if (textQuadType == BDraw_Text_QuadType.Arrow) { float3 d = GetDirection(p); t.p0 = p.position - d; t.p1 = p.position + d; }
    t.quadType = (uint)textQuadType;
    t.color = GetColor(p);
    t.height = textHeight;
    return base.vert_BDraw_Text(t, i, j, o);
  }
  public override v2f vert_Spheres(uint i, uint j, v2f o) { Particle p = GetParticle(i); return vert_BDraw_Sphere(p.position, 0.01f, GetColor(p), i, j, o); }
  public override v2f vert_Lines(uint i, uint j, v2f o) { Particle p = GetParticle(i); float3 d = GetDirection(p); return vert_BDraw_Line(p.position - d, p.position + d, length(d) / 10, GetColor(p), i, j, o); ; }
  public override v2f vert_Arrows(uint i, uint j, v2f o) { Particle p = GetParticle(i); float3 d = GetDirection(p); return vert_BDraw_Arrow(p.position - d, p.position + d, length(d) / 10, GetColor(p), i, j, o); }
  public override v2f vert_Points(uint i, uint j, v2f o) { Particle p = GetParticle(i); return vert_BDraw_Point(p.position, GetColor(p), i, o); }
}