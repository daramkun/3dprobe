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
		 Guid("c4fec28f-7966-4e95-9f94-f431cb56c3b8"),
		 InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
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

		[Flags]
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

			D3D12_COMMAND_LIST_SUPPORT_FLAG_VIDEO_DECODE =
				(1 << D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_VIDEO_DECODE),

			D3D12_COMMAND_LIST_SUPPORT_FLAG_VIDEO_PROCESS =
				(1 << D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_VIDEO_PROCESS),

			D3D12_COMMAND_LIST_SUPPORT_FLAG_VIDEO_ENCODE =
				(1 << D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_VIDEO_ENCODE)
		}

		public enum D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER
		{
			D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_0 = 0,
			D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_1 = (D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_0 + 1),
			D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_2 = (D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_1 + 1),
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
			D3D12_RAYTRACING_TIER_1_0 = 10,
			D3D12_RAYTRACING_TIER_1_1 = 11
		}

		public enum D3D12_VARIABLE_SHADING_RATE_TIER
		{
			D3D12_VARIABLE_SHADING_RATE_TIER_NOT_SUPPORTED = 0,
			D3D12_VARIABLE_SHADING_RATE_TIER_1 = 1,
			D3D12_VARIABLE_SHADING_RATE_TIER_2 = 2
		}

		[Flags]
		public enum D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAGS
		{
			D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAG_NONE = 0,
			D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAG_SUPPORTED = 0x1
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS
		{
			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("64비트 부동소수점 셰이더 연산", null)]
			public bool DoublePrecisionFloatShaderOps;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("출력 병합기 논리 연산", null)]
			public bool OutputMergerLogicOp;

			[ItemDescription("지원하는 최소 정밀도", null)] public D3D12_SHADER_MIN_PRECISION_SUPPORT MinPrecisionSupport;
			[ItemDescription("타일 리소스 단계", null)] public D3D12_TILED_RESOURCES_TIER TiledResourcesTier;
			[ItemDescription("리소스 바인딩 단계", null)] public D3D12_RESOURCE_BINDING_TIER ResourceBindingTier;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("픽셀 셰이더의 지정된 스텐실 참조 지원", null)]
			public bool PSSpecifiedStencilRefSupported;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("타입이 지정된 정렬되지 않은 접근 뷰 추가 포맷 지원", null)]
			public bool TypedUAVLoadAdditionalFormats;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("정렬된 래스터라이저 뷰 지원", null)]
			public bool ROVsSupported;

			[ItemDescription("보수적 래스터라이제이션 단계", null)]
			public D3D12_CONSERVATIVE_RASTERIZATION_TIER ConservativeRasterizationTier;

			[ItemDescription("리소스 당 최대 GPU 가상 주소 비트 수", null)]
			public int MaxGPUVirtualAddressBitsPerResource;

			[MarshalAs(UnmanagedType.Bool)] public bool StandardSwizzle64KBSupported;
			public D3D12_CROSS_NODE_SHARING_TIER CrossNodeSharingTier;
			[MarshalAs(UnmanagedType.Bool)] public bool CrossAdapterRowMajorTextureSupported;

			[MarshalAs(UnmanagedType.Bool)]
			[ItemDescription("지오메트리 셰이더 에뮬레이션을 제외한 셰이더 피딩 래스터라이저 지원을 통한 뷰포트 및 렌더타겟 배열 색인", null)]
			public bool VPAndRTArrayIndexFromAnyShaderFeedingRasterizerSupportedWithoutGSEmulation;

			[ItemDescription("리소스 힙 단계", null)] public D3D12_RESOURCE_HEAP_TIER ResourceHeapTier;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS1
		{
			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("HLSL 6.0의 Wave 연산 지원", null)]
			public bool WaveOps;

			[ItemDescription("최소 Wave 레인 수", null)]
			public int WaveLaneCountMin;

			[ItemDescription("최대 Wave 레인 수", null)]
			public int WaveLaneCountMax;

			[ItemDescription("총 레인 수", null)] public int TotalLaneCount;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("확장된 컴퓨트 리소스 상태", null)]
			public bool ExpandedComputeResourceStates;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("64비트 정수 셰이더 연산", null)]
			public bool Int64ShaderOps;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS2
		{
			[MarshalAs(UnmanagedType.Bool)] public bool DepthBoundsTestSupported;
			public D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER ProgrammableSamplePositionsTier;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS3
		{
			[MarshalAs(UnmanagedType.Bool)] public bool CopyQueueTimestampQueriesSupported;
			[MarshalAs(UnmanagedType.Bool)] public bool CastingFullyTypedFormatSupported;
			public D3D12_COMMAND_LIST_SUPPORT_FLAGS WriteBufferImmediateSupportFlags;
			[ItemDescription ("뷰 인스턴싱 단계", null)]
			public D3D12_VIEW_INSTANCING_TIER ViewInstancingTier;
			[ItemDescription ("질량중심 지원", null)]
			public bool BarycentricsSupported;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS4
		{
			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("MSAA 64KB 정렬된 텍스처 지원", null)]
			public bool MSAA64KBAlignedTextureSupported;

			[ItemDescription("공유 메모리 호환성 단계", null)]
			public D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER SharedResourceCompatibilityTier;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("네이티브 16비트 셰이더 연산 지원", null)]
			public bool Native16BitShaderOpsSupported;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS5
		{
			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("셰이더 리소스 뷰의 타일 리소스 단계 3", null)]
			public bool SRVOnlyTiledResourceTier3;

			public D3D12_RENDER_PASS_TIER RenderPassesTier;
			[ItemDescription("레이트레이싱 단계", null)] public D3D12_RAYTRACING_TIER RaytracingTier;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_D3D12_OPTIONS6
		{
			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("추가적 셰이딩 단계 지원", null)]
			public bool AdditionalShadingRatesSupported;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("뷰포트 색인으로 지원하는 프리미티브별 셰이딩 비율", null)]
			public bool PerPrimitiveShadingRateSupportedWithViewportIndexing;

			[ItemDescription("가변 셰이딩 비율 단계", null)]
			public D3D12_VARIABLE_SHADING_RATE_TIER VariableShadingRateTier;

			[ItemDescription("셰이딩 비율 이미지 타일 크기", null)]
			public UINT ShadingRateImageTileSize;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("백그라운드 처리 지원", null)]
			public bool BackgroundProcessingSupported;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_ROOT_SIGNATURE
		{
			[ItemDescription("지원하는 가장 높은 버전", null)]
			public D3D_ROOT_SIGNATURE_VERSION HighestVersion;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_ARCHITECTURE
		{
			[ItemDescription("쿼리 할 어댑터 색인", null)] public int NodeIndex;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("타일 기반 렌더러", null)]
			public bool TileBasedRenderer;

			[MarshalAs(UnmanagedType.Bool)]
			[ItemDescription("통합 메모리 아키텍처", "통합 메모리 아키텍처를 통해 CPU와 GPU 간 데이터를 복사하지 않고 이용할 수 있습니다.")]
			public bool UMA;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("캐시 일관된 통합 메모리 아키텍처", null)]
			public bool CacheCoherentUMA;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_ARCHITECTURE1
		{
			[ItemDescription("쿼리 할 어댑터 색인", null)] public int NodeIndex;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("타일 기반 렌더러", null)]
			public bool TileBasedRenderer;

			[MarshalAs(UnmanagedType.Bool)]
			[ItemDescription("통합 메모리 아키텍처", "통합 메모리 아키텍처를 통해 CPU와 GPU 간 데이터를 복사하지 않고 이용할 수 있습니다.")]
			public bool UMA;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("캐시 일관된 통합 메모리 아키텍처", null)]
			public bool CacheCoherentUMA;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("독립된 메모리 관리 장치", null)]
			public bool IsolatedMMU;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_SHADER_MODEL
		{
			[ItemDescription("지원하는 최대 셰이더 모델", null)]
			public D3D_SHADER_MODEL HighestShaderModel;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_MULTISAMPLE_QUALITY_LEVELS
		{
			public DXGI_FORMAT Format;
			public int SampleCount;
			public D3D12_MULTISAMPLE_QUALITY_LEVEL_FLAGS Flags;
			public int NumQualityLevels;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_GPU_VIRTUAL_ADDRESS_SUPPORT
		{
			[ItemDescription("리소스 당 최대 GPU 가상 주소 비트 수", null)]
			public int MaxGPUVirtualAddressBitsPerResource;

			[ItemDescription("프로세스 당 최대 GPU 가상 주소 비트 수", null)]
			public int MaxGPUVirtualAddressBitsPerProcess;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_SHADER_CACHE
		{
			[ItemDescription("지원하는 셰이더 캐시 기능", null)]
			public D3D12_SHADER_CACHE_SUPPORT_FLAGS SupportFlags;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_COMMAND_QUEUE_PRIORITY
		{
			public D3D12_COMMAND_LIST_TYPE CommandListType;
			public int Priority;
			[MarshalAs(UnmanagedType.Bool)] public bool PriorityForTypeIsSupported;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_SERIALIZATION
		{
			[ItemDescription("쿼리 할 어댑터 색인", null)] public UINT NodeIndex;
			[ItemDescription("힙 직렬화 단계", null)] public D3D12_HEAP_SERIALIZATION_TIER HeapSerializationTier;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_CROSS_NODE
		{
			[ItemDescription("공유 단계", null)] public D3D12_CROSS_NODE_SHARING_TIER SharingTier;

			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("아토믹 셰이더 명령", null)]
			public bool AtomicShaderInstructions;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_PROTECTED_RESOURCE_SESSION_SUPPORT
		{
			[ItemDescription("쿼리 할 어댑터 색인", null)] public UINT NodeIndex;

			[ItemDescription("보호된 리소스 세션 지원", null)]
			public D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAGS Support;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct D3D12_FEATURE_DATA_EXISTING_HEAPS
		{
			[MarshalAs(UnmanagedType.Bool)] [ItemDescription("기존 방식 힙", null)]
			public bool Supported;
		}
	}
}
