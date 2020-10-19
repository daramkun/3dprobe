using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using static _3dprobe.Bridge.DXGI;
using static _3dprobe.Bridge.Direct3D11;
using BOOL = System.Int32;
using UINT = System.UInt32;

namespace _3dprobe.Bridge
{
	public static class Direct3D12
	{
		public static readonly Guid IID_D3D12Device = new Guid("189819f1-1db6-4b57-be54-1821339b85f7");

		[DllImport("d3d12.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int D3D12CreateDevice([MarshalAs(UnmanagedType.Interface)] object pAdapter,
			D3D_FEATURE_LEVEL MinimumFeatureLevel, Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppDevice);

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid ("c4fec28f-7966-4e95-9f94-f431cb56c3b8"),
		 InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
		public interface ID3D12Object
		{
			[PreserveSig]
			int GetPrivateData();

			[PreserveSig]
			int SetPrivateData();

			[PreserveSig]
			int SetPrivateDataInterface();

			[PreserveSig]
			int SetName();
		}

		[ComImport,
		 SuppressUnmanagedCodeSecurity,
		 Guid("189819f1-1db6-4b57-be54-1821339b85f7"),
		 InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		public interface ID3D12Device : ID3D12Object
		{
			[PreserveSig]
			new int GetPrivateData();

			[PreserveSig]
			new int SetPrivateData();

			[PreserveSig]
			new int SetPrivateDataInterface();

			[PreserveSig]
			new int SetName();

			[PreserveSig]
			uint GetNodeCount();

			[PreserveSig]
			int CreateCommandQueue();

			[PreserveSig]
			int CreateCommandAllocator();

			[PreserveSig]
			int CreateGraphicsPipelineState();

			[PreserveSig]
			int CreateComputePipelineState();

			[PreserveSig]
			int CreateCommandList();

			[PreserveSig]
			int CheckFeatureSupport(D3D12_FEATURE Feature, IntPtr pFeatureSupportData, uint FeatureSupportDataSize);

			[PreserveSig]
			int CreateDescriptorHeap();

			[PreserveSig]
			int GetDescriptorHandleIncrementSize();

			[PreserveSig]
			int CreateRootSignature();

			[PreserveSig]
			int CreateConstantBufferView();

			[PreserveSig]
			int CreateShaderResourceView();

			[PreserveSig]
			int CreateUnorderedAccessView();

			[PreserveSig]
			int CreateRenderTargetView();

			[PreserveSig]
			int CreateDepthStencilView();

			[PreserveSig]
			int CreateSampler();

			[PreserveSig]
			int CopyDescriptors();

			[PreserveSig]
			int CopyDescriptorsSimple();

			[PreserveSig]
			void GetResourceAllocationInfo();

			[PreserveSig]
			void GetCustomHeapProperties();

			[PreserveSig]
			int CreateCommittedResource();

			[PreserveSig]
			int CreateHeap();

			[PreserveSig]
			int CreatePlacedResource();

			[PreserveSig]
			int CreateReservedResource();

			[PreserveSig]
			int CreateSharedHandle();

			[PreserveSig]
			int OpenSharedHandle();

			[PreserveSig]
			int OpenSharedHandleByName();

			[PreserveSig]
			int MakeResident();

			[PreserveSig]
			int Evict();

			[PreserveSig]
			int CreateFence();

			[PreserveSig]
			int GetDeviceRemovedReason();

			[PreserveSig]
			int GetCopyableFootprints();

			[PreserveSig]
			int CreateQueryHeap();

			[PreserveSig]
			int SetStablePowerState();

			[PreserveSig]
			int CreateCommandSignature();

			[PreserveSig]
			int GetResourceTiling();

			[PreserveSig]
			void GetAdapterLuid();
		}

		public enum D3D12_FEATURE
		{
			D3D12_FEATURE_D3D12_OPTIONS = 0,
			D3D12_FEATURE_ARCHITECTURE = 1,
			D3D12_FEATURE_FEATURE_LEVELS = 2,
			D3D12_FEATURE_FORMAT_SUPPORT = 3,
			D3D12_FEATURE_MULTISAMPLE_QUALITY_LEVELS = 4,
			D3D12_FEATURE_FORMAT_INFO = 5,
			D3D12_FEATURE_GPU_VIRTUAL_ADDRESS_SUPPORT = 6,
			D3D12_FEATURE_SHADER_MODEL = 7,
			D3D12_FEATURE_D3D12_OPTIONS1 = 8,
			D3D12_FEATURE_PROTECTED_RESOURCE_SESSION_SUPPORT = 10,
			D3D12_FEATURE_ROOT_SIGNATURE = 12,
			D3D12_FEATURE_ARCHITECTURE1 = 16,
			D3D12_FEATURE_D3D12_OPTIONS2 = 18,
			D3D12_FEATURE_SHADER_CACHE = 19,
			D3D12_FEATURE_COMMAND_QUEUE_PRIORITY = 20,
			D3D12_FEATURE_D3D12_OPTIONS3 = 21,
			D3D12_FEATURE_EXISTING_HEAPS = 22,
			D3D12_FEATURE_D3D12_OPTIONS4 = 23,
			D3D12_FEATURE_SERIALIZATION = 24,
			D3D12_FEATURE_CROSS_NODE = 25,
			D3D12_FEATURE_D3D12_OPTIONS5 = 27,
			D3D12_FEATURE_D3D12_OPTIONS6 = 30,
			D3D12_FEATURE_QUERY_META_COMMAND = 31
		}

		public enum D3D12_MULTISAMPLE_QUALITY_LEVEL_FLAGS
		{
			D3D12_MULTISAMPLE_QUALITY_LEVELS_FLAG_NONE = 0,
			D3D12_MULTISAMPLE_QUALITY_LEVELS_FLAG_TILED_RESOURCE = 0x1
		}

		public enum D3D12_CROSS_NODE_SHARING_TIER
		{
			D3D12_CROSS_NODE_SHARING_TIER_NOT_SUPPORTED = 0,
			D3D12_CROSS_NODE_SHARING_TIER_1_EMULATED = 1,
			D3D12_CROSS_NODE_SHARING_TIER_1 = 2,
			D3D12_CROSS_NODE_SHARING_TIER_2 = 3,
			D3D12_CROSS_NODE_SHARING_TIER_3 = 4
		}

		public enum D3D12_RESOURCE_HEAP_TIER
		{
			D3D12_RESOURCE_HEAP_TIER_1 = 1,
			D3D12_RESOURCE_HEAP_TIER_2 = 2
		}

		public enum D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER
		{
			D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER_NOT_SUPPORTED = 0,
			D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER_1 = 1,
			D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER_2 = 2
		}

		public enum D3D12_VIEW_INSTANCING_TIER
		{
			D3D12_VIEW_INSTANCING_TIER_NOT_SUPPORTED = 0,
			D3D12_VIEW_INSTANCING_TIER_1 = 1,
			D3D12_VIEW_INSTANCING_TIER_2 = 2,
			D3D12_VIEW_INSTANCING_TIER_3 = 3
		}

		public enum D3D12_SHADER_MIN_PRECISION_SUPPORT
		{
			D3D12_SHADER_MIN_PRECISION_SUPPORT_NONE = 0,
			D3D12_SHADER_MIN_PRECISION_SUPPORT_10_BIT = 0x1,
			D3D12_SHADER_MIN_PRECISION_SUPPORT_16_BIT = 0x2
		}

		public enum D3D12_TILED_RESOURCES_TIER
		{
			D3D12_TILED_RESOURCES_TIER_NOT_SUPPORTED = 0,
			D3D12_TILED_RESOURCES_TIER_1 = 1,
			D3D12_TILED_RESOURCES_TIER_2 = 2,
			D3D12_TILED_RESOURCES_TIER_3 = 3,
			D3D12_TILED_RESOURCES_TIER_4 = 4
		}

		public enum D3D12_RESOURCE_BINDING_TIER
		{
			D3D12_RESOURCE_BINDING_TIER_1 = 1,
			D3D12_RESOURCE_BINDING_TIER_2 = 2,
			D3D12_RESOURCE_BINDING_TIER_3 = 3
		}

		public enum D3D12_CONSERVATIVE_RASTERIZATION_TIER
		{
			D3D12_CONSERVATIVE_RASTERIZATION_TIER_NOT_SUPPORTED = 0,
			D3D12_CONSERVATIVE_RASTERIZATION_TIER_1 = 1,
			D3D12_CONSERVATIVE_RASTERIZATION_TIER_2 = 2,
			D3D12_CONSERVATIVE_RASTERIZATION_TIER_3 = 3
		}

		public enum D3D_ROOT_SIGNATURE_VERSION
		{
			D3D_ROOT_SIGNATURE_VERSION_1 = 0x1,
			D3D_ROOT_SIGNATURE_VERSION_1_0 = 0x1,
			D3D_ROOT_SIGNATURE_VERSION_1_1 = 0x2
		}

		public enum D3D_SHADER_MODEL
		{
			D3D_SHADER_MODEL_5_1 = 0x51,
			D3D_SHADER_MODEL_6_0 = 0x60,
			D3D_SHADER_MODEL_6_1 = 0x61,
			D3D_SHADER_MODEL_6_2 = 0x62,
			D3D_SHADER_MODEL_6_3 = 0x63,
			D3D_SHADER_MODEL_6_4 = 0x64,
			D3D_SHADER_MODEL_6_5 = 0x65
		}

		public enum D3D12_SHADER_CACHE_SUPPORT_FLAGS
		{
			D3D12_SHADER_CACHE_SUPPORT_NONE = 0,
			D3D12_SHADER_CACHE_SUPPORT_SINGLE_PSO = 0x1,
			D3D12_SHADER_CACHE_SUPPORT_LIBRARY = 0x2,
			D3D12_SHADER_CACHE_SUPPORT_AUTOMATIC_INPROC_CACHE = 0x4,
			D3D12_SHADER_CACHE_SUPPORT_AUTOMATIC_DISK_CACHE = 0x8
		}

		public enum D3D12_COMMAND_LIST_TYPE
		{
			D3D12_COMMAND_LIST_TYPE_DIRECT = 0,
			D3D12_COMMAND_LIST_TYPE_BUNDLE = 1,
			D3D12_COMMAND_LIST_TYPE_COMPUTE = 2,
			D3D12_COMMAND_LIST_TYPE_COPY = 3,
			D3D12_COMMAND_LIST_TYPE_VIDEO_DECODE = 4,
			D3D12_COMMAND_LIST_TYPE_VIDEO_PROCESS = 5,
			D3D12_COMMAND_LIST_TYPE_VIDEO_ENCODE = 6
		}

		public enum D3D12_COMMAND_LIST_SUPPORT_FLAGS
		{
			D3D12_COMMAND_LIST_SUPPORT_FLAG_NONE = 0,
			D3D12_COMMAND_LIST_SUPPORT_FLAG_DIRECT = (1 << D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT),
			D3D12_COMMAND_LIST_SUPPORT_FLAG_BUNDLE = (1 << D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_BUNDLE),
			D3D12_COMMAND_LIST_SUPPORT_FLAG_COMPUTE = (1 << D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_COMPUTE),
			D3D12_COMMAND_LIST_SUPPORT_FLAG_COPY = (1 << D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_COPY),
			D3D12_COMMAND_LIST_SUPPORT_FLAG_VIDEO_DECODE = (1 << D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_VIDEO_DECODE),
			D3D12_COMMAND_LIST_SUPPORT_FLAG_VIDEO_PROCESS = (1 << D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_VIDEO_PROCESS),
			D3D12_COMMAND_LIST_SUPPORT_FLAG_VIDEO_ENCODE = (1 << D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_VIDEO_ENCODE)
		}

		public enum D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER
		{
			D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_0 = 0,
			D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_1 = (D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_0 + 1)
		}

		public enum D3D12_HEAP_SERIALIZATION_TIER
		{
			D3D12_HEAP_SERIALIZATION_TIER_0 = 0,
			D3D12_HEAP_SERIALIZATION_TIER_10 = 10
		}

		public enum D3D12_RENDER_PASS_TIER
		{
			D3D12_RENDER_PASS_TIER_0 = 0,
			D3D12_RENDER_PASS_TIER_1 = 1,
			D3D12_RENDER_PASS_TIER_2 = 2
		}

		public enum D3D12_RAYTRACING_TIER
		{
			D3D12_RAYTRACING_TIER_NOT_SUPPORTED = 0,
			D3D12_RAYTRACING_TIER_1_0 = 10
		}

		public enum D3D12_VARIABLE_SHADING_RATE_TIER
		{
			D3D12_VARIABLE_SHADING_RATE_TIER_NOT_SUPPORTED = 0,
			D3D12_VARIABLE_SHADING_RATE_TIER_1 = 1,
			D3D12_VARIABLE_SHADING_RATE_TIER_2 = 2
		}

		public enum D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAGS
		{
			D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAG_NONE = 0,
			D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAG_SUPPORTED = 0x1
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool DoublePrecisionFloatShaderOps;
			[MarshalAs (UnmanagedType.Bool)]
			public bool OutputMergerLogicOp;
			public D3D12_SHADER_MIN_PRECISION_SUPPORT MinPrecisionSupport;
			public D3D12_TILED_RESOURCES_TIER TiledResourcesTier;
			public D3D12_RESOURCE_BINDING_TIER ResourceBindingTier;
			[MarshalAs (UnmanagedType.Bool)]
			public bool PSSpecifiedStencilRefSupported;
			[MarshalAs (UnmanagedType.Bool)]
			public bool TypedUAVLoadAdditionalFormats;
			[MarshalAs (UnmanagedType.Bool)]
			public bool ROVsSupported;
			public D3D12_CONSERVATIVE_RASTERIZATION_TIER ConservativeRasterizationTier;
			public int MaxGPUVirtualAddressBitsPerResource;
			[MarshalAs (UnmanagedType.Bool)]
			public bool StandardSwizzle64KBSupported;
			public D3D12_CROSS_NODE_SHARING_TIER CrossNodeSharingTier;
			[MarshalAs (UnmanagedType.Bool)]
			public bool CrossAdapterRowMajorTextureSupported;
			[MarshalAs (UnmanagedType.Bool)]
			public bool VPAndRTArrayIndexFromAnyShaderFeedingRasterizerSupportedWithoutGSEmulation;
			public D3D12_RESOURCE_HEAP_TIER ResourceHeapTier;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS1
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool WaveOps;
			public int WaveLaneCountMin;
			public int WaveLaneCountMax;
			public int TotalLaneCount;
			[MarshalAs (UnmanagedType.Bool)]
			public bool ExpandedComputeResourceStates;
			[MarshalAs (UnmanagedType.Bool)]
			public bool Int64ShaderOps;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS2
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool DepthBoundsTestSupported;
			public D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER ProgrammableSamplePositionsTier;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS3
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool CopyQueueTimestampQueriesSupported;
			[MarshalAs (UnmanagedType.Bool)]
			public bool CastingFullyTypedFormatSupported;
			public D3D12_COMMAND_LIST_SUPPORT_FLAGS WriteBufferImmediateSupportFlags;
			public D3D12_VIEW_INSTANCING_TIER ViewInstancingTier;
			public bool BarycentricsSupported;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS4
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool MSAA64KBAlignedTextureSupported;
			public D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER SharedResourceCompatibilityTier;
			[MarshalAs (UnmanagedType.Bool)]
			public bool Native16BitShaderOpsSupported;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS5
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool SRVOnlyTiledResourceTier3;
			public D3D12_RENDER_PASS_TIER RenderPassesTier;
			public D3D12_RAYTRACING_TIER RaytracingTier;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS6
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool AdditionalShadingRatesSupported;
			[MarshalAs (UnmanagedType.Bool)]
			public bool PerPrimitiveShadingRateSupportedWithViewportIndexing;
			public D3D12_VARIABLE_SHADING_RATE_TIER VariableShadingRateTier;
			public UINT ShadingRateImageTileSize;
			[MarshalAs (UnmanagedType.Bool)]
			public bool BackgroundProcessingSupported;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_ROOT_SIGNATURE
		{
			public D3D_ROOT_SIGNATURE_VERSION HighestVersion;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_ARCHITECTURE
		{
			public int NodeIndex;
			[MarshalAs (UnmanagedType.Bool)]
			public bool TileBasedRenderer;
			[MarshalAs (UnmanagedType.Bool)]
			public bool UMA;
			[MarshalAs (UnmanagedType.Bool)]
			public bool CacheCoherentUMA;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_ARCHITECTURE1
		{
			public int NodeIndex;
			[MarshalAs (UnmanagedType.Bool)]
			public bool TileBasedRenderer;
			[MarshalAs (UnmanagedType.Bool)]
			public bool UMA;
			[MarshalAs (UnmanagedType.Bool)]
			public bool CacheCoherentUMA;
			[MarshalAs (UnmanagedType.Bool)]
			public bool IsolatedMMU;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_SHADER_MODEL
		{
			public D3D_SHADER_MODEL HighestShaderModel;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_MULTISAMPLE_QUALITY_LEVELS
		{
			public DXGI_FORMAT Format;
			public int SampleCount;
			public D3D12_MULTISAMPLE_QUALITY_LEVEL_FLAGS Flags;
			public int NumQualityLevels;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_GPU_VIRTUAL_ADDRESS_SUPPORT
		{
			public int MaxGPUVirtualAddressBitsPerResource;
			public int MaxGPUVirtualAddressBitsPerProcess;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_SHADER_CACHE
		{
			public D3D12_SHADER_CACHE_SUPPORT_FLAGS SupportFlags;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_COMMAND_QUEUE_PRIORITY
		{
			public D3D12_COMMAND_LIST_TYPE CommandListType;
			public int Priority;
			[MarshalAs (UnmanagedType.Bool)]
			public bool PriorityForTypeIsSupported;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_SERIALIZATION
		{
			public UINT NodeIndex;
			public D3D12_HEAP_SERIALIZATION_TIER HeapSerializationTier;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_CROSS_NODE
		{
			public D3D12_CROSS_NODE_SHARING_TIER SharingTier;
			[MarshalAs (UnmanagedType.Bool)]
			public bool AtomicShaderInstructions;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_PROTECTED_RESOURCE_SESSION_SUPPORT
		{
			public UINT NodeIndex;
			public D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAGS Support;
		}

		[StructLayout (LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_EXISTING_HEAPS
		{
			[MarshalAs (UnmanagedType.Bool)]
			public bool Supported;
		}
	}
}
