using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using INT = System.Int32;
using UINT = System.UInt32;
using DWORD = System.UInt32;

namespace _3dprobe.Bridge
{
	public static class Direct3D9
	{
		public static uint D3D_SDK_VERSION = 32;

		[DllImport("d3d9.dll", CallingConvention = CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public static extern IDirect3D9 Direct3DCreate9(uint SDKVersion);

		[DllImport("d3d9.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int Direct3DCreate9Ex(uint SDKVersion,
			[MarshalAs(UnmanagedType.Interface)] out IDirect3D9Ex d3d9ex);

		public const uint D3DERR_INVALIDDEVICE = 0x8876086b;

		public const uint D3DCAPS_OVERLAY = 0x00000800;
		public const uint D3DCAPS_READ_SCANLINE = 0x00020000;

		public const uint D3DCAPS2_FULLSCREENGAMMA = 0x00020000;
		public const uint D3DCAPS2_CANCALIBRATEGAMMA = 0x00100000;
		public const uint D3DCAPS2_RESERVED = 0x02000000;
		public const uint D3DCAPS2_CANMANAGERESOURCE = 0x10000000;
		public const uint D3DCAPS2_DYNAMICTEXTURES = 0x20000000;
		public const uint D3DCAPS2_CANAUTOGENMIPMAP = 0x40000000;
		public const uint D3DCAPS2_CANSHARERESOURCE = 0x80000000;

		public const uint D3DCAPS3_ALPHA_FULLSCREEN_FLIP_OR_DISCARD = 0x00000020;
		public const uint D3DCAPS3_LINEAR_TO_SRGB_PRESENTATION = 0x00000080;
		public const uint D3DCAPS3_COPY_TO_VIDMEM = 0x00000100;
		public const uint D3DCAPS3_COPY_TO_SYSTEMMEM = 0x00000200;
		public const uint D3DCAPS3_DXVAHD = 0x00000400;
		public const uint D3DCAPS3_DXVAHD_LIMITED = 0x00000800;

		public const uint D3DPRESENT_INTERVAL_DEFAULT = 0x00000000;
		public const uint D3DPRESENT_INTERVAL_ONE = 0x00000001;
		public const uint D3DPRESENT_INTERVAL_TWO = 0x00000002;
		public const uint D3DPRESENT_INTERVAL_THREE = 0x00000004;
		public const uint D3DPRESENT_INTERVAL_FOUR = 0x00000008;
		public const uint D3DPRESENT_INTERVAL_IMMEDIATE = 0x80000000;

		public const uint D3DDEVCAPS_EXECUTESYSTEMMEMORY = 0x00000010;
		public const uint D3DDEVCAPS_EXECUTEVIDEOMEMORY = 0x00000020;
		public const uint D3DDEVCAPS_TLVERTEXSYSTEMMEMORY = 0x00000040;
		public const uint D3DDEVCAPS_TLVERTEXVIDEOMEMORY = 0x00000080;
		public const uint D3DDEVCAPS_TEXTURESYSTEMMEMORY = 0x00000100;
		public const uint D3DDEVCAPS_TEXTUREVIDEOMEMORY = 0x00000200;
		public const uint D3DDEVCAPS_DRAWPRIMTLVERTEX = 0x00000400;
		public const uint D3DDEVCAPS_CANRENDERAFTERFLIP = 0x00000800;
		public const uint D3DDEVCAPS_TEXTURENONLOCALVIDMEM = 0x00001000;
		public const uint D3DDEVCAPS_DRAWPRIMITIVES2 = 0x00002000;
		public const uint D3DDEVCAPS_SEPARATETEXTUREMEMORIES = 0x00004000;
		public const uint D3DDEVCAPS_DRAWPRIMITIVES2EX = 0x00008000;
		public const uint D3DDEVCAPS_HWTRANSFORMANDLIGHT = 0x00010000;
		public const uint D3DDEVCAPS_CANBLTSYSTONONLOCAL = 0x00020000;
		public const uint D3DDEVCAPS_HWRASTERIZATION = 0x00080000;
		public const uint D3DDEVCAPS_PUREDEVICE = 0x00100000;
		public const uint D3DDEVCAPS_QUINTICRTPATCHES = 0x00200000;
		public const uint D3DDEVCAPS_RTPATCHES = 0x00400000;
		public const uint D3DDEVCAPS_RTPATCHHANDLEZERO = 0x00800000;
		public const uint D3DDEVCAPS_NPATCHES = 0x01000000;

		public const uint D3DDEVCAPS2_STREAMOFFSET = 0x00000001;
		public const uint D3DDEVCAPS2_DMAPNPATCH = 0x00000002;
		public const uint D3DDEVCAPS2_ADAPTIVETESSRTPATCH = 0x00000004;
		public const uint D3DDEVCAPS2_ADAPTIVETESSNPATCH = 0x00000008;
		public const uint D3DDEVCAPS2_CAN_STRETCHRECT_FROM_TEXTURES = 0x00000010;
		public const uint D3DDEVCAPS2_PRESAMPLEDDMAPNPATCH = 0x00000020;
		public const uint D3DDEVCAPS2_VERTEXELEMENTSCANSHARESTREAMOFFSET = 0x00000040;

		public const uint D3DPMISCCAPS_MASKZ = 0x00000002;
		public const uint D3DPMISCCAPS_CULLNONE = 0x00000010;
		public const uint D3DPMISCCAPS_CULLCW = 0x00000020;
		public const uint D3DPMISCCAPS_CULLCCW = 0x00000040;
		public const uint D3DPMISCCAPS_COLORWRITEENABLE = 0x00000080;
		public const uint D3DPMISCCAPS_CLIPPLANESCALEDPOINTS = 0x00000100;
		public const uint D3DPMISCCAPS_CLIPTLVERTS = 0x00000200;
		public const uint D3DPMISCCAPS_TSSARGTEMP = 0x00000400;
		public const uint D3DPMISCCAPS_BLENDOP = 0x00000800;
		public const uint D3DPMISCCAPS_NULLREFERENCE = 0x00001000;
		public const uint D3DPMISCCAPS_INDEPENDENTWRITEMASKS = 0x00004000;
		public const uint D3DPMISCCAPS_PERSTAGECONSTANT = 0x00008000;
		public const uint D3DPMISCCAPS_FOGANDSPECULARALPHA = 0x00010000;
		public const uint D3DPMISCCAPS_SEPARATEALPHABLEND = 0x00020000;
		public const uint D3DPMISCCAPS_MRTINDEPENDENTBITDEPTHS = 0x00040000;
		public const uint D3DPMISCCAPS_MRTPOSTPIXELSHADERBLENDING = 0x00080000;
		public const uint D3DPMISCCAPS_FOGVERTEXCLAMPED = 0x00100000;
		public const uint D3DPMISCCAPS_POSTBLENDSRGBCONVERT = 0x00200000;

		public const uint D3DCURSORCAPS_COLOR = 0x00000001;
		public const uint D3DCURSORCAPS_LOWRES = 0x00000002;

		public const uint D3DPRASTERCAPS_DITHER = 0x00000001;
		public const uint D3DPRASTERCAPS_ZTEST = 0x00000010;
		public const uint D3DPRASTERCAPS_FOGVERTEX = 0x00000080;
		public const uint D3DPRASTERCAPS_FOGTABLE = 0x00000100;
		public const uint D3DPRASTERCAPS_MIPMAPLODBIAS = 0x00002000;
		public const uint D3DPRASTERCAPS_ZBUFFERLESSHSR = 0x00008000;
		public const uint D3DPRASTERCAPS_FOGRANGE = 0x00010000;
		public const uint D3DPRASTERCAPS_ANISOTROPY = 0x00020000;
		public const uint D3DPRASTERCAPS_WBUFFER = 0x00040000;
		public const uint D3DPRASTERCAPS_WFOG = 0x00100000;
		public const uint D3DPRASTERCAPS_ZFOG = 0x00200000;
		public const uint D3DPRASTERCAPS_COLORPERSPECTIVE = 0x00400000;
		public const uint D3DPRASTERCAPS_SCISSORTEST = 0x01000000;
		public const uint D3DPRASTERCAPS_SLOPESCALEDEPTHBIAS = 0x02000000;
		public const uint D3DPRASTERCAPS_DEPTHBIAS = 0x04000000;
		public const uint D3DPRASTERCAPS_MULTISAMPLE_TOGGLE = 0x08000000;

		public const uint D3DPSHADECAPS_COLORGOURAUDRGB = 0x00000008;
		public const uint D3DPSHADECAPS_SPECULARGOURAUDRGB = 0x00000200;
		public const uint D3DPSHADECAPS_ALPHAGOURAUDBLEND = 0x00004000;
		public const uint D3DPSHADECAPS_FOGGOURAUD = 0x00080000;

		public const uint D3DPTEXTURECAPS_PERSPECTIVE = 0x00000001;
		public const uint D3DPTEXTURECAPS_POW2 = 0x00000002;
		public const uint D3DPTEXTURECAPS_ALPHA = 0x00000004;
		public const uint D3DPTEXTURECAPS_SQUAREONLY = 0x00000020;
		public const uint D3DPTEXTURECAPS_TEXREPEATNOTSCALEDBYSIZE = 0x00000040;
		public const uint D3DPTEXTURECAPS_ALPHAPALETTE = 0x00000080;
		public const uint D3DPTEXTURECAPS_NONPOW2CONDITIONAL = 0x00000100;
		public const uint D3DPTEXTURECAPS_PROJECTED = 0x00000400;
		public const uint D3DPTEXTURECAPS_CUBEMAP = 0x00000800;
		public const uint D3DPTEXTURECAPS_VOLUMEMAP = 0x00002000;
		public const uint D3DPTEXTURECAPS_MIPMAP = 0x00004000;
		public const uint D3DPTEXTURECAPS_MIPVOLUMEMAP = 0x00008000;
		public const uint D3DPTEXTURECAPS_MIPCUBEMAP = 0x00010000;
		public const uint D3DPTEXTURECAPS_CUBEMAP_POW2 = 0x00020000;
		public const uint D3DPTEXTURECAPS_VOLUMEMAP_POW2 = 0x00040000;
		public const uint D3DPTEXTURECAPS_NOPROJECTEDBUMPENV = 0x00200000;

		public const uint D3DPCMPCAPS_NEVER = 0x00000001;
		public const uint D3DPCMPCAPS_LESS = 0x00000002;
		public const uint D3DPCMPCAPS_EQUAL = 0x00000004;
		public const uint D3DPCMPCAPS_LESSEQUAL = 0x00000008;
		public const uint D3DPCMPCAPS_GREATER = 0x00000010;
		public const uint D3DPCMPCAPS_NOTEQUAL = 0x00000020;
		public const uint D3DPCMPCAPS_GREATEREQUAL = 0x00000040;
		public const uint D3DPCMPCAPS_ALWAYS = 0x00000080;

		public const uint D3DPBLENDCAPS_ZERO = 0x00000001;
		public const uint D3DPBLENDCAPS_ONE = 0x00000002;
		public const uint D3DPBLENDCAPS_SRCCOLOR = 0x00000004;
		public const uint D3DPBLENDCAPS_INVSRCCOLOR = 0x00000008;
		public const uint D3DPBLENDCAPS_SRCALPHA = 0x00000010;
		public const uint D3DPBLENDCAPS_INVSRCALPHA = 0x00000020;
		public const uint D3DPBLENDCAPS_DESTALPHA = 0x00000040;
		public const uint D3DPBLENDCAPS_INVDESTALPHA = 0x00000080;
		public const uint D3DPBLENDCAPS_DESTCOLOR = 0x00000100;
		public const uint D3DPBLENDCAPS_INVDESTCOLOR = 0x00000200;
		public const uint D3DPBLENDCAPS_SRCALPHASAT = 0x00000400;
		public const uint D3DPBLENDCAPS_BOTHSRCALPHA = 0x00000800;
		public const uint D3DPBLENDCAPS_BOTHINVSRCALPHA = 0x00001000;
		public const uint D3DPBLENDCAPS_BLENDFACTOR = 0x00002000;
		public const uint D3DPBLENDCAPS_SRCCOLOR2 = 0x00004000;
		public const uint D3DPBLENDCAPS_INVSRCCOLOR2 = 0x00008000;

		public enum D3DDEVTYPE
		{
			HAL = 1,
			REF = 2,
			SW = 3,
			NULLREF = 4,
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3DVSHADERCAPS2_0
		{
			public DWORD Caps;
			public INT DynamicFlowControlDepth;
			public INT NumTemps;
			public INT StaticFlowControlDepth;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3DPSHADERCAPS2_0
		{
			public DWORD Caps;
			public INT DynamicFlowControlDepth;
			public INT NumTemps;
			public INT StaticFlowControlDepth;
			public INT NumInstructionSlots;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3DCAPS9
		{
			/* Device Info */
			public D3DDEVTYPE DeviceType;
			public UINT AdapterOrdinal;

			/* Caps from DX7 Draw */
			public DWORD Caps;
			public DWORD Caps2;
			public DWORD Caps3;
			public DWORD PresentationIntervals;

			/* Cursor Caps */
			public DWORD CursorCaps;

			/* 3D Device Caps */
			public DWORD DevCaps;

			public DWORD PrimitiveMiscCaps;
			public DWORD RasterCaps;
			public DWORD ZCmpCaps;
			public DWORD SrcBlendCaps;
			public DWORD DestBlendCaps;
			public DWORD AlphaCmpCaps;
			public DWORD ShadeCaps;
			public DWORD TextureCaps;
			public DWORD TextureFilterCaps; // D3DPTFILTERCAPS for IDirect3DTexture9's
			public DWORD CubeTextureFilterCaps; // D3DPTFILTERCAPS for IDirect3DCubeTexture9's
			public DWORD VolumeTextureFilterCaps; // D3DPTFILTERCAPS for IDirect3DVolumeTexture9's
			public DWORD TextureAddressCaps; // D3DPTADDRESSCAPS for IDirect3DTexture9's
			public DWORD VolumeTextureAddressCaps; // D3DPTADDRESSCAPS for IDirect3DVolumeTexture9's

			public DWORD LineCaps; // D3DLINECAPS

			public DWORD MaxTextureWidth, MaxTextureHeight;
			public DWORD MaxVolumeExtent;

			public DWORD MaxTextureRepeat;
			public DWORD MaxTextureAspectRatio;
			public DWORD MaxAnisotropy;
			public float MaxVertexW;

			public float GuardBandLeft;
			public float GuardBandTop;
			public float GuardBandRight;
			public float GuardBandBottom;

			public float ExtentsAdjust;
			public DWORD StencilCaps;

			public DWORD FVFCaps;
			public DWORD TextureOpCaps;
			public DWORD MaxTextureBlendStages;
			public DWORD MaxSimultaneousTextures;

			public DWORD VertexProcessingCaps;
			public DWORD MaxActiveLights;
			public DWORD MaxUserClipPlanes;
			public DWORD MaxVertexBlendMatrices;
			public DWORD MaxVertexBlendMatrixIndex;

			public float MaxPointSize;

			public DWORD MaxPrimitiveCount; // max number of primitives per DrawPrimitive call
			public DWORD MaxVertexIndex;
			public DWORD MaxStreams;
			public DWORD MaxStreamStride; // max stride for SetStreamSource

			public DWORD VertexShaderVersion;
			public DWORD MaxVertexShaderConst; // number of vertex shader constant registers

			public DWORD PixelShaderVersion;
			public float PixelShader1xMaxValue; // max value storable in registers of ps.1.x shaders

			// Here are the DX9 specific ones
			public DWORD DevCaps2;

			public float MaxNpatchTessellationLevel;
			public DWORD Reserved5;

			public UINT MasterAdapterOrdinal; // ordinal of master adaptor for adapter group
			public UINT AdapterOrdinalInGroup; // ordinal inside the adapter group
			public UINT NumberOfAdaptersInGroup; // number of adapters in this adapter group (only if master)
			public DWORD DeclTypes; // Data types, supported in vertex declarations
			public DWORD NumSimultaneousRTs; // Will be at least 1
			public DWORD StretchRectFilterCaps; // Filter caps supported by StretchRect
			public D3DVSHADERCAPS2_0 VS20Caps;
			public D3DPSHADERCAPS2_0 PS20Caps;
			public DWORD VertexTextureFilterCaps; // D3DPTFILTERCAPS for IDirect3DTexture9's for texture, used in vertex shaders
			public DWORD MaxVShaderInstructionsExecuted; // maximum number of vertex shader instructions that can be executed
			public DWORD MaxPShaderInstructionsExecuted; // maximum number of pixel shader instructions that can be executed
			public DWORD MaxVertexShader30InstructionSlots;
			public DWORD MaxPixelShader30InstructionSlots;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3DADAPTER_IDENTIFIER9
		{
			[MarshalAs (UnmanagedType.ByValTStr, SizeConst = 512)]
			public string Driver;
			[MarshalAs (UnmanagedType.ByValTStr, SizeConst = 512)]
			public string Description;
			[MarshalAs (UnmanagedType.ByValTStr, SizeConst = 32)]
			public string DeviceName;
			public long DriverVersion;
			public DWORD DriverVersionLowPart;
			public DWORD DriverVersionHighPart;
			public DWORD VendorId;
			public DWORD DeviceId;
			public DWORD SubSysId;
			public DWORD Revision;
			public Guid DeviceIdentifier;
			public DWORD WHQLLevel;
		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid ("81BDCBCA-64D4-426d-AE8D-AD0147F4275C"),
		 InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
		public interface IDirect3D9
		{
			[PreserveSig]
			int RegisterSoftwareDevice ();
			[PreserveSig]
			int GetAdapterCount ();
			[PreserveSig]
			int GetAdapterIdentifier (uint Adapter, DWORD Flags, out D3DADAPTER_IDENTIFIER9 pIdentifier);
			[PreserveSig]
			uint GetAdapterModeCount ();
			[PreserveSig]
			int EnumAdapterModes ();
			[PreserveSig]
			int GetAdapterDisplayMode ();
			[PreserveSig]
			int CheckDeviceType ();
			[PreserveSig]
			int CheckDeviceFormat ();
			[PreserveSig]
			int CheckDeviceMultiSampleType ();
			[PreserveSig]
			int CheckDepthStencilMatch ();
			[PreserveSig]
			int CheckDeviceFormatConversion ();

			[PreserveSig]
			int GetDeviceCaps (uint Adapter, D3DDEVTYPE DeviceType, out D3DCAPS9 pCaps);
			[PreserveSig]
			IntPtr GetAdapterMonitor ();
			[PreserveSig]
			int CreateDevice ();
		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid ("02177241-69FC-400C-8FF1-93A44DF6861D"),
		 InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
		public interface IDirect3D9Ex : IDirect3D9
		{
			[PreserveSig]
			new int RegisterSoftwareDevice ();
			[PreserveSig]
			new int GetAdapterCount ();
			[PreserveSig]
			new int GetAdapterIdentifier (uint Adapter, DWORD Flags, out D3DADAPTER_IDENTIFIER9 pIdentifier);
			[PreserveSig]
			new uint GetAdapterModeCount ();
			[PreserveSig]
			new int EnumAdapterModes ();
			[PreserveSig]
			new int GetAdapterDisplayMode ();
			[PreserveSig]
			new int CheckDeviceType ();
			[PreserveSig]
			new int CheckDeviceFormat ();
			[PreserveSig]
			new int CheckDeviceMultiSampleType ();
			[PreserveSig]
			new int CheckDepthStencilMatch ();
			[PreserveSig]
			new int CheckDeviceFormatConversion ();

			[PreserveSig]
			new int GetDeviceCaps (uint Adapter, D3DDEVTYPE DeviceType, out D3DCAPS9 pCaps);
			[PreserveSig]
			new IntPtr GetAdapterMonitor ();
			[PreserveSig]
			new int CreateDevice ();

			[PreserveSig]
			int GetAdapterModeCountEx();
			[PreserveSig]
			int EnumAdapterModesEx ();
			[PreserveSig]
			int GetAdapterDisplayModeEx ();
			[PreserveSig]
			int CreateDeviceEx ();
			[PreserveSig]
			int GetAdapterLUID ();
		}
	}
}
