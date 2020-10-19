using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using DWORD = System.UInt32;

namespace _3dprobe.Bridge
{
	public static class DXGI
	{
		public static readonly Guid IID_DXGIFactory = new Guid("7b7166ec-21c7-44ae-b21a-c9ae321ae369");
		public static readonly Guid IID_DXGIFactory2 = new Guid ("50c83a1c-e072-4c48-87b0-3630fa36a6d0");

		[DllImport ("dxgi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public static extern int CreateDXGIFactory (Guid riid,
			[MarshalAs (UnmanagedType.Interface)] out object ppFactory);

		[DllImport ("dxgi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public static extern int CreateDXGIFactory2 (uint Flags, Guid riid,
			[MarshalAs (UnmanagedType.Interface)] out object ppFactory);

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid ("aec22fb8-76f3-4639-9be0-28eb43a67a2e"),
		 InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
		public interface IDXGIObject
		{
			[PreserveSig]
			int SetPrivateData ();

			[PreserveSig]
			int SetPrivateDataInterface ();

			[PreserveSig]
			int GetPrivateData ();

			[PreserveSig]
			int GetParent ();
		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid ("7b7166ec-21c7-44ae-b21a-c9ae321ae369"),
		 InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
		public interface IDXGIFactory : IDXGIObject
		{
			[PreserveSig]
			new int SetPrivateData ();

			[PreserveSig]
			new int SetPrivateDataInterface ();

			[PreserveSig]
			new int GetPrivateData ();

			[PreserveSig]
			new int GetParent ();

			[PreserveSig]
			int EnumAdapters (uint Adapter,
				[MarshalAs (UnmanagedType.Interface)] out IDXGIAdapter ppAdapter);

			[PreserveSig]
			int MakeWindowAssociation ();

			[PreserveSig]
			int GetWindowAssociation ();

			[PreserveSig]
			int CreateSwapChain ();

			[PreserveSig]
			int CreateSoftwareAdapter ();
		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid ("770aae78-f26f-4dba-a829-253c83d1b387"),
		 InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
		public interface IDXGIFactory1 : IDXGIFactory
		{
			[PreserveSig]
			new int SetPrivateData ();

			[PreserveSig]
			new int SetPrivateDataInterface ();

			[PreserveSig]
			new int GetPrivateData ();

			[PreserveSig]
			new int GetParent ();

			[PreserveSig]
			new int EnumAdapters (uint Adapter,
				[MarshalAs (UnmanagedType.Interface)] out IDXGIAdapter ppAdapter);

			[PreserveSig]
			new int MakeWindowAssociation ();

			[PreserveSig]
			new int GetWindowAssociation ();

			[PreserveSig]
			new int CreateSwapChain ();

			[PreserveSig]
			new int CreateSoftwareAdapter ();

			[PreserveSig]
			int EnumAdapters1 (uint Adapter,
				[MarshalAs (UnmanagedType.Interface)] out IDXGIAdapter1 ppAdapter);

			[PreserveSig]
			[return: MarshalAs (UnmanagedType.Bool)]
			bool IsCurrent ();
		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid ("50c83a1c-e072-4c48-87b0-3630fa36a6d0"),
		 InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
		public interface IDXGIFactory2 : IDXGIFactory1
		{
			[PreserveSig]
			new int SetPrivateData ();

			[PreserveSig]
			new int SetPrivateDataInterface ();

			[PreserveSig]
			new int GetPrivateData ();

			[PreserveSig]
			new int GetParent ();

			[PreserveSig]
			new int EnumAdapters (uint Adapter,
				[MarshalAs (UnmanagedType.Interface)] out IDXGIAdapter ppAdapter);

			[PreserveSig]
			new int MakeWindowAssociation ();

			[PreserveSig]
			new int GetWindowAssociation ();

			[PreserveSig]
			new int CreateSwapChain ();

			[PreserveSig]
			new int CreateSoftwareAdapter ();

			[PreserveSig]
			new int EnumAdapters1 (uint Adapter,
				[MarshalAs (UnmanagedType.Interface)] out IDXGIAdapter1 ppAdapter);

			[PreserveSig]
			[return: MarshalAs (UnmanagedType.Bool)]
			new bool IsCurrent ();

			[PreserveSig]
			[return: MarshalAs (UnmanagedType.Bool)]
			bool IsWindowedStereoEnabled ();

			[PreserveSig]
			int CreateSwapChainForHwnd ();

			[PreserveSig]
			int CreateSwapChainForCoreWindow ();

			[PreserveSig]
			int GetSharedResourceAdapterLuid ();

			[PreserveSig]
			int RegisterStereoStatusWindow ();

			[PreserveSig]
			int RegisterStereoStatusEvent ();

			[PreserveSig]
			int UnregisterStereoStatus ();

			[PreserveSig]
			int RegisterOcclusionStatusWindow ();

			[PreserveSig]
			int RegisterOcclusionStatusEvent ();

			[PreserveSig]
			int UnregisterOcclusionStatus ();

			[PreserveSig]
			int CreateSwapChainForComposition ();
		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid ("2411e7e1-12ac-4ccf-bd14-9798e8534dc0"),
		 InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
		public interface IDXGIAdapter : IDXGIObject
		{
			[PreserveSig]
			new int SetPrivateData ();

			[PreserveSig]
			new int SetPrivateDataInterface ();

			[PreserveSig]
			new int GetPrivateData ();

			[PreserveSig]
			new int GetParent ();

			[PreserveSig]
			int EnumOutputs ();

			[PreserveSig]
			int GetDesc (out DXGI_ADAPTER_DESC pDesc);
		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid ("29038f61-3839-4626-91fd-086879011a05"),
		 InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
		public interface IDXGIAdapter1 : IDXGIAdapter
		{
			[PreserveSig]
			new int SetPrivateData ();

			[PreserveSig]
			new int SetPrivateDataInterface ();

			[PreserveSig]
			new int GetPrivateData ();

			[PreserveSig]
			new int GetParent ();

			[PreserveSig]
			new int EnumOutputs ();

			[PreserveSig]
			new int GetDesc (out DXGI_ADAPTER_DESC pDesc);

			[PreserveSig]
			int GetDesc1 (out DXGI_ADAPTER_DESC1 pDesc);
		}

		[StructLayout (LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct LUID
		{
			public DWORD LowPart;
			public int HighPart;
		}

		[StructLayout (LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DXGI_ADAPTER_DESC
		{
			[MarshalAs (UnmanagedType.ByValTStr, SizeConst = 128)]
			public string Description;

			public uint VendorId;
			public uint DeviceId;
			public uint SubSysId;
			public uint Revision;
			public IntPtr DedicatedVideoMemory;
			public IntPtr DedicatedSystemMemory;
			public IntPtr SharedSystemMemory;
			public LUID AdapterLuid;
		}

		[StructLayout (LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DXGI_ADAPTER_DESC1
		{
			[MarshalAs (UnmanagedType.ByValTStr, SizeConst = 128)]
			public string Description;

			public uint VendorId;
			public uint DeviceId;
			public uint SubSysId;
			public uint Revision;
			public IntPtr DedicatedVideoMemory;
			public IntPtr DedicatedSystemMemory;
			public IntPtr SharedSystemMemory;
			public LUID AdapterLuid;
			public uint Flags;
		}

		public enum DXGI_FORMAT
		{
			DXGI_FORMAT_UNKNOWN = 0,
			DXGI_FORMAT_R32G32B32A32_TYPELESS = 1,
			DXGI_FORMAT_R32G32B32A32_FLOAT = 2,
			DXGI_FORMAT_R32G32B32A32_UINT = 3,
			DXGI_FORMAT_R32G32B32A32_SINT = 4,
			DXGI_FORMAT_R32G32B32_TYPELESS = 5,
			DXGI_FORMAT_R32G32B32_FLOAT = 6,
			DXGI_FORMAT_R32G32B32_UINT = 7,
			DXGI_FORMAT_R32G32B32_SINT = 8,
			DXGI_FORMAT_R16G16B16A16_TYPELESS = 9,
			DXGI_FORMAT_R16G16B16A16_FLOAT = 10,
			DXGI_FORMAT_R16G16B16A16_UNORM = 11,
			DXGI_FORMAT_R16G16B16A16_UINT = 12,
			DXGI_FORMAT_R16G16B16A16_SNORM = 13,
			DXGI_FORMAT_R16G16B16A16_SINT = 14,
			DXGI_FORMAT_R32G32_TYPELESS = 15,
			DXGI_FORMAT_R32G32_FLOAT = 16,
			DXGI_FORMAT_R32G32_UINT = 17,
			DXGI_FORMAT_R32G32_SINT = 18,
			DXGI_FORMAT_R32G8X24_TYPELESS = 19,
			DXGI_FORMAT_D32_FLOAT_S8X24_UINT = 20,
			DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS = 21,
			DXGI_FORMAT_X32_TYPELESS_G8X24_UINT = 22,
			DXGI_FORMAT_R10G10B10A2_TYPELESS = 23,
			DXGI_FORMAT_R10G10B10A2_UNORM = 24,
			DXGI_FORMAT_R10G10B10A2_UINT = 25,
			DXGI_FORMAT_R11G11B10_FLOAT = 26,
			DXGI_FORMAT_R8G8B8A8_TYPELESS = 27,
			DXGI_FORMAT_R8G8B8A8_UNORM = 28,
			DXGI_FORMAT_R8G8B8A8_UNORM_SRGB = 29,
			DXGI_FORMAT_R8G8B8A8_UINT = 30,
			DXGI_FORMAT_R8G8B8A8_SNORM = 31,
			DXGI_FORMAT_R8G8B8A8_SINT = 32,
			DXGI_FORMAT_R16G16_TYPELESS = 33,
			DXGI_FORMAT_R16G16_FLOAT = 34,
			DXGI_FORMAT_R16G16_UNORM = 35,
			DXGI_FORMAT_R16G16_UINT = 36,
			DXGI_FORMAT_R16G16_SNORM = 37,
			DXGI_FORMAT_R16G16_SINT = 38,
			DXGI_FORMAT_R32_TYPELESS = 39,
			DXGI_FORMAT_D32_FLOAT = 40,
			DXGI_FORMAT_R32_FLOAT = 41,
			DXGI_FORMAT_R32_UINT = 42,
			DXGI_FORMAT_R32_SINT = 43,
			DXGI_FORMAT_R24G8_TYPELESS = 44,
			DXGI_FORMAT_D24_UNORM_S8_UINT = 45,
			DXGI_FORMAT_R24_UNORM_X8_TYPELESS = 46,
			DXGI_FORMAT_X24_TYPELESS_G8_UINT = 47,
			DXGI_FORMAT_R8G8_TYPELESS = 48,
			DXGI_FORMAT_R8G8_UNORM = 49,
			DXGI_FORMAT_R8G8_UINT = 50,
			DXGI_FORMAT_R8G8_SNORM = 51,
			DXGI_FORMAT_R8G8_SINT = 52,
			DXGI_FORMAT_R16_TYPELESS = 53,
			DXGI_FORMAT_R16_FLOAT = 54,
			DXGI_FORMAT_D16_UNORM = 55,
			DXGI_FORMAT_R16_UNORM = 56,
			DXGI_FORMAT_R16_UINT = 57,
			DXGI_FORMAT_R16_SNORM = 58,
			DXGI_FORMAT_R16_SINT = 59,
			DXGI_FORMAT_R8_TYPELESS = 60,
			DXGI_FORMAT_R8_UNORM = 61,
			DXGI_FORMAT_R8_UINT = 62,
			DXGI_FORMAT_R8_SNORM = 63,
			DXGI_FORMAT_R8_SINT = 64,
			DXGI_FORMAT_A8_UNORM = 65,
			DXGI_FORMAT_R1_UNORM = 66,
			DXGI_FORMAT_R9G9B9E5_SHAREDEXP = 67,
			DXGI_FORMAT_R8G8_B8G8_UNORM = 68,
			DXGI_FORMAT_G8R8_G8B8_UNORM = 69,
			DXGI_FORMAT_BC1_TYPELESS = 70,
			DXGI_FORMAT_BC1_UNORM = 71,
			DXGI_FORMAT_BC1_UNORM_SRGB = 72,
			DXGI_FORMAT_BC2_TYPELESS = 73,
			DXGI_FORMAT_BC2_UNORM = 74,
			DXGI_FORMAT_BC2_UNORM_SRGB = 75,
			DXGI_FORMAT_BC3_TYPELESS = 76,
			DXGI_FORMAT_BC3_UNORM = 77,
			DXGI_FORMAT_BC3_UNORM_SRGB = 78,
			DXGI_FORMAT_BC4_TYPELESS = 79,
			DXGI_FORMAT_BC4_UNORM = 80,
			DXGI_FORMAT_BC4_SNORM = 81,
			DXGI_FORMAT_BC5_TYPELESS = 82,
			DXGI_FORMAT_BC5_UNORM = 83,
			DXGI_FORMAT_BC5_SNORM = 84,
			DXGI_FORMAT_B5G6R5_UNORM = 85,
			DXGI_FORMAT_B5G5R5A1_UNORM = 86,
			DXGI_FORMAT_B8G8R8A8_UNORM = 87,
			DXGI_FORMAT_B8G8R8X8_UNORM = 88,
			DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM = 89,
			DXGI_FORMAT_B8G8R8A8_TYPELESS = 90,
			DXGI_FORMAT_B8G8R8A8_UNORM_SRGB = 91,
			DXGI_FORMAT_B8G8R8X8_TYPELESS = 92,
			DXGI_FORMAT_B8G8R8X8_UNORM_SRGB = 93,
			DXGI_FORMAT_BC6H_TYPELESS = 94,
			DXGI_FORMAT_BC6H_UF16 = 95,
			DXGI_FORMAT_BC6H_SF16 = 96,
			DXGI_FORMAT_BC7_TYPELESS = 97,
			DXGI_FORMAT_BC7_UNORM = 98,
			DXGI_FORMAT_BC7_UNORM_SRGB = 99,
			DXGI_FORMAT_AYUV = 100,
			DXGI_FORMAT_Y410 = 101,
			DXGI_FORMAT_Y416 = 102,
			DXGI_FORMAT_NV12 = 103,
			DXGI_FORMAT_P010 = 104,
			DXGI_FORMAT_P016 = 105,
			DXGI_FORMAT_420_OPAQUE = 106,
			DXGI_FORMAT_YUY2 = 107,
			DXGI_FORMAT_Y210 = 108,
			DXGI_FORMAT_Y216 = 109,
			DXGI_FORMAT_NV11 = 110,
			DXGI_FORMAT_AI44 = 111,
			DXGI_FORMAT_IA44 = 112,
			DXGI_FORMAT_P8 = 113,
			DXGI_FORMAT_A8P8 = 114,
			DXGI_FORMAT_B4G4R4A4_UNORM = 115,

			DXGI_FORMAT_P208 = 130,
			DXGI_FORMAT_V208 = 131,
			DXGI_FORMAT_V408 = 132,
		}
	}
}
