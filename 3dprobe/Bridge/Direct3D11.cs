using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using static _3dprobe.Bridge.DXGI;
using DWORD = System.UInt32;
using UINT = System.UInt32;
using BOOL = System.Int32;

namespace _3dprobe.Bridge
{
	public static class Direct3D11
	{
		[DllImport("d3d11.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int D3D11CreateDevice(
			[MarshalAs(UnmanagedType.Interface)]
			IDXGIAdapter pAdapter,
			D3D_DRIVER_TYPE DriverType,
			IntPtr Software,
			D3D11_CREATE_DEVICE_FLAG Flags,
			D3D_FEATURE_LEVEL[] pFeatureLevels,
			UINT FeatureLevels,
			UINT SDKVersion,
			[MarshalAs(UnmanagedType.Interface)] out ID3D11Device ppDevice,
			out D3D_FEATURE_LEVEL pFeatureLevel,
			[MarshalAs(UnmanagedType.Interface)] out ID3D11DeviceContext ppImmediateContext
		);

		public const uint D3D11_SDK_VERSION = 7;

		public enum D3D_DRIVER_TYPE
		{
			D3D_DRIVER_TYPE_UNKNOWN = 0,
			D3D_DRIVER_TYPE_HARDWARE = (D3D_DRIVER_TYPE_UNKNOWN + 1),
			D3D_DRIVER_TYPE_REFERENCE = (D3D_DRIVER_TYPE_HARDWARE + 1),
			D3D_DRIVER_TYPE_NULL = (D3D_DRIVER_TYPE_REFERENCE + 1),
			D3D_DRIVER_TYPE_SOFTWARE = (D3D_DRIVER_TYPE_NULL + 1),
			D3D_DRIVER_TYPE_WARP = (D3D_DRIVER_TYPE_SOFTWARE + 1),
		}

		public enum D3D_FEATURE_LEVEL
		{
			D3D_FEATURE_LEVEL_1_0_CORE = 0x1000,
			D3D_FEATURE_LEVEL_9_1 = 0x9100,
			D3D_FEATURE_LEVEL_9_2 = 0x9200,
			D3D_FEATURE_LEVEL_9_3 = 0x9300,
			D3D_FEATURE_LEVEL_10_0 = 0xa000,
			D3D_FEATURE_LEVEL_10_1 = 0xa100,
			D3D_FEATURE_LEVEL_11_0 = 0xb000,
			D3D_FEATURE_LEVEL_11_1 = 0xb100,
			D3D_FEATURE_LEVEL_12_0 = 0xc000,
			D3D_FEATURE_LEVEL_12_1 = 0xc100,
		}

		[Flags]
		public enum D3D11_CREATE_DEVICE_FLAG
		{
			D3D11_CREATE_DEVICE_SINGLETHREADED = 0x1,
			D3D11_CREATE_DEVICE_DEBUG = 0x2,
			D3D11_CREATE_DEVICE_SWITCH_TO_REF = 0x4,
			D3D11_CREATE_DEVICE_PREVENT_INTERNAL_THREADING_OPTIMIZATIONS = 0x8,
			D3D11_CREATE_DEVICE_BGRA_SUPPORT = 0x20,
			D3D11_CREATE_DEVICE_DEBUGGABLE = 0x40,
			D3D11_CREATE_DEVICE_PREVENT_ALTERING_LAYER_SETTINGS_FROM_REGISTRY = 0x80,
			D3D11_CREATE_DEVICE_DISABLE_GPU_TIMEOUT = 0x100,
			D3D11_CREATE_DEVICE_VIDEO_SUPPORT = 0x800,
		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid("db6f6ddb-ac77-4e88-8253-819df9bbf140"),
		 InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		public interface ID3D11Device
		{
			[PreserveSig]
			int CreateBuffer();

			[PreserveSig]
			int CreateTexture1D();

			[PreserveSig]
			int CreateTexture2D();

			[PreserveSig]
			int CreateTexture3D();

			[PreserveSig]
			int CreateShaderResourceView();

			[PreserveSig]
			int CreateUnorderedAccessView();

			[PreserveSig]
			int CreateRenderTargetView();

			[PreserveSig]
			int CreateDepthStencilView();

			[PreserveSig]
			int CreateInputLayout();

			[PreserveSig]
			int CreateVertexShader();

			[PreserveSig]
			int CreateGeometryShader();

			[PreserveSig]
			int CreateGeometryShaderWithStreamOutput();

			[PreserveSig]
			int CreatePixelShader();

			[PreserveSig]
			int CreateHullShader();

			[PreserveSig]
			int CreateDomainShader();

			[PreserveSig]
			int CreateComputeShader();

			[PreserveSig]
			int CreateClassLinkage();

			[PreserveSig]
			int CreateBlendState();

			[PreserveSig]
			int CreateDepthStencilState();

			[PreserveSig]
			int CreateRasterizerState();

			[PreserveSig]
			int CreateSamplerState();

			[PreserveSig]
			int CreateQuery();

			[PreserveSig]
			int CreatePredicate();

			[PreserveSig]
			int CreateCounter();

			[PreserveSig]
			int CreateDeferredContext();

			[PreserveSig]
			int OpenSharedResource();

			[PreserveSig]
			int CheckFormatSupport();

			[PreserveSig]
			int CheckMultisampleQualityLevels();

			[PreserveSig]
			int CheckCounterInfo();

			[PreserveSig]
			int CheckCounter();

			[PreserveSig]
			int CheckFeatureSupport(D3D11_FEATURE Feature, IntPtr pFeatureSupportData, UINT FeatureSupportDataSize);

			[PreserveSig]
			int GetPrivateData();

			[PreserveSig]
			int SetPrivateData();

			[PreserveSig]
			int SetPrivateDataInterface();

			[PreserveSig]
			D3D_FEATURE_LEVEL GetFeatureLevel();

			[PreserveSig]
			uint GetCreationFlags();

			[PreserveSig]
			int GetDeviceRemovedReason();

			[PreserveSig]
			int GetImmediateContext(out ID3D11DeviceContext ppImmediateContext);

			[PreserveSig]
			int SetExceptionMode();

			[PreserveSig]
			uint GetExceptionMode();
		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid("1841e5c8-16b0-489b-bcc8-44cfb0d5deae"),
		 InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		public interface ID3D11DeviceChild
		{

		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid("c0bfa96c-e089-44fb-8eaf-26f8796190da"),
		 InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		public interface ID3D11DeviceContext : ID3D11DeviceChild
		{

		}

		public enum D3D11_FEATURE
		{
			D3D11_FEATURE_THREADING = 0,
			D3D11_FEATURE_DOUBLES = (D3D11_FEATURE_THREADING + 1),
			D3D11_FEATURE_FORMAT_SUPPORT = (D3D11_FEATURE_DOUBLES + 1),
			D3D11_FEATURE_FORMAT_SUPPORT2 = (D3D11_FEATURE_FORMAT_SUPPORT + 1),
			D3D11_FEATURE_D3D10_X_HARDWARE_OPTIONS = (D3D11_FEATURE_FORMAT_SUPPORT2 + 1),
			D3D11_FEATURE_D3D11_OPTIONS = (D3D11_FEATURE_D3D10_X_HARDWARE_OPTIONS + 1),
			D3D11_FEATURE_ARCHITECTURE_INFO = (D3D11_FEATURE_D3D11_OPTIONS + 1),
			D3D11_FEATURE_D3D9_OPTIONS = (D3D11_FEATURE_ARCHITECTURE_INFO + 1),
			D3D11_FEATURE_SHADER_MIN_PRECISION_SUPPORT = (D3D11_FEATURE_D3D9_OPTIONS + 1),
			D3D11_FEATURE_D3D9_SHADOW_SUPPORT = (D3D11_FEATURE_SHADER_MIN_PRECISION_SUPPORT + 1),
			D3D11_FEATURE_D3D11_OPTIONS1 = (D3D11_FEATURE_D3D9_SHADOW_SUPPORT + 1),
			D3D11_FEATURE_D3D9_SIMPLE_INSTANCING_SUPPORT = (D3D11_FEATURE_D3D11_OPTIONS1 + 1),
			D3D11_FEATURE_MARKER_SUPPORT = (D3D11_FEATURE_D3D9_SIMPLE_INSTANCING_SUPPORT + 1),
			D3D11_FEATURE_D3D9_OPTIONS1 = (D3D11_FEATURE_MARKER_SUPPORT + 1),
			D3D11_FEATURE_D3D11_OPTIONS2 = (D3D11_FEATURE_D3D9_OPTIONS1 + 1),
			D3D11_FEATURE_D3D11_OPTIONS3 = (D3D11_FEATURE_D3D11_OPTIONS2 + 1),
			D3D11_FEATURE_GPU_VIRTUAL_ADDRESS_SUPPORT = (D3D11_FEATURE_D3D11_OPTIONS3 + 1),
			D3D11_FEATURE_D3D11_OPTIONS4 = (D3D11_FEATURE_GPU_VIRTUAL_ADDRESS_SUPPORT + 1),
			D3D11_FEATURE_SHADER_CACHE = (D3D11_FEATURE_D3D11_OPTIONS4 + 1),
			D3D11_FEATURE_D3D11_OPTIONS5 = (D3D11_FEATURE_SHADER_CACHE + 1)
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_THREADING
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool DriverConcurrentCreates;
			[MarshalAs (UnmanagedType.Bool)]
			public bool DriverCommandLists;
		}

		public struct D3D11_FEATURE_DATA_DOUBLES
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool DoublePrecisionFloatShaderOps;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_FORMAT_SUPPORT
		{
			public DXGI_FORMAT InFormat;
			public UINT OutFormatSupport;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_FORMAT_SUPPORT2
		{
			public DXGI_FORMAT InFormat;
			public UINT OutFormatSupport2;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D10_X_HARDWARE_OPTIONS
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool ComputeShaders_Plus_RawAndStructuredBuffers_Via_Shader_4_x;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D11_OPTIONS
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool OutputMergerLogicOp;
			[MarshalAs (UnmanagedType.Bool)]
			public bool UAVOnlyRenderingForcedSampleCount;
			[MarshalAs (UnmanagedType.Bool)]
			public bool DiscardAPIsSeenByDriver;
			[MarshalAs (UnmanagedType.Bool)]
			public bool FlagsForUpdateAndCopySeenByDriver;
			[MarshalAs (UnmanagedType.Bool)]
			public bool ClearView;
			[MarshalAs (UnmanagedType.Bool)]
			public bool CopyWithOverlap;
			[MarshalAs (UnmanagedType.Bool)]
			public bool ConstantBufferPartialUpdate;
			[MarshalAs (UnmanagedType.Bool)]
			public bool ConstantBufferOffsetting;
			[MarshalAs (UnmanagedType.Bool)]
			public bool MapNoOverwriteOnDynamicConstantBuffer;
			[MarshalAs (UnmanagedType.Bool)]
			public bool MapNoOverwriteOnDynamicBufferSRV;
			[MarshalAs (UnmanagedType.Bool)]
			public bool MultisampleRTVWithForcedSampleCountOne;
			[MarshalAs (UnmanagedType.Bool)]
			public bool SAD4ShaderInstructions;
			[MarshalAs (UnmanagedType.Bool)]
			public bool ExtendedDoublesShaderInstructions;
			[MarshalAs (UnmanagedType.Bool)]
			public bool ExtendedResourceSharing;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_ARCHITECTURE_INFO
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool TileBasedDeferredRenderer;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D9_OPTIONS
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool FullNonPow2TextureSupport;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D9_SHADOW_SUPPORT
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool SupportsDepthAsTextureWithLessEqualComparisonFilter;
		}


		public enum D3D11_SHADER_MIN_PRECISION_SUPPORT
		{
			D3D11_SHADER_MIN_PRECISION_10_BIT = 0x1,
			D3D11_SHADER_MIN_PRECISION_16_BIT = 0x2
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_SHADER_MIN_PRECISION_SUPPORT
		{
			public UINT PixelShaderMinPrecision;
			public UINT AllOtherShaderStagesMinPrecision;
		}


		public enum D3D11_TILED_RESOURCES_TIER
		{
			D3D11_TILED_RESOURCES_NOT_SUPPORTED = 0,
			D3D11_TILED_RESOURCES_TIER_1 = 1,
			D3D11_TILED_RESOURCES_TIER_2 = 2,
			D3D11_TILED_RESOURCES_TIER_3 = 3
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D11_OPTIONS1
		{
			public D3D11_TILED_RESOURCES_TIER TiledResourcesTier;
			[MarshalAs (UnmanagedType.Bool)]
			public bool MinMaxFiltering;
			[MarshalAs (UnmanagedType.Bool)]
			public bool ClearViewAlsoSupportsDepthOnlyFormats;
			[MarshalAs (UnmanagedType.Bool)]
			public bool MapOnDefaultBuffers;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D9_SIMPLE_INSTANCING_SUPPORT
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool SimpleInstancingSupported;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_MARKER_SUPPORT
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool Profile;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D9_OPTIONS1
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool FullNonPow2TextureSupported;
			[MarshalAs (UnmanagedType.Bool)]
			public bool DepthAsTextureWithLessEqualComparisonFilterSupported;
			[MarshalAs (UnmanagedType.Bool)]
			public bool SimpleInstancingSupported;
			[MarshalAs (UnmanagedType.Bool)]
			public bool TextureCubeFaceRenderTargetWithNonCubeDepthStencilSupported;
		}


		public enum D3D11_CONSERVATIVE_RASTERIZATION_TIER
		{
			D3D11_CONSERVATIVE_RASTERIZATION_NOT_SUPPORTED = 0,
			D3D11_CONSERVATIVE_RASTERIZATION_TIER_1 = 1,
			D3D11_CONSERVATIVE_RASTERIZATION_TIER_2 = 2,
			D3D11_CONSERVATIVE_RASTERIZATION_TIER_3 = 3
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D11_OPTIONS2
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool PSSpecifiedStencilRefSupported;
			[MarshalAs (UnmanagedType.Bool)]
			public bool TypedUAVLoadAdditionalFormats;
			[MarshalAs (UnmanagedType.Bool)]
			public bool ROVsSupported;
			public D3D11_CONSERVATIVE_RASTERIZATION_TIER ConservativeRasterizationTier;
			public D3D11_TILED_RESOURCES_TIER TiledResourcesTier;
			[MarshalAs (UnmanagedType.Bool)]
			public bool MapOnDefaultTextures;
			[MarshalAs (UnmanagedType.Bool)]
			public bool StandardSwizzle;
			[MarshalAs (UnmanagedType.Bool)]
			public bool UnifiedMemoryArchitecture;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D11_OPTIONS3
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool VPAndRTArrayIndexFromAnyShaderFeedingRasterizer;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_GPU_VIRTUAL_ADDRESS_SUPPORT
		{
			public UINT MaxGPUVirtualAddressBitsPerResource;
			public UINT MaxGPUVirtualAddressBitsPerProcess;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D11_OPTIONS4
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool ExtendedNV12SharedTextureSupported;
		}

		public enum D3D11_SHADER_CACHE_SUPPORT_FLAGS
		{
			D3D11_SHADER_CACHE_SUPPORT_NONE = 0,
			D3D11_SHADER_CACHE_SUPPORT_AUTOMATIC_INPROC_CACHE = 0x1,
			D3D11_SHADER_CACHE_SUPPORT_AUTOMATIC_DISK_CACHE = 0x2
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_SHADER_CACHE
		{
			public UINT SupportFlags;
		}

		public enum D3D11_SHARED_RESOURCE_TIER
		{
			D3D11_SHARED_RESOURCE_TIER_0 = 0,
			D3D11_SHARED_RESOURCE_TIER_1 = (D3D11_SHARED_RESOURCE_TIER_0 + 1),
			D3D11_SHARED_RESOURCE_TIER_2 = (D3D11_SHARED_RESOURCE_TIER_1 + 1),
			D3D11_SHARED_RESOURCE_TIER_3 = (D3D11_SHARED_RESOURCE_TIER_2 + 1),
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D11_FEATURE_DATA_D3D11_OPTIONS5
		{
			public D3D11_SHARED_RESOURCE_TIER SharedResourceTier;
		}
	}
}
