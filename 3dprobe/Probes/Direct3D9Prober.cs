using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using static _3dprobe.Bridge.Direct3D9;

namespace _3dprobe.Probes
{
	public class Direct3D9Probe : IProbe
	{
		public string DeviceName { get; }

		[SectionName("일반")] public GeneralProbe General;
		[SectionName("능력1")] public Caps1Probe Caps1;
		[SectionName("능력2")] public Caps2Probe Caps2;
		[SectionName("능력3")] public Caps3Probe Caps3;
		[SectionName("장치능력1")] public DeviceCaps1Probe DeviceCaps1;
		[SectionName("장치능력2")] public DeviceCaps2Probe DeviceCaps2;
		[SectionName("프리미티브능력")] public PrimitiveMiscCapsProbe PrimitiveMiscCaps;
		[SectionName("셰이드능력")] public ShadeCapsProbe ShadeCaps;
		[SectionName("래스터능력")] public RasterCapsProbe RasterCaps;
		[SectionName("텍스처능력")] public TextureCapsProbe TextureCaps;
		[SectionName("셰이더")] public ShaderProbe Shader;
		[SectionName("커서")] public CursorProbe Cursor;
		[SectionName("갱신주기")] public PresentationIntervalsProbe PresentationIntervals;
		[SectionName("깊이 테스트")] public ZTestProbe ZTest;
		[SectionName("블렌드원본")] public BlendProbe SourceBlend;
		[SectionName("블렌드대상")] public BlendProbe DestinationBlend;
		[SectionName("알바비교")] public AlphaCompareProbe AlphaCompare;

		public Direct3D9Probe(string deviceName)
		{
			DeviceName = deviceName;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ShaderProbe
		{
			[ItemDescription("정점셰이더 지원", "정점셰이더 지원 여부")]
			public bool SupportVertexShader;

			[ItemDescription("픽셀셰이더 지원", "픽셀셰이더 지원 여부")]
			public bool SupportPixelShader;

			[ItemDescription("정점셰이더 버전", "최대 지원 정점셰이더 버전")]
			public Version VertexShaderVersion;

			[ItemDescription("정점셰이더 버전", "최대 지원 정점셰이더 버전")]
			public Version PixelShaderVersion;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct GeneralProbe
		{
			[ItemDescription("최대 텍스처 크기", "장치에서 지원하는 최대 텍스처 크기")]
			public Size MaximumTextureSize;

			[ItemDescription("최대 텍스처 블렌드 스테이지", "장치에서 지원하는 최대 텍스처 블렌드 스테이지 수")]
			public int MaximumTextureBlendStages;

			[ItemDescription("최대 동시 텍스처", "장치에서 지원하는 최대 동시 텍스처 수")]
			public int MaximumSimultaneousTextures;

			[ItemDescription("최대 이방성 레벨", "장치에서 지원하는 최대 이방성 레벨")]
			public int MaximumAnistropyLevel;

			[ItemDescription("최대 활성 광원", "장치에서 지원하는 최대 활성 광원 수")]
			public int MaximumActiveLights;

			[ItemDescription("최대 동시 데이터 스트림", "장치에서 지원하는 최대 동시 데이터 스트림 수")]
			public int MaximumConcurrentDataStreams;

			[ItemDescription("최대 스트림 스트라이드", "장치에서 지원하는 최대 스트림 스트라이드 수")]
			public int MaximumStreamStride;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Caps1Probe
		{
			[ItemDescription("오버레이", "오버레이 DDI 지원 여부")]
			public bool Overlay;

			[ItemDescription("주사선 읽기", "디스플레이의 현재 주사선 읽기 가능 여부")]
			public bool ReadScanline;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Caps2Probe
		{
			[ItemDescription("밉맵 자동 생성", "텍스처의 밉맵 자동 생성 가능 여부. 지원하지 않으면 밉맵 텍스처를 수동으로 생성해야 함.")]
			public bool AutoGenerationMipmap;

			[ItemDescription("감마 보정", "감마 보정 여부")] public bool CalibrateGamma;

			[ItemDescription("리소스 관리", "리소스 관리 여부")]
			public bool ManageResource;

			[ItemDescription("동적 텍스처", "동적 텍스처 지원 여부")]
			public bool DynamicTextures;

			[ItemDescription("전체화면 감마", "전체화면 감마 지원 여부")]
			public bool FullscreenGamma;

			[ItemDescription("셰이더 리소스", "셰이더 리소스 지원 여부")]
			public bool ShaderResource;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Caps3Probe
		{
			[ItemDescription("알파 전체화면 Flip 또는 Discard", null)]
			public bool AlphaFullscreenFlipOrDiscard;

			[ItemDescription("비디오 메모리로 복사", null)] public bool CopyToVideoMemory;
			[ItemDescription("시스템 메모리로 복사", null)] public bool CopyToSystemMemory;
			[ItemDescription("선형 sRGB 표시", null)] public bool LinearToSrgbPresentation;
			[ItemDescription("DXVA HD", null)] public bool DxvaHd;
			[ItemDescription("제한된 DXVA HD", null)] public bool DxvaHdLimited;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PresentationIntervalsProbe
		{
			[ItemDescription("즉시 갱신", "수직동기화 없이 렌더링한 화면을 바로 화면에 출력할 수 있습니다.")]
			public bool PresentIntervalImmediate;

			[ItemDescription("수직동기화 1배", "일반적으로 말하는 수직동기화입니다.")]
			public bool PresentInterval1x;

			[ItemDescription("수직동기화 2배", "수직동기화를 절반 수준의 속도로 실행합니다. 예를 들면 60Hz가 기본 수직동기화 주기라면 x2라면 30Hz로 실행됩니다.")]
			public bool PresentInterval2x;

			[ItemDescription("수직동기화 3배", "수직동기화를 1/3 수준의 속도로 실행합니다. 예를 들면 60Hz가 기본 수직동기화 주기라면 x3라면 20Hz로 실행됩니다.")]
			public bool PresentInterval3x;

			[ItemDescription("수직동기화 4배", "수직동기화를 1/4 수준의 속도로 실행합니다. 예를 들면 60Hz가 기본 수직동기화 주기라면 x4라면 15Hz로 실행됩니다.")]
			public bool PresentInterval4x;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CursorProbe
		{
			[ItemDescription("고해상도 풀컬러 커서", "고해상도에서 32비트 컬러의 커서를 사용할 수 있습니다.")]
			public bool HighResolutionFullColorCursor;

			[ItemDescription("고/저해상도 풀컬러 커서", "고해상도 저해상도 상관 없이 32비트 컬러의 커서를 사용할 수 있습니다.")]
			public bool HighAndLowResolutionFullColorCursor;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct DeviceCaps1Probe
		{
			[ItemDescription("시스템 -> 비로컬로 데이터 블릿", null)]
			public bool BlitSystemToNonLocalMemory;

			[ItemDescription("Flip 후 렌더", null)] public bool RenderAfterFlip;

			[ItemDescription("DrawPrimitive2", null)]
			public bool DrawPrimitive2;

			[ItemDescription("DrawPrimitive2Ex", null)]
			public bool DrawPrimitive2Ex;

			[ItemDescription("DrawPrimitive의 HAL 지원", null)]
			public bool HalSupportDrawPrimitive;

			[ItemDescription("시스템메모리 실행", null)] public bool ExecuteSystemMemory;
			[ItemDescription("비디오메모리 실행", null)] public bool ExecuteVideoMemory;
			[ItemDescription("하드웨어 래스터라이즈", null)] public bool HardwareRasterization;

			[ItemDescription("하드웨어 변환 및 라이팅", null)]
			public bool HardwareTransformAndLight;

			[ItemDescription("N-패치", null)] public bool NPatches;

			[ItemDescription("하드웨어 래스터라이즈/변환/라이팅/셰이딩", null)]
			public bool HardwareRasterizeTransformLightingShading;

			[ItemDescription(null, null)] public bool QuinticRectanglePatches;
			[ItemDescription(null, null)] public bool RectanglePatches;
			[ItemDescription(null, null)] public bool RectanglePatchHandleZero;
			[ItemDescription(null, null)] public bool SeparateTextureMemories;

			[ItemDescription("비로컬 비디오 메모리로부터 텍스처", null)]
			public bool TextureFromNonLocalVideoMemory;

			[ItemDescription("시스템 메모리로부터 텍스처", null)]
			public bool TextureFromSystemMemory;

			[ItemDescription("비디오 메모리로부터 텍스처", null)]
			public bool TextureFromVideoMemory;

			[ItemDescription("정점을 위한 시스템 메모리로부터 변환된 라이팅", null)]
			public bool TransformedLightForVertexFromSystemMemory;

			[ItemDescription("정점을 위한 비디오 메모리로부터 변환된 라이팅", null)]
			public bool TransformedLightForVertexFromVideoMemory;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct DeviceCaps2Probe
		{
			[ItemDescription("스트림 오프셋", null)] public bool StreamOffset;
			[ItemDescription(null, null)] public bool DisplacementMapNPatch;
			[ItemDescription("적응형 테셀레이션 RT-패치", null)] public bool AdaptiveTesselationRTPatch;
			[ItemDescription("적응형 테셀레이션 N-패치", null)] public bool AdaptiveTesselationNPatch;
			[ItemDescription("텍스처로부터 사각형 늘이기 가능", null)] public bool CanStretchRectangleFromTextures;
			[ItemDescription(null, null)] public bool PreSampledDisplacementMapNPatch;
			[ItemDescription("정점 요소의 스트림 오프셋 공유 가능", null)] public bool VertexElementsCanShareStreamOffset;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PrimitiveMiscCapsProbe
		{
			[ItemDescription("깊이 버퍼에 쓰기 활성화", null)]
			public bool DepthBufferWritingEnable;

			[ItemDescription("컬링 없음", null)] public bool NoCulling;
			[ItemDescription("시계방향 컬링", null)] public bool ClockwiseCulling;
			[ItemDescription("반시계방향 컬링", null)] public bool CounterClockwiseCulling;
			[ItemDescription("색상 쓰기 활성화", null)] public bool ColorWritingEnable;

			[ItemDescription("조정된 점 평면 클리핑", null)]
			public bool ClippingPlaneScaleEndPoints;

			[ItemDescription("변환된 정점 클리핑", null)] public bool ClippingTransformedLightedVertices;

			[ItemDescription("임시 레지스터를 위한 텍스처 매개변수 지원", null)]
			public bool D3DTASupportForTemporaryRegister;

			[ItemDescription("블렌드 연산", null)] public bool BlendOperation;

			[ItemDescription("렌더링하지 않는 레퍼런스 장치", null)]
			public bool NullReference;

			[ItemDescription("독립된 쓰기 마스크", null)] public bool IndependentWriteMasks;
			[ItemDescription("스테이지별 상수", null)] public bool PerStageConstant;
			[ItemDescription("안개 및 반사광 알파", null)] public bool FogAndSpecularAlpha;
			[ItemDescription("분리된 알파블렌드", null)] public bool SeparateAlphaBlend;

			[ItemDescription("다수의 렌더타겟 독립 비트깊이", null)]
			public bool MultipleRenderTargetIndependentBitDepth;

			[ItemDescription("다수의 렌더타겟 픽셀셰이더 후 블렌딩", null)]
			public bool MultipleRenderTargetPostPixelShaderBlending;

			[ItemDescription("안개 정점 고정", null)] public bool FogVertexClamped;

			[ItemDescription("블렌드 후 sRGB 변환", null)]
			public bool PostBlendSrgbConvert;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RasterCapsProbe
		{
			[ItemDescription("이방성 필터링", null)] public bool AnisotropyFiltering;
			[ItemDescription("색상 원근", null)] public bool ColorPerspective;
			[ItemDescription("디더링", null)] public bool Dithering;
			[ItemDescription("레거시 깊이 바이어스", null)] public bool DepthBias;
			[ItemDescription("안개 범위", null)] public bool FogRange;
			[ItemDescription("안개 테이블", null)] public bool FogTable;
			[ItemDescription("안개 정점", null)] public bool FogVertex;
			[ItemDescription("밉맵 LOD 바이어스", null)] public bool MipmapLODBias;
			[ItemDescription("멀티샘플 토글", null)] public bool MultisampleToggle;
			[ItemDescription("시저 테스트", null)] public bool ScissorTest;

			[ItemDescription("슬로프 스케일 기반 깊이 바이어스", null)]
			public bool SlopeScaleDepthBias;

			[ItemDescription("W 기반 깊이 버퍼", null)] public bool WBaseDepthBuffer;
			[ItemDescription("W 기반 안개", null)] public bool WBaseFog;
			[ItemDescription("Z 버퍼 없이 HRS", null)] public bool ZBufferlessHRS;
			[ItemDescription("Z 기반 안개", null)] public bool ZBaseFog;
			[ItemDescription("Z 테스트", null)] public bool ZTest;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ShadeCapsProbe
		{
			[ItemDescription("알파 고라우드 블렌드", null)] public bool AlphaGouraudBlend;
			[ItemDescription("컬러 고라우드 RGB", null)] public bool ColorGouraudRGB;
			[ItemDescription("안개 고라우드", null)] public bool FogGouraud;

			[ItemDescription("반사광 고라우드 RGB", null)]
			public bool SpecularGouraudRGB;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TextureCapsProbe
		{
			[ItemDescription("텍스처 알파 지원", null)] public bool AlphaInTexture;

			[ItemDescription("텍스처 알파 팔레트 지원", null)]
			public bool AlphaPaletteInTexture;

			[ItemDescription("조건부 비승수 크기 텍스처", null)]
			public bool ConditionalNonePowerSizeTexture;

			[ItemDescription("반드시 승수 크기 텍스처", null)]
			public bool TextureSizeMustPower;

			[ItemDescription(null, null)] public bool TextureSizeSquareOnly;
			[ItemDescription("큐브 텍스처 지원", null)] public bool SupportCubeTexture;
			[ItemDescription(null, null)] public bool CubeTextureSizeMustPower;
			[ItemDescription("볼륨 텍스처 지원", null)] public bool SupportVolumeTexture;
			[ItemDescription(null, null)] public bool VolumeTextureSizeMustPower;
			[ItemDescription("밉맵 지원", null)] public bool SupportMipmap;

			[ItemDescription("큐브 텍스처에서의 밉맵 지원", null)]
			public bool SupportMipmapOnCubeTexture;

			[ItemDescription("볼륨 텍스처에서의 밉맵 지원", null)]
			public bool SupportMipmapOnVolumeTexture;

			[ItemDescription(null, null)] public bool NoProjectedBumpEnvironmentLookup;
			[ItemDescription(null, null)] public bool PerspectiveCorrection;

			[ItemDescription("D3DTTFF_PROJECTED 플래그", null)]
			public bool D3DTTFF_PROJECTEDFlag;

			[ItemDescription(null, null)] public bool TextureRepeatNotScaledBySize;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ZTestProbe
		{
			[ItemDescription("항상 통과", null)] public bool PassAlways;
			[ItemDescription("통과 불가", null)] public bool NeverPass;
			[ItemDescription("같을 때 통과", null)] public bool PassEqual;
			[ItemDescription("다를 때 통과", null)] public bool PassNotEqual;
			[ItemDescription("클 때 통과", null)] public bool PassGreater;
			[ItemDescription("크거나 같을 때 통과", null)] public bool PassGreaterEqual;
			[ItemDescription("작을 때 통과", null)] public bool PassLess;
			[ItemDescription("작거나 같을 때 통과", null)] public bool PassLessEqual;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct BlendProbe
		{
			[ItemDescription("블렌드 계수", null)] public bool BlendFactor;
			[ItemDescription("블렌드 피연산자 - 0", null)] public bool BlendOperandZero;
			[ItemDescription("블렌드 피연산자 - 1", null)] public bool BlendOperandOne;
			[ItemDescription("블렌드 피연산자 - 원본 색상", null)] public bool BlendOperandSourceColor;
			[ItemDescription("블렌드 피연산자 - 반전된 원본 색상", null)] public bool BlendOperandInvertedSourceColor;
			[ItemDescription("블렌드 피연산자 - 원본 투명도", null)] public bool BlendOperandSourceAlpha;
			[ItemDescription("블렌드 피연산자 - 반전된 원본 투명도", null)] public bool BlendOperandInvertedSourceAlpha;
			[ItemDescription("블렌드 피연산자 - 대상 투명도", null)] public bool BlendOperandDestinationAlpha;
			[ItemDescription("블렌드 피연산자 - 반전된 대상 투명도", null)] public bool BlendOperandInvertedDestinationAlpha;
			[ItemDescription("블렌드 피연산자 - 대상 색상", null)] public bool BlendOperandDestinationColor;
			[ItemDescription("블렌드 피연산자 - 반전된 대상 색상", null)] public bool BlendOperandInvertedDestinationColor;
			[ItemDescription("블렌드 피연산자 - 범위 내 원본 투명도", null)] public bool BlendOperandSourceAlphaSaturate;
			[ItemDescription("블렌드 피연산자 - 양쪽 원본 투명도", null)] public bool BlendOperandBothSourceAlpha;
			[ItemDescription("블렌드 피연산자 - 양쪽 반전된 양쪽 투명도", null)] public bool BlendOperandBothInvertedSourceAlpha;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct AlphaCompareProbe
		{
			[ItemDescription("투명도 비교 지원", null)] public bool SupportAlphaCompare;
			[ItemDescription("투명도 비교 - 항상", null)] public bool AlphaCompareAlways;
			[ItemDescription("투명도 비교 - 절대", null)] public bool AlphaCompareNever;
			[ItemDescription("투명도 비교 - 같음", null)] public bool AlphaCompareEqual;
			[ItemDescription("투명도 비교 - 같지 않음", null)] public bool AlphaCompareNotEqual;
			[ItemDescription("투명도 비교 - 작음", null)] public bool AlphaCompareLess;
			[ItemDescription("투명도 비교 - 작거나 같음", null)] public bool AlphaCompareLessEqual;
			[ItemDescription("투명도 비교 - 큼", null)] public bool AlphaCompareGreater;
			[ItemDescription("투명도 비교 - 크거나 같음", null)] public bool AlphaCompareGreaterEqual;
		}
	}

	public sealed class Direct3D9Prober : IProber<Direct3D9Probe>
	{
		private IDirect3D9Ex _d3d9;

		public Direct3D9Prober ()
		{
			try
			{
				if (Direct3DCreate9Ex(D3D_SDK_VERSION, out _d3d9) != 0)
					throw new Exception();
			}
			catch
			{
				Debug.WriteLine ("Direct3D9 unsupported platform.");
			}
		}

		public void Dispose ()
		{
			if (_d3d9 != null)
				Marshal.ReleaseComObject(_d3d9);
			_d3d9 = null;
		}

		public string Name => "Direct3D 9";

		IEnumerable IProber.Probes => Probes;

		public IEnumerable<Direct3D9Probe> Probes
		{
			get
			{
				if (_d3d9 == null)
					yield break;

				var i = -1;
				for (; ; ++i)
				{
					Direct3D9Probe probe;

					D3DCAPS9 caps;
					var hr = i == -1
						? _d3d9.GetDeviceCaps (0, D3DDEVTYPE.REF, out caps)
						: _d3d9.GetDeviceCaps ((uint) i, D3DDEVTYPE.HAL, out caps);

					if (hr < 0 && hr != unchecked((int) D3DERR_INVALIDDEVICE))
						break;
					if (hr == unchecked((int) D3DERR_INVALIDDEVICE))
						continue;

					if (i != -1)
					{
						hr = _d3d9.GetAdapterIdentifier ((uint) (i == -1 ? 0 : i), 0, out var identifier);
						if (hr < 0)
							break;
						probe = new Direct3D9Probe (identifier.Description);
					}
					else
						probe = new Direct3D9Probe ("Microsoft Direct3D 9 Reference Adapter");

					probe.Shader.SupportVertexShader = (caps.VertexShaderVersion & 0xFFFE0000) == 0xFFFE0000;
					probe.Shader.SupportPixelShader = (caps.PixelShaderVersion & 0xFFFE0000) == 0xFFFE0000;
					probe.Shader.VertexShaderVersion = new Version ((int) (caps.VertexShaderVersion & 0xff00) >> 8,
						(int) caps.VertexShaderVersion & 0xff);
					probe.Shader.PixelShaderVersion = new Version ((int) (caps.PixelShaderVersion & 0xff00) >> 8,
						(int) caps.PixelShaderVersion & 0xff);

					probe.General.MaximumTextureSize =
						new Size ((int) caps.MaxTextureWidth, (int) caps.MaxTextureHeight);
					probe.General.MaximumTextureBlendStages = (int) caps.MaxTextureBlendStages;
					probe.General.MaximumSimultaneousTextures = (int) caps.MaxSimultaneousTextures;
					probe.General.MaximumAnistropyLevel = (int) caps.MaxAnisotropy;
					probe.General.MaximumActiveLights = (int) caps.MaxActiveLights;
					probe.General.MaximumConcurrentDataStreams = (int) caps.MaxStreams;
					probe.General.MaximumStreamStride = (int) caps.MaxStreamStride;

					probe.Caps1.Overlay = (caps.Caps & D3DCAPS_OVERLAY) != 0;
					probe.Caps1.ReadScanline = (caps.Caps & D3DCAPS_READ_SCANLINE) != 0;

					probe.Caps2.AutoGenerationMipmap = (caps.Caps2 & D3DCAPS2_CANAUTOGENMIPMAP) != 0;
					probe.Caps2.CalibrateGamma = (caps.Caps2 & D3DCAPS2_CANCALIBRATEGAMMA) != 0;
					probe.Caps2.ManageResource = (caps.Caps2 & D3DCAPS2_CANMANAGERESOURCE) != 0;
					probe.Caps2.DynamicTextures = (caps.Caps2 & D3DCAPS2_DYNAMICTEXTURES) != 0;
					probe.Caps2.FullscreenGamma = (caps.Caps2 & D3DCAPS2_FULLSCREENGAMMA) != 0;
					probe.Caps2.ShaderResource = (caps.Caps2 & D3DCAPS2_CANSHARERESOURCE) != 0;

					probe.Caps3.AlphaFullscreenFlipOrDiscard =
						(caps.Caps3 & D3DCAPS3_ALPHA_FULLSCREEN_FLIP_OR_DISCARD) != 0;
					probe.Caps3.CopyToVideoMemory = (caps.Caps3 & D3DCAPS3_COPY_TO_VIDMEM) != 0;
					probe.Caps3.CopyToSystemMemory = (caps.Caps3 & D3DCAPS3_COPY_TO_SYSTEMMEM) != 0;
					probe.Caps3.LinearToSrgbPresentation = (caps.Caps3 & D3DCAPS3_LINEAR_TO_SRGB_PRESENTATION) != 0;
					probe.Caps3.DxvaHd = (caps.Caps3 & D3DCAPS3_DXVAHD) != 0;
					probe.Caps3.DxvaHdLimited = (caps.Caps3 & D3DCAPS3_DXVAHD_LIMITED) != 0;

					probe.PresentationIntervals.PresentIntervalImmediate =
						(caps.PresentationIntervals & D3DPRESENT_INTERVAL_IMMEDIATE) != 0;
					probe.PresentationIntervals.PresentInterval1x =
						(caps.PresentationIntervals & D3DPRESENT_INTERVAL_ONE) != 0;
					probe.PresentationIntervals.PresentInterval2x =
						(caps.PresentationIntervals & D3DPRESENT_INTERVAL_TWO) != 0;
					probe.PresentationIntervals.PresentInterval3x =
						(caps.PresentationIntervals & D3DPRESENT_INTERVAL_THREE) != 0;
					probe.PresentationIntervals.PresentInterval4x =
						(caps.PresentationIntervals & D3DPRESENT_INTERVAL_FOUR) != 0;

					probe.Cursor.HighResolutionFullColorCursor = (caps.CursorCaps & D3DCURSORCAPS_COLOR) != 0;
					probe.Cursor.HighAndLowResolutionFullColorCursor = (caps.CursorCaps & D3DCURSORCAPS_LOWRES) != 0;

					probe.DeviceCaps1.BlitSystemToNonLocalMemory = (caps.DevCaps & D3DDEVCAPS_CANBLTSYSTONONLOCAL) != 0;
					probe.DeviceCaps1.RenderAfterFlip = (caps.DevCaps & D3DDEVCAPS_CANRENDERAFTERFLIP) != 0;
					probe.DeviceCaps1.DrawPrimitive2 = (caps.DevCaps & D3DDEVCAPS_DRAWPRIMITIVES2) != 0;
					probe.DeviceCaps1.DrawPrimitive2Ex = (caps.DevCaps & D3DDEVCAPS_DRAWPRIMITIVES2EX) != 0;
					probe.DeviceCaps1.HalSupportDrawPrimitive = (caps.DevCaps & D3DDEVCAPS_DRAWPRIMTLVERTEX) != 0;
					probe.DeviceCaps1.ExecuteSystemMemory = (caps.DevCaps & D3DDEVCAPS_EXECUTESYSTEMMEMORY) != 0;
					probe.DeviceCaps1.ExecuteVideoMemory = (caps.DevCaps & D3DDEVCAPS_EXECUTEVIDEOMEMORY) != 0;
					probe.DeviceCaps1.HardwareRasterization = (caps.DevCaps & D3DDEVCAPS_HWRASTERIZATION) != 0;
					probe.DeviceCaps1.HardwareTransformAndLight = (caps.DevCaps & D3DDEVCAPS_HWTRANSFORMANDLIGHT) != 0;
					probe.DeviceCaps1.NPatches = (caps.DevCaps & D3DDEVCAPS_NPATCHES) != 0;
					probe.DeviceCaps1.HardwareRasterizeTransformLightingShading =
						(caps.DevCaps & D3DDEVCAPS_PUREDEVICE) != 0;
					probe.DeviceCaps1.QuinticRectanglePatches = (caps.DevCaps & D3DDEVCAPS_QUINTICRTPATCHES) != 0;
					probe.DeviceCaps1.RectanglePatches = (caps.DevCaps & D3DDEVCAPS_RTPATCHES) != 0;
					probe.DeviceCaps1.RectanglePatchHandleZero = (caps.DevCaps & D3DDEVCAPS_RTPATCHHANDLEZERO) != 0;
					probe.DeviceCaps1.SeparateTextureMemories =
						(caps.DevCaps & D3DDEVCAPS_SEPARATETEXTUREMEMORIES) != 0;
					probe.DeviceCaps1.TextureFromNonLocalVideoMemory =
						(caps.DevCaps & D3DDEVCAPS_TEXTURENONLOCALVIDMEM) != 0;
					probe.DeviceCaps1.TextureFromSystemMemory = (caps.DevCaps & D3DDEVCAPS_TEXTURESYSTEMMEMORY) != 0;
					probe.DeviceCaps1.TextureFromVideoMemory = (caps.DevCaps & D3DDEVCAPS_TEXTUREVIDEOMEMORY) != 0;
					probe.DeviceCaps1.TransformedLightForVertexFromSystemMemory =
						(caps.DevCaps & D3DDEVCAPS_TLVERTEXSYSTEMMEMORY) != 0;
					probe.DeviceCaps1.TransformedLightForVertexFromVideoMemory =
						(caps.DevCaps & D3DDEVCAPS_TLVERTEXVIDEOMEMORY) != 0;

					probe.DeviceCaps2.StreamOffset = (caps.DevCaps2 & D3DDEVCAPS2_STREAMOFFSET) != 0;
					probe.DeviceCaps2.DisplacementMapNPatch = (caps.DevCaps2 & D3DDEVCAPS2_DMAPNPATCH) != 0;
					probe.DeviceCaps2.AdaptiveTesselationRTPatch =
						(caps.DevCaps2 & D3DDEVCAPS2_ADAPTIVETESSRTPATCH) != 0;
					probe.DeviceCaps2.AdaptiveTesselationNPatch = (caps.DevCaps2 & D3DDEVCAPS2_ADAPTIVETESSNPATCH) != 0;
					probe.DeviceCaps2.CanStretchRectangleFromTextures =
						(caps.DevCaps2 & D3DDEVCAPS2_CAN_STRETCHRECT_FROM_TEXTURES) != 0;
					probe.DeviceCaps2.PreSampledDisplacementMapNPatch =
						(caps.DevCaps2 & D3DDEVCAPS2_PRESAMPLEDDMAPNPATCH) != 0;
					probe.DeviceCaps2.VertexElementsCanShareStreamOffset =
						(caps.DevCaps2 & D3DDEVCAPS2_VERTEXELEMENTSCANSHARESTREAMOFFSET) != 0;

					probe.PrimitiveMiscCaps.DepthBufferWritingEnable =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_MASKZ) != 0;
					probe.PrimitiveMiscCaps.NoCulling = (caps.PrimitiveMiscCaps & D3DPMISCCAPS_CULLNONE) != 0;
					probe.PrimitiveMiscCaps.ClockwiseCulling = (caps.PrimitiveMiscCaps & D3DPMISCCAPS_CULLCW) != 0;
					probe.PrimitiveMiscCaps.CounterClockwiseCulling =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_CULLCCW) != 0;
					probe.PrimitiveMiscCaps.ColorWritingEnable =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_COLORWRITEENABLE) != 0;
					probe.PrimitiveMiscCaps.ClippingPlaneScaleEndPoints =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_CLIPPLANESCALEDPOINTS) != 0;
					probe.PrimitiveMiscCaps.ClippingTransformedLightedVertices =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_CLIPTLVERTS) != 0;
					probe.PrimitiveMiscCaps.D3DTASupportForTemporaryRegister =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_TSSARGTEMP) != 0;
					probe.PrimitiveMiscCaps.BlendOperation = (caps.PrimitiveMiscCaps & D3DPMISCCAPS_BLENDOP) != 0;
					probe.PrimitiveMiscCaps.NullReference = (caps.PrimitiveMiscCaps & D3DPMISCCAPS_NULLREFERENCE) != 0;
					probe.PrimitiveMiscCaps.IndependentWriteMasks =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_INDEPENDENTWRITEMASKS) != 0;
					probe.PrimitiveMiscCaps.PerStageConstant =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_PERSTAGECONSTANT) != 0;
					probe.PrimitiveMiscCaps.FogAndSpecularAlpha =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_FOGANDSPECULARALPHA) != 0;
					probe.PrimitiveMiscCaps.SeparateAlphaBlend =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_SEPARATEALPHABLEND) != 0;
					probe.PrimitiveMiscCaps.MultipleRenderTargetIndependentBitDepth =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_MRTINDEPENDENTBITDEPTHS) != 0;
					probe.PrimitiveMiscCaps.MultipleRenderTargetPostPixelShaderBlending =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_MRTPOSTPIXELSHADERBLENDING) != 0;
					probe.PrimitiveMiscCaps.FogVertexClamped =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_FOGVERTEXCLAMPED) != 0;
					probe.PrimitiveMiscCaps.PostBlendSrgbConvert =
						(caps.PrimitiveMiscCaps & D3DPMISCCAPS_POSTBLENDSRGBCONVERT) != 0;

					probe.RasterCaps.AnisotropyFiltering = (caps.RasterCaps & D3DPRASTERCAPS_ANISOTROPY) != 0;
					probe.RasterCaps.ColorPerspective = (caps.RasterCaps & D3DPRASTERCAPS_COLORPERSPECTIVE) != 0;
					probe.RasterCaps.Dithering = (caps.RasterCaps & D3DPRASTERCAPS_DITHER) != 0;
					probe.RasterCaps.DepthBias = (caps.RasterCaps & D3DPRASTERCAPS_DEPTHBIAS) != 0;
					probe.RasterCaps.FogRange = (caps.RasterCaps & D3DPRASTERCAPS_FOGRANGE) != 0;
					probe.RasterCaps.FogTable = (caps.RasterCaps & D3DPRASTERCAPS_FOGTABLE) != 0;
					probe.RasterCaps.FogVertex = (caps.RasterCaps & D3DPRASTERCAPS_FOGVERTEX) != 0;
					probe.RasterCaps.MipmapLODBias = (caps.RasterCaps & D3DPRASTERCAPS_MIPMAPLODBIAS) != 0;
					probe.RasterCaps.MultisampleToggle = (caps.RasterCaps & D3DPRASTERCAPS_MULTISAMPLE_TOGGLE) != 0;
					probe.RasterCaps.ScissorTest = (caps.RasterCaps & D3DPRASTERCAPS_SCISSORTEST) != 0;
					probe.RasterCaps.SlopeScaleDepthBias = (caps.RasterCaps & D3DPRASTERCAPS_SLOPESCALEDEPTHBIAS) != 0;
					probe.RasterCaps.WBaseDepthBuffer = (caps.RasterCaps & D3DPRASTERCAPS_WBUFFER) != 0;
					probe.RasterCaps.WBaseFog = (caps.RasterCaps & D3DPRASTERCAPS_WFOG) != 0;
					probe.RasterCaps.ZBufferlessHRS = (caps.RasterCaps & D3DPRASTERCAPS_ZBUFFERLESSHSR) != 0;
					probe.RasterCaps.ZBaseFog = (caps.RasterCaps & D3DPRASTERCAPS_ZFOG) != 0;
					probe.RasterCaps.ZTest = (caps.RasterCaps & D3DPRASTERCAPS_ZTEST) != 0;

					probe.ShadeCaps.AlphaGouraudBlend = (caps.ShadeCaps & D3DPSHADECAPS_ALPHAGOURAUDBLEND) != 0;
					probe.ShadeCaps.ColorGouraudRGB = (caps.ShadeCaps & D3DPSHADECAPS_COLORGOURAUDRGB) != 0;
					probe.ShadeCaps.FogGouraud = (caps.ShadeCaps & D3DPSHADECAPS_FOGGOURAUD) != 0;
					probe.ShadeCaps.SpecularGouraudRGB = (caps.ShadeCaps & D3DPSHADECAPS_SPECULARGOURAUDRGB) != 0;

					probe.TextureCaps.AlphaInTexture = (caps.TextureCaps & D3DPTEXTURECAPS_ALPHA) != 0;
					probe.TextureCaps.AlphaPaletteInTexture = (caps.TextureCaps & D3DPTEXTURECAPS_ALPHAPALETTE) != 0;
					probe.TextureCaps.ConditionalNonePowerSizeTexture =
						(caps.TextureCaps & D3DPTEXTURECAPS_NONPOW2CONDITIONAL) != 0;
					probe.TextureCaps.TextureSizeMustPower = (caps.TextureCaps & D3DPTEXTURECAPS_POW2) != 0;
					probe.TextureCaps.TextureSizeSquareOnly = (caps.TextureCaps & D3DPTEXTURECAPS_SQUAREONLY) != 0;
					probe.TextureCaps.SupportCubeTexture = (caps.TextureCaps & D3DPTEXTURECAPS_CUBEMAP) != 0;
					probe.TextureCaps.CubeTextureSizeMustPower = (caps.TextureCaps & D3DPTEXTURECAPS_CUBEMAP_POW2) != 0;
					probe.TextureCaps.SupportVolumeTexture = (caps.TextureCaps & D3DPTEXTURECAPS_VOLUMEMAP) != 0;
					probe.TextureCaps.VolumeTextureSizeMustPower =
						(caps.TextureCaps & D3DPTEXTURECAPS_VOLUMEMAP_POW2) != 0;
					probe.TextureCaps.SupportMipmap = (caps.TextureCaps & D3DPTEXTURECAPS_MIPMAP) != 0;
					probe.TextureCaps.SupportMipmapOnCubeTexture = (caps.TextureCaps & D3DPTEXTURECAPS_MIPCUBEMAP) != 0;
					probe.TextureCaps.SupportMipmapOnVolumeTexture =
						(caps.TextureCaps & D3DPTEXTURECAPS_MIPVOLUMEMAP) != 0;
					probe.TextureCaps.NoProjectedBumpEnvironmentLookup =
						(caps.TextureCaps & D3DPTEXTURECAPS_NOPROJECTEDBUMPENV) != 0;
					probe.TextureCaps.PerspectiveCorrection = (caps.TextureCaps & D3DPTEXTURECAPS_PERSPECTIVE) != 0;
					probe.TextureCaps.D3DTTFF_PROJECTEDFlag = (caps.TextureCaps & D3DPTEXTURECAPS_PROJECTED) != 0;
					probe.TextureCaps.TextureRepeatNotScaledBySize =
						(caps.TextureCaps & D3DPTEXTURECAPS_TEXREPEATNOTSCALEDBYSIZE) != 0;

					probe.ZTest.PassAlways = (caps.ZCmpCaps & D3DPCMPCAPS_ALWAYS) != 0;
					probe.ZTest.NeverPass = (caps.ZCmpCaps & D3DPCMPCAPS_NEVER) != 0;
					probe.ZTest.PassEqual = (caps.ZCmpCaps & D3DPCMPCAPS_EQUAL) != 0;
					probe.ZTest.PassNotEqual = (caps.ZCmpCaps & D3DPCMPCAPS_NOTEQUAL) != 0;
					probe.ZTest.PassGreater = (caps.ZCmpCaps & D3DPCMPCAPS_GREATER) != 0;
					probe.ZTest.PassGreaterEqual = (caps.ZCmpCaps & D3DPCMPCAPS_GREATEREQUAL) != 0;
					probe.ZTest.PassLess = (caps.ZCmpCaps & D3DPCMPCAPS_LESS) != 0;
					probe.ZTest.PassLessEqual = (caps.ZCmpCaps & D3DPCMPCAPS_LESSEQUAL) != 0;

					probe.SourceBlend.BlendFactor = (caps.SrcBlendCaps & D3DPBLENDCAPS_BLENDFACTOR) != 0;
					probe.SourceBlend.BlendOperandZero = (caps.SrcBlendCaps & D3DPBLENDCAPS_ZERO) != 0;
					probe.SourceBlend.BlendOperandOne = (caps.SrcBlendCaps & D3DPBLENDCAPS_ONE) != 0;
					probe.SourceBlend.BlendOperandSourceColor = (caps.SrcBlendCaps & D3DPBLENDCAPS_SRCCOLOR) != 0;
					probe.SourceBlend.BlendOperandInvertedSourceColor =
						(caps.SrcBlendCaps & D3DPBLENDCAPS_INVSRCCOLOR) != 0;
					probe.SourceBlend.BlendOperandSourceAlpha = (caps.SrcBlendCaps & D3DPBLENDCAPS_SRCALPHA) != 0;
					probe.SourceBlend.BlendOperandInvertedSourceAlpha =
						(caps.SrcBlendCaps & D3DPBLENDCAPS_INVSRCALPHA) != 0;
					probe.SourceBlend.BlendOperandDestinationAlpha = (caps.SrcBlendCaps & D3DPBLENDCAPS_DESTALPHA) != 0;
					probe.SourceBlend.BlendOperandInvertedDestinationAlpha =
						(caps.SrcBlendCaps & D3DPBLENDCAPS_INVDESTALPHA) != 0;
					probe.SourceBlend.BlendOperandDestinationColor = (caps.SrcBlendCaps & D3DPBLENDCAPS_DESTCOLOR) != 0;
					probe.SourceBlend.BlendOperandInvertedDestinationColor =
						(caps.SrcBlendCaps & D3DPBLENDCAPS_INVDESTCOLOR) != 0;
					probe.SourceBlend.BlendOperandSourceAlphaSaturate =
						(caps.SrcBlendCaps & D3DPBLENDCAPS_SRCALPHASAT) != 0;
					probe.SourceBlend.BlendOperandBothSourceAlpha =
						(caps.SrcBlendCaps & D3DPBLENDCAPS_BOTHSRCALPHA) != 0;
					probe.SourceBlend.BlendOperandBothInvertedSourceAlpha =
						(caps.SrcBlendCaps & D3DPBLENDCAPS_BOTHINVSRCALPHA) != 0;

					probe.DestinationBlend.BlendFactor = (caps.DestBlendCaps & D3DPBLENDCAPS_BLENDFACTOR) != 0;
					probe.DestinationBlend.BlendOperandZero = (caps.DestBlendCaps & D3DPBLENDCAPS_ZERO) != 0;
					probe.DestinationBlend.BlendOperandOne = (caps.DestBlendCaps & D3DPBLENDCAPS_ONE) != 0;
					probe.DestinationBlend.BlendOperandSourceColor = (caps.DestBlendCaps & D3DPBLENDCAPS_SRCCOLOR) != 0;
					probe.DestinationBlend.BlendOperandInvertedSourceColor =
						(caps.DestBlendCaps & D3DPBLENDCAPS_INVSRCCOLOR) != 0;
					probe.DestinationBlend.BlendOperandSourceAlpha = (caps.DestBlendCaps & D3DPBLENDCAPS_SRCALPHA) != 0;
					probe.DestinationBlend.BlendOperandInvertedSourceAlpha =
						(caps.DestBlendCaps & D3DPBLENDCAPS_INVSRCALPHA) != 0;
					probe.DestinationBlend.BlendOperandDestinationAlpha =
						(caps.DestBlendCaps & D3DPBLENDCAPS_DESTALPHA) != 0;
					probe.DestinationBlend.BlendOperandInvertedDestinationAlpha =
						(caps.DestBlendCaps & D3DPBLENDCAPS_INVDESTALPHA) != 0;
					probe.DestinationBlend.BlendOperandDestinationColor =
						(caps.DestBlendCaps & D3DPBLENDCAPS_DESTCOLOR) != 0;
					probe.DestinationBlend.BlendOperandInvertedDestinationColor =
						(caps.DestBlendCaps & D3DPBLENDCAPS_INVDESTCOLOR) != 0;
					probe.DestinationBlend.BlendOperandSourceAlphaSaturate =
						(caps.DestBlendCaps & D3DPBLENDCAPS_SRCALPHASAT) != 0;
					probe.DestinationBlend.BlendOperandBothSourceAlpha =
						(caps.DestBlendCaps & D3DPBLENDCAPS_BOTHSRCALPHA) != 0;
					probe.DestinationBlend.BlendOperandBothInvertedSourceAlpha =
						(caps.DestBlendCaps & D3DPBLENDCAPS_BOTHINVSRCALPHA) != 0;

					probe.AlphaCompare.SupportAlphaCompare =
						!(caps.AlphaCmpCaps == D3DPCMPCAPS_ALWAYS || caps.AlphaCmpCaps == D3DPCMPCAPS_NEVER);
					probe.AlphaCompare.AlphaCompareAlways = (caps.AlphaCmpCaps & D3DPCMPCAPS_ALWAYS) != 0;
					probe.AlphaCompare.AlphaCompareNever = (caps.AlphaCmpCaps & D3DPCMPCAPS_NEVER) != 0;
					probe.AlphaCompare.AlphaCompareEqual = (caps.AlphaCmpCaps & D3DPCMPCAPS_EQUAL) != 0;
					probe.AlphaCompare.AlphaCompareNotEqual = (caps.AlphaCmpCaps & D3DPCMPCAPS_NOTEQUAL) != 0;
					probe.AlphaCompare.AlphaCompareLess = (caps.AlphaCmpCaps & D3DPCMPCAPS_LESS) != 0;
					probe.AlphaCompare.AlphaCompareLessEqual = (caps.AlphaCmpCaps & D3DPCMPCAPS_LESSEQUAL) != 0;
					probe.AlphaCompare.AlphaCompareGreater = (caps.AlphaCmpCaps & D3DPCMPCAPS_GREATER) != 0;
					probe.AlphaCompare.AlphaCompareGreaterEqual = (caps.AlphaCmpCaps & D3DPCMPCAPS_GREATEREQUAL) != 0;

					yield return probe;
				}
			}
		}
	}
}
