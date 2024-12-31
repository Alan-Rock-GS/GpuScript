// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC, Update: 178


//#pragma use_dxc
#define _gs
#define gs defined(_gs)

#define public
#define protected
#define private
#define override

#define unchecked

#define ref inout
#define new
#define IEnumerator void
#define string uint
#define strings uint

#define maxByteN 2097152

#define ASCII_NUL 0x00
#define ASCII_SOH 0x01
#define ASCII_STX 0x02
#define ASCII_ETX 0x03
#define ASCII_EOT 0x04
#define ASCII_ENQ 0x05
#define ASCII_ACK 0x06
#define ASCII_BEL 0x07
#define ASCII_BS 0x08
#define ASCII_HT 0x09
#define ASCII_LF 0x0A
#define ASCII_VT 0x0B
#define ASCII_FF 0x0C
#define ASCII_CR 0x0D
#define ASCII_SO 0x0E
#define ASCII_SI 0x0F
#define ASCII_DLE 0x10
#define ASCII_DC1 0x11
#define ASCII_DC2 0x12
#define ASCII_DC3 0x13
#define ASCII_DC4 0x14
#define ASCII_NAK 0x15
#define ASCII_SYN 0x16
#define ASCII_ETB 0x17
#define ASCII_CAN 0x18
#define ASCII_EM 0x19
#define ASCII_SUB 0x1A
#define ASCII_ESC 0x1B
#define ASCII_FS 0x1C
#define ASCII_GS 0x1D
#define ASCII_RS 0x1E
#define ASCII_US 0x1F
#define ASCII_Space 0x20
#define ASCII_Exclamation 0x21
#define ASCII_Quote 0x22
#define ASCII_pound 0x23
#define ASCII_dollar 0x24
#define ASCII_percent 0x25
#define ASCII_ampersand 0x26
#define ASCII_Apostrophe 0x27
#define ASCII_OpenParenthesis 0x28
#define ASCII_ClosedParenthesis 0x29
#define ASCII_Asterisk 0x2A
#define ASCII_Plus 0x2B
#define ASCII_Comma 0x2C
#define ASCII_Dash 0x2D
#define ASCII_Period 0x2E
#define ASCII_Slash 0x2F
#define ASCII_0 0x30
#define ASCII_1 0x31
#define ASCII_2 0x32
#define ASCII_3 0x33
#define ASCII_4 0x34
#define ASCII_5 0x35
#define ASCII_6 0x36
#define ASCII_7 0x37
#define ASCII_8 0x38
#define ASCII_9 0x39
#define ASCII_Color 0x3A
#define ASCII_SemiColon 0x3B
#define ASCII_Less 0x3C
#define ASCII_Equal 0x3D
#define ASCII_Greater 0x3E
#define ASCII_Question 0x3F
#define ASCII_at 0x40
#define ASCII_A 0x41
#define ASCII_B 0x42
#define ASCII_C 0x43
#define ASCII_D 0x44
#define ASCII_E 0x45
#define ASCII_F 0x46
#define ASCII_G 0x47
#define ASCII_H 0x48
#define ASCII_I 0x49
#define ASCII_J 0x4A
#define ASCII_K 0x4B
#define ASCII_L 0x4C
#define ASCII_M 0x4D
#define ASCII_N 0x4E
#define ASCII_O 0x4F
#define ASCII_P 0x50
#define ASCII_Q 0x51
#define ASCII_R 0x52
#define ASCII_S 0x53
#define ASCII_T 0x54
#define ASCII_U 0x55
#define ASCII_V 0x56
#define ASCII_W 0x57
#define ASCII_X 0x58
#define ASCII_Y 0x59
#define ASCII_Z 0x5A
#define ASCII_LeftBracket 0x5B
#define ASCII_BackSlash 0x5C
#define ASCII_RightBracket 0x5D
#define ASCII_hat 0x5E
#define ASCII_Underscore 0x5F
#define ASCII_BackTick 0x60
#define ASCII_a 0x61
#define ASCII_b 0x62
#define ASCII_c 0x63
#define ASCII_d 0x64
#define ASCII_e 0x65
#define ASCII_f 0x66
#define ASCII_g 0x67
#define ASCII_h 0x68
#define ASCII_i 0x69
#define ASCII_j 0x6A
#define ASCII_k 0x6B
#define ASCII_l 0x6C
#define ASCII_m 0x6D
#define ASCII_n 0x6E
#define ASCII_o 0x6F
#define ASCII_p 0x70
#define ASCII_q 0x71
#define ASCII_r 0x72
#define ASCII_s 0x73
#define ASCII_t 0x74
#define ASCII_u 0x75
#define ASCII_v 0x76
#define ASCII_w 0x77
#define ASCII_x 0x78
#define ASCII_y 0x79
#define ASCII_z 0x7A
#define ASCII_LeftCurly 0x7B
#define ASCII_Vertical 0x7C
#define ASCII_RightCurly 0x7D
#define ASCII_Tilde 0x7E
#define ASCII_DEL 0x7F

#define max_uint 4294967295u
#define max_int 2147483647

#define uint_max 4294967295u
#define uint_min 0u
#define uint_mid 8388607u
#define uint_mid1 8388608u
#define int_max 2147483647
#define int_min -2147483648
#define uint24_max 16777215
#define uint24_mid 8388607
#define uint24_mid1 8388608
#define uint24 16777215
#define uint23 8388607
#define float_NegativeInfinity -3.4e38
#define float_PositiveInfinity  3.4e38
#define PI 3.14159265f
#define PIo2 1.570796327f
#define PIo4 0.785398163f
#define TwoPI 6.28318531f
#define EPS 1e-6f
#define Sqrt2 1.414213562f
#define Sqrt3 1.732050808f
#define Sqrt2o3 0.816496581f
#define Sqrt3o2 1.224744871f
#define Sqrt2PI 2.506628275f
#define rcpSqrt2PI 0.39894228f
#define LN2 0.693147181f

#define gravity_m_per_s2  9.80665f


#define fNegInf float_NegativeInfinity
#define fNegInf2 float2(fNegInf, fNegInf)
#define fNegInf3 float3(fNegInf, fNegInf, fNegInf)
#define fNegInf4 float4(fNegInf, fNegInf, fNegInf, fNegInf)
#define fPosInf float_PositiveInfinity
#define fPosInf2 float2(fPosInf, fPosInf)
#define fPosInf3 float3(fPosInf, fPosInf, fPosInf)
#define fPosInf4 float4(fPosInf, fPosInf, fPosInf, fPosInf)

#define initRange float2(float_PositiveInfinity, float_NegativeInfinity)
#define initRangei int2(int_max, int_min)
#define initRangeu uint2(uint_max, uint_min)

#define true 1
#define false 0
#define iTrue 1
#define iFalse 0
#define uTrue 1u
#define uFalse 0

#define findMSB(a) firstbithigh(a)
#define findLSB(a) firstbitlow(a)

#define const static

#define Color float4
#define Color32 uint

#define EMPTY float4(0,0,0,0)
#define BLACK float4(0,0,0,1)
#define DARK_GRAY float4(0.25f,0.25f,0.25f,1)
#define GRAY float4(0.5f,0.5f,0.5f,1)
#define LIGHT_GRAY float4(0.75f,0.75f,0.75f,1)
#define WHITE float4(1,1,1,1)
#define DARK_MAGENTA float4(0.5f,0.0f,0.5f,1)
#define MAGENTA float4(1,0,1,1)
#define LIGHT_MAGENTA float4(1,0.5f,1,1)
#define DARK_BLUE float4(0.0f,0.0f,0.5f,1)
#define BLUE float4(0,0,1,1)
#define LIGHT_BLUE float4(0.5f,0.5f,1,1)
#define DARK_CYAN float4(0.0f,0.5f,0.5f,1)
#define CYAN float4(0,1,1,1)
#define LIGHT_CYAN float4(0.5f,1,1,1)
#define DARK_GREEN float4(0.0f,0.5f,0.0f,1)
#define GREEN float4(0,1,0,1)
#define LIGHT_GREEN float4(0.5f,1,0.5f,1)
#define DARK_YELLOW float4(0.5f,0.5f,0.0f,1)
#define YELLOW float4(1,1,0,1)
#define LIGHT_YELLOW float4(1,1,0.5f,1)
#define DARK_RED float4(0.5f,0.0f,0.0f,1)
#define RED float4(1,0,0,1)
#define LIGHT_RED float4(1,0.5f,0.5f,1)
#define BROWN float4(0.75f,0,0,1)
#define ORANGE float4(1,0.37f,0.08f,1)

//#define GetSample(a, i) ((a[(i) >> 1] >> ((int)((i) % 2) << 4)) & 0x0000ffff)
//#define GetSamplef(a, i) ((GetSample(a, i) - 32768.0f) * 3.0517578125e-5f)
//
//#define SetSample(a, i, c) a[(i) >> 1] = (uint)((uint)(a[(i) >> 1] & (0xffff << (((int)((i) + 1) % 2) << 4))) | ((uint)((int)(c) << (((int)(i) % 2) << 4))))
//#define SetSamplef(a, i, c) (SetSample(a, i, (uint)(32767 * (clamp((c), -1, 1) + 1))))
//#define GetFloat2(a, i) (float2(GetSamplef(a, (i) << 1), GetSamplef(a, ((i) << 1) + 1)))
//#define SetFloat2(a, i, v) { SetSamplef(a, (i) << 1, (v).x); SetSamplef(a, ((i) << 1) + 1, (v).y); }

#define c32_f4(u) (float4((u) & 0xff, ((u) >> 8) & 0xff, ((u) >> 16) & 0xff, (u) >> 24) * 3.92156862745098e-3f)
#define c32_f3(u) (float3((u) & 0xff, ((u) >> 8) & 0xff, ((u) >> 16) & 0xff) * 3.92156862745098e-3f)

//#define c32_f4(u) (float4((u) & 0xff, ((u) >> 8) & 0xff, ((u) >> 16) & 0xff, (u) >> 24) * 1.99e-3f)
//#define c32_f3(u) (float3((u) & 0xff, ((u) >> 8) & 0xff, ((u) >> 16) & 0xff) * 1.99e-3f)


#define f4_c32(c) (dot((uint4)(round((c) * 255)), uint4(1, 256, 65536, 16777216)))
#define f3_c32(c) f4_c32(float4(c,1))

#define f4_u(c) (dot((uint4)(round((c) * 255)), uint4(1, 256, 65536, 16777216)))
#define u_f4(v) (float4((v) >> 24, ((v) >> 16) & 0xff, ((v) >> 8) & 0xff, (v) & 0xff) * 3.92156862745098e-3f)
#define c32_u(c) (c)
#define u_c32(u) (u)

#define f00 float2(0, 0)
#define f01 float2(0, 1)
#define f10 float2(1, 0)
#define f11 float2(1, 1)

#define f1_ float2(1, -1)
#define f_1 float2(-1, 1)
#define f__ float2(-1, -1)
#define f0_ float2(0, -1)
#define f_0 float2(-1, 0)

#define f_0 float2(-1, 0)
#define f_1 float2(-1, 1)
#define f0_ float2(0, -1)
#define f1_ float2(1, -1)
#define f__ float2(-1, -1)

#define f000 float3(0, 0, 0)
#define f001 float3(0, 0, 1)
#define f010 float3(0, 1, 0)
#define f011 float3(0, 1, 1)
#define f100 float3(1, 0, 0)
#define f101 float3(1, 0, 1)
#define f110 float3(1, 1, 0)
#define f111 float3(1, 1, 1)

#define f_00 float3(-1, 0, 0)
#define f_01 float3(-1, 0, 1)
#define f_10 float3(-1, 1, 0)
#define f_11 float3(-1, 1, 1)
#define f0_0 float3(0, -1, 0)
#define f0_1 float3(0, -1, 1)
#define f1_0 float3(1, -1, 0)
#define f1_1 float3(1, -1, 1)
#define f00_ float3(0, 0, -1)
#define f01_ float3(0, 1, -1)
#define f10_ float3(1, 0, -1)
#define f11_ float3(1, 1, -1)
#define f__0 float3(-1, -1, 0)
#define f__1 float3(-1, -1, 1)
#define f_0_ float3(-1, 0, -1)
#define f_1_ float3(-1, 1, -1)
#define f0__ float3(0, -1, -1)
#define f1__ float3(1, -1, -1)
#define f___ float3(-1, -1, -1)

#define f0000 float4(0, 0, 0, 0)
#define f0001 float4(0, 0, 0, 1)
#define f0010 float4(0, 0, 1, 0)
#define f0011 float4(0, 0, 1, 1)
#define f0100 float4(0, 1, 0, 0)
#define f0101 float4(0, 1, 0, 1)
#define f0110 float4(0, 1, 1, 0)
#define f0111 float4(0, 1, 1, 1)
#define f1000 float4(1, 0, 0, 0)
#define f1001 float4(1, 0, 0, 1)
#define f1010 float4(1, 0, 1, 0)
#define f1011 float4(1, 0, 1, 1)
#define f1100 float4(1, 1, 0, 0)
#define f1101 float4(1, 1, 0, 1)
#define f1110 float4(1, 1, 1, 0)
#define f1111 float4(1, 1, 1, 1)

#define f____ float4(-1, -1, -1, -1)
#define f___1 float4(-1, -1, -1, 1)
#define f__1_ float4(-1, -1, 1, -1)
#define f__11 float4(-1, -1, 1, 1)
#define f_1__ float4(-1, 1, -1, -1)
#define f_1_1 float4(-1, 1, -1, 1)
#define f_11_ float4(-1, 1, 1, -1)
#define f_111 float4(-1, 1, 1, 1)
#define f1___ float4(1, -1, -1, -1)
#define f1__1 float4(1, -1, -1, 1)
#define f1_1_ float4(1, -1, 1, -1)
#define f1_11 float4(1, -1, 1, 1)
#define f11__ float4(1, 1, -1, -1)
#define f11_1 float4(1, 1, -1, 1)
#define f111_ float4(1, 1, 1, -1)

#define f000_ float4(0, 0, 0, -1)
#define f00_0 float4(0, 0, -1, 0)
#define f00__ float4(0, 0, -1, -1)
#define f0_00 float4(0, -1, 0, 0)
#define f0_0_ float4(0, -1, 0, -1)
#define f0__0 float4(0, -1, -1, 0)
#define f0___ float4(0, -1, -1, -1)
#define f_000 float4(-1, 0, 0, 0)
#define f_00_ float4(-1, 0, 0, -1)
#define f_0_0 float4(-1, 0, -1, 0)
#define f_0__ float4(-1, 0, -1, -1)
#define f__00 float4(-1, -1, 0, 0)
#define f__0_ float4(-1, -1, 0, -1)
#define f___0 float4(-1, -1, -1, 0)

#define d00 double2(0, 0)
#define d01 double2(0, 1)
#define d10 double2(1, 0)
#define d11 double2(1, 1)

#define d1_ double2(1, -1)
#define d_1 double2(-1, 1)
#define d__ double2(-1, -1)
#define d0_ double2(0, -1)
#define d_0 double2(-1, 0)

#define d_0 double2(-1, 0)
#define d_1 double2(-1, 1)
#define d0_ double2(0, -1)
#define d1_ double2(1, -1)
#define d__ double2(-1, -1)

#define d000 double3(0, 0, 0)
#define d001 double3(0, 0, 1)
#define d010 double3(0, 1, 0)
#define d011 double3(0, 1, 1)
#define d100 double3(1, 0, 0)
#define d101 double3(1, 0, 1)
#define d110 double3(1, 1, 0)
#define d111 double3(1, 1, 1)

#define d_00 double3(-1, 0, 0)
#define d_01 double3(-1, 0, 1)
#define d_10 double3(-1, 1, 0)
#define d_11 double3(-1, 1, 1)
#define d0_0 double3(0, -1, 0)
#define d0_1 double3(0, -1, 1)
#define d1_0 double3(1, -1, 0)
#define d1_1 double3(1, -1, 1)
#define d00_ double3(0, 0, -1)
#define d01_ double3(0, 1, -1)
#define d10_ double3(1, 0, -1)
#define d11_ double3(1, 1, -1)
#define d__0 double3(-1, -1, 0)
#define d__1 double3(-1, -1, 1)
#define d_0_ double3(-1, 0, -1)
#define d_1_ double3(-1, 1, -1)
#define d0__ double3(0, -1, -1)
#define d1__ double3(1, -1, -1)
#define d___ double3(-1, -1, -1)

#define d0000 double4(0, 0, 0, 0)
#define d0001 double4(0, 0, 0, 1)
#define d0010 double4(0, 0, 1, 0)
#define d0011 double4(0, 0, 1, 1)
#define d0100 double4(0, 1, 0, 0)
#define d0101 double4(0, 1, 0, 1)
#define d0110 double4(0, 1, 1, 0)
#define d0111 double4(0, 1, 1, 1)
#define d1000 double4(1, 0, 0, 0)
#define d1001 double4(1, 0, 0, 1)
#define d1010 double4(1, 0, 1, 0)
#define d1011 double4(1, 0, 1, 1)
#define d1100 double4(1, 1, 0, 0)
#define d1101 double4(1, 1, 0, 1)
#define d1110 double4(1, 1, 1, 0)
#define d1111 double4(1, 1, 1, 1)
#define d___1 double4(-1, -1, -1, 1)

#define i00 int2(0, 0)
#define i01 int2(0, 1)
#define i10 int2(1, 0)
#define i11 int2(1, 1)
#define i_0 int2(-1, 0)
#define i_1 int2(-1, 1)
#define i0_ int2(0, -1)
#define i1_ int2(1, -1)
#define i__ int2(-1, -1)

#define i000 int3(0, 0, 0)
#define i001 int3(0, 0, 1)
#define i010 int3(0, 1, 0)
#define i011 int3(0, 1, 1)
#define i100 int3(1, 0, 0)
#define i101 int3(1, 0, 1)
#define i110 int3(1, 1, 0)
#define i111 int3(1, 1, 1)
#define i_00 int3(-1, 0, 0)
#define i_01 int3(-1, 0, 1)
#define i_10 int3(-1, 1, 0)
#define i_11 int3(-1, 1, 1)
#define i0_0 int3(0, -1, 0)
#define i0_1 int3(0, -1, 1)
#define i1_0 int3(1, -1, 0)
#define i1_1 int3(1, -1, 1)
#define i00_ int3(0, 0, -1)
#define i01_ int3(0, 1, -1)
#define i10_ int3(1, 0, -1)
#define i11_ int3(1, 1, -1)
#define i__0 int3(-1, -1, 0)
#define i__1 int3(-1, -1, 1)
#define i_0_ int3(-1, 0, -1)
#define i_1_ int3(-1, 1, -1)
#define i0__ int3(0, -1, -1)
#define i1__ int3(1, -1, -1)
#define i___ int3(-1, -1, -1)

#define i0000 int4(0, 0, 0, 0)
#define i0001 int4(0, 0, 0, 1)
#define i0010 int4(0, 0, 1, 0)
#define i0011 int4(0, 0, 1, 1)
#define i0100 int4(0, 1, 0, 0)
#define i0101 int4(0, 1, 0, 1)
#define i0110 int4(0, 1, 1, 0)
#define i0111 int4(0, 1, 1, 1)
#define i1000 int4(1, 0, 0, 0)
#define i1001 int4(1, 0, 0, 1)
#define i1010 int4(1, 0, 1, 0)
#define i1011 int4(1, 0, 1, 1)
#define i1100 int4(1, 1, 0, 0)
#define i1101 int4(1, 1, 0, 1)
#define i1110 int4(1, 1, 1, 0)
#define i1111 int4(1, 1, 1, 1)

#define u00 uint2(0, 0)
#define u01 uint2(0, 1)
#define u10 uint2(1, 0)
#define u11 uint2(1, 1)
#define u000 uint3(0, 0, 0)
#define u001 uint3(0, 0, 1)
#define u010 uint3(0, 1, 0)
#define u011 uint3(0, 1, 1)
#define u100 uint3(1, 0, 0)
#define u101 uint3(1, 0, 1)
#define u110 uint3(1, 1, 0)
#define u111 uint3(1, 1, 1)
#define u0000 uint4(0, 0, 0, 0)
#define u0001 uint4(0, 0, 0, 1)
#define u0010 uint4(0, 0, 1, 0)
#define u0011 uint4(0, 0, 1, 1)
#define u0100 uint4(0, 1, 0, 0)
#define u0101 uint4(0, 1, 0, 1)
#define u0110 uint4(0, 1, 1, 0)
#define u0111 uint4(0, 1, 1, 1)
#define u1000 uint4(1, 0, 0, 0)
#define u1001 uint4(1, 0, 0, 1)
#define u1010 uint4(1, 0, 1, 0)
#define u1011 uint4(1, 0, 1, 1)
#define u1100 uint4(1, 1, 0, 0)
#define u1101 uint4(1, 1, 0, 1)
#define u1110 uint4(1, 1, 1, 0)
#define u1111 uint4(1, 1, 1, 1)

#define b00 bool2(0, 0)
#define b01 bool2(0, 1)
#define b10 bool2(1, 0)
#define b11 bool2(1, 1)
#define b000 bool3(0, 0, 0)
#define b001 bool3(0, 0, 1)
#define b010 bool3(0, 1, 0)
#define b011 bool3(0, 1, 1)
#define b100 bool3(1, 0, 0)
#define b101 bool3(1, 0, 1)
#define b110 bool3(1, 1, 0)
#define b111 bool3(1, 1, 1)
#define b0000 bool4(0, 0, 0, 0)
#define b0001 bool4(0, 0, 0, 1)
#define b0010 bool4(0, 0, 1, 0)
#define b0011 bool4(0, 0, 1, 1)
#define b0100 bool4(0, 1, 0, 0)
#define b0101 bool4(0, 1, 0, 1)
#define b0110 bool4(0, 1, 1, 0)
#define b0111 bool4(0, 1, 1, 1)
#define b1000 bool4(1, 0, 0, 0)
#define b1001 bool4(1, 0, 0, 1)
#define b1010 bool4(1, 0, 1, 0)
#define b1011 bool4(1, 0, 1, 1)
#define b1100 bool4(1, 1, 0, 0)
#define b1101 bool4(1, 1, 0, 1)
#define b1110 bool4(1, 1, 1, 0)
#define b1111 bool4(1, 1, 1, 1)

#define fTetra float3(1, 0.816496581f, 0.866025404f)


#define paletteColor(_PaletteTex, v) tex2Dlod(_PaletteTex, f1000 * clamp(v, 0.01f, 0.99f))
//#define paletteColor(_PaletteTex, v) tex2D(_PaletteTex, f10 * clamp(v, 0.01f, 0.99f))


#define Matrix4x4 float4x4

#define long int64_t
#define ulong uint64_t

//struct PGA4 { float4 v0, v1, v2, v3; };

//#define pga4_zero pga4(0, 0)
//#define pga4_e0 pga4(1, 1)
//#define pga4_e1 pga4(1, 2)
//#define pga4_e2 pga4(1, 3)
//#define pga4_e3 pga4(1, 4)
//#define pga4_e01 Wedge4(pga4_e0, pga4_e1) 
//#define pga4_e02 Wedge4(pga4_e0, pga4_e2) 
//#define pga4_e03 Wedge4(pga4_e0, pga4_e3) 
//#define pga4_e12 Wedge4(pga4_e1, pga4_e2) 
//#define pga4_e31 Wedge4(pga4_e3, pga4_e1) 
//#define pga4_e23 Wedge4(pga4_e2, pga4_e3)
//#define pga4_e123 Wedge4(pga4_e1, pga4_e2, pga4_e3) 
//#define pga4_e032 Wedge4(pga4_e0, pga4_e3, pga4_e2) 
//#define pga4_e013 Wedge4(pga4_e0, pga4_e1, pga4_e3) 
//#define pga4_e021 Wedge4(pga4_e0, pga4_e2, pga4_e1)

struct PGA3 { float s; float4 v; float3 e, E; float4 t; float p; };
#define pga3_zero pga3(0, 0)
#define pga3_e0 pga3(1, 1)
#define pga3_e1 pga3(1, 2)
#define pga3_e2 pga3(1, 3)
#define pga3_e3 pga3(1, 4)
#define pga3_e01 Wedge(pga3_e0, pga3_e1) 
#define pga3_e02 Wedge(pga3_e0, pga3_e2) 
#define pga3_e03 Wedge(pga3_e0, pga3_e3) 
#define pga3_e12 Wedge(pga3_e1, pga3_e2) 
#define pga3_e31 Wedge(pga3_e3, pga3_e1) 
#define pga3_e23 Wedge(pga3_e2, pga3_e3)
#define pga3_e123 Wedge(pga3_e1, pga3_e2, pga3_e3) 
#define pga3_e032 Wedge(pga3_e0, pga3_e3, pga3_e2) 
#define pga3_e013 Wedge(pga3_e0, pga3_e1, pga3_e3) 
#define pga3_e021 Wedge(pga3_e0, pga3_e2, pga3_e1)

struct PGA2 { float s; float3 e, E; float p; };
#define pga2_zero pga2(0, 0)
#define pga2_e0 pga2(1, 1)
#define pga2_e1 pga2(1, 2)
#define pga2_e2 pga2(1, 3)
#define pga2_e01 Wedge(pga2_e0, pga2_e1) 
#define pga2_e20 Wedge(pga2_e2, pga2_e0) 
#define pga2_e12 Wedge(pga2_e1, pga2_e2) 
#define pga2_e012 Wedge(pga2_e0, pga2_e1, pga2_e2)


#include "../../GS/HLSL/GS.cs"

