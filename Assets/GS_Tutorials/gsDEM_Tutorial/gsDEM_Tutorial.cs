using GpuScript;

public class gsDEM_Tutorial : gsDEM_Tutorial_
{
  public override void onLoaded() { GenerateNodes(); base.onLoaded(); }
  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();
    if (runContinuously) for (int i = 0; i < 1; i++) { Gpu_zero_forces(); Gpu_calc_forces(); Gpu_check_collisions(); Gpu_move_nodes(); }
    var node = nodes[0]; node.position = MouseIntersectsPlane(f000, f00_); nodes[0] = node;
  }
  public override void GenerateNodes()
  {
    nodeN3 = uint3(nodeN, 1, 1);
    AddComputeBuffer(ref nodes, nameof(nodes), nodeN);
    AddComputeBuffer(ref forces, nameof(forces), 3 * nodeN);
    Gpu_BuildNodes();
  }
  public uint3 velocityConstraint(uint u) => uint3((u & 4) >> 2, (u & 2) >> 1, u & 1);
  public uint velocityConstraint(uint u, uint3 vI) => (u & ~8u) | (vI.x << 2) | (vI.y << 1) | vI.z;
  public uint validLink(uint u, uint lnkI, bool v) { return SetBit(u, lnkI + 3u, Is(v)); }
  public uint3 getLink_FCC(uint3 id, uint3 _nodeN, uint lnkI) { return TetraLinkID_FCC(id, _nodeN, lnkI); }
  public uint getLink_FCC(uint i, uint3 _nodeN, uint lnkI) { return TetraLinkI_FCC(i, _nodeN, lnkI); }
  public uint3 lnkI_to_lnk_cube27(uint lnkI) { return (lnkI / uint3(1, 3, 9)) % 3; }
  public uint3 lnkI_to_lnk_cube26(uint lnkI) { if (lnkI >= 13) lnkI++; return (lnkI / uint3(1, 3, 9)) % 3; }
  public uint lnk_to_lnkI_cube26(uint3 lnk) { uint lnkI = csum(lnk * uint3(1, 3, 9)); if (lnkI >= 13) lnkI--; return lnkI; }
  public override void BuildNodes_GS(uint3 id)
  {
    uint i = id.x;
    Node node = nodes[i];
    node.position = i * resolution * f010;
    uint u = 0;
    if (i < nodeN - 1) u = velocityConstraint(u, u111);
    for (uint lnkI = 0; lnkI < 1; lnkI++) u = validLink(u, lnkI, all(getLink_FCC(id, nodeN3, lnkI) < nodeN));
    node.u = u;
    float r = resolution / 2, r2 = r * r, r3 = r * r2, unitWeight_N_per_m3 = 26500, restitution = 0.01f, volume_m3 = 4 * r3 * PI / 3 * 1.35f,
      mass_N = unitWeight_N_per_m3 * volume_m3 / gravity_m_per_s2, modulus = 1e6f, stiffness = modulus * PI * r / 4, en = -ln(restitution),
      damping = 2 * en * sqrt(stiffness * mass_N / (PI * PI + en * en));
    node.mass = mass_N; node.stiff = stiffness; node.damp = damping;
    node.velocity = f000; node.position0 = node.position; node.velocity0 = node.velocity;
    nodes[i] = node;
  }
  public float3 force(uint nodeI) { uint i = nodeI * 3; return float3(forces[i], forces[i + 1], forces[i + 2]) / 1e3f; }
  public void force(uint nodeI, float3 f) { uint i = nodeI * 3; int3 v = roundi(f * 1e3f); forces[i] = v.x; forces[i + 1] = v.y; forces[i + 2] = v.z; }
  public override void zero_forces_GS(uint3 id)
  {
    uint i = id.x; Node node = nodes[i]; node.velocity0 = node.velocity; node.position0 = node.position; nodes[i] = node;
    force(i, f000);
  }
  public uint getLink_FCC(uint i, uint lnkI) { return TetraLinkI_FCC(i, nodeN3, lnkI); }
  public uint3 getLink_FCC(uint3 id, uint lnkI) { return TetraLinkID_FCC(id, nodeN3, lnkI); }
  public uint validLink_u_FCC(uint u, uint lnkI) { return GetBit(u, lnkI + 3); }
  public uint validLink_FCC(uint i, uint lnkI) { return validLink_u_FCC(nodes[i].u, lnkI); }
  public uint getLinkNodeI_FCC(uint dm_nodeI, uint lnkI) { return Is(validLink_FCC(dm_nodeI, lnkI)) ? getLink_FCC(dm_nodeI, lnkI) : uint_max; }
  public bool getLinkNode_FCC(uint dm_nodeI, uint lnkI, ref uint lnkNodeI, ref Node linkNode) { lnkNodeI = getLinkNodeI_FCC(dm_nodeI, lnkI); bool ok = lnkNodeI != uint_max; if (ok) linkNode = nodes[lnkNodeI]; return ok; }
  public bool getLinkNode_FCC(uint dm_nodeI, uint lnkI, ref Node linkNode) { uint lnkNodeI = uint_max; return getLinkNode_FCC(dm_nodeI, lnkI, ref lnkNodeI, ref linkNode); }
  public void calc_link_force(Node node, Node linkNode, float springLength, ref float3 node_force)
  {
    float3 n = linkNode.position0 - node.position;
    float dist = length(n); //dist = abs(r_ij)
    n /= dist; //n = r_ij
    float damp = (node.damp + linkNode.damp) / 2, springStiffness;
    springStiffness = (node.stiff + linkNode.stiff) / 2;
    float3 v_ij = node.velocity - linkNode.velocity0;

    node_force += n * (springStiffness * (dist - springLength) - dot(n, v_ij) * damp);

    //float3 tForce = -0.01f * springStiffness * (v_ij - dot(v_ij, n) * n);
    //node_force += tForce;
  }

  public void AddForce(uint nodeI, float3 node_force) { int3 iForce = roundi(node_force * 1e3f); if (any(iForce != i000)) { uint forceI = nodeI * 3; InterlockedAdd(forces, forceI + 0, iForce.x); InterlockedAdd(forces, forceI + 1, iForce.y); InterlockedAdd(forces, forceI + 2, iForce.z); } }
  public override void calc_forces_GS(uint3 id)
  {
    uint i = id.x;
    Node node = nodes[i];
    float3 node_force = f000;
    bool isGridNode = i < nodeN;
    Node linkNode = node;
    if (isGridNode)
      for (uint lnkI = 0; lnkI < 12; lnkI++)
        if (getLinkNode_FCC(i, lnkI, ref linkNode))
          calc_link_force(node, linkNode, resolution, ref node_force);
    node_force.y -= node.mass * gravity_m_per_s2;
    InterlockedMax(uints, UInts_maxForce, roundu(length(node_force) * 1e3f));
    AddForce(i, node_force);
  }

  public override void check_collisions_GS(uint3 id)
  {
    uint i = id.x;
    uint2 u = upperTriangularIndex(i, nodeN);
    if (extent(u) > 1 || u.x == nodeN - 2)
    {
      Node node0 = nodes[u.x], node1 = nodes[u.y];
      float dist = distance(node0.position, node1.position);
      if (dist < resolution)
      {
        float3 node_force = f000;
        calc_link_force(node0, node1, resolution, ref node_force);
        AddForce(u.x, node_force);
        AddForce(u.y, -node_force);
        InterlockedMax(uints, UInts_maxForce, roundu(length(node_force) * 1e3f));
      }
    }
  }

  public uint3 velocityConstraint_u_FCC(uint u) { return uint3(GetBit(u, 0), GetBit(u, 1u), GetBit(u, 2u)); }
  public override void move_nodes_GS(uint3 id)
  {
    uint i = id.x;
    Node node = nodes[i];
    float3 node_force = force(i);

    bool isGridNode = i < nodeN;
    float restitution = 0.01f, minMass = node.mass, minRestitution = restitution, maxVel = 10, maxStiffness = node.stiff, minDamping = minRestitution;
    float timeStep = sqrt(minMass) / maxStiffness / maxVel * minDamping * 30000;
    node.velocity = node.velocity0 + node_force * timeStep / node.mass;
    node.velocity *= 0.99f; //remove
    if (isGridNode) node.velocity *= velocityConstraint_u_FCC(node.u);
    node.position = node.position0 + node.velocity * timeStep;
    nodes[i] = node;
  }
  public override v2f vert_Nodes(uint i, uint j, v2f o)
  {
    Node n = nodes[i];
    float4 c = i == 0 ? YELLOW : i == nodeN - 1 ? RED : GREEN;
    return vert_BDraw_Sphere(n.position, resolution / 2, c, i, j, o);
  }
}
