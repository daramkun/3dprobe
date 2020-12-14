using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using _3dprobe.Bridge;
using static _3dprobe.Bridge.DXGI;
using static _3dprobe.Bridge.Direct3D11;

namespace _3dprobe.Probes
{
	public class D3D11Probe : IProbe
	{
		public string DeviceName { get; }

		[SectionName("기능레벨")] public FeatureLevelProbe FeatureLevel;
		[SectionName("멀티스레드")] public D3D11_FEATURE_DATA_THREADING Threading;
		[SectionName("64비트 부동소수점")] public D3D11_FEATURE_DATA_DOUBLES Doubles;
		[SectionName("Direct3D 10 하드웨어 옵션")] public D3D11_FEATURE_DATA_D3D10_X_HARDWARE_OPTIONS D3D10HardwareOptions;
		[SectionName("Direct3D 11 옵션")] public D3D11_FEATURE_DATA_D3D11_OPTIONS D3D11Options;
		[SectionName("아키텍처")] public D3D11_FEATURE_DATA_ARCHITECTURE_INFO Architecture;
		[SectionName("Direct3D 9 옵션")] public D3D11_FEATURE_DATA_D3D9_OPTIONS D3D9Option;
		[SectionName("Direct3D 9 그림자 지원")] public D3D11_FEATURE_DATA_D3D9_SHADOW_SUPPORT D3D9ShadowSupport;

		[SectionName("셰이더 최소 정밀도")]
		public D3D11_FEATURE_DATA_SHADER_MIN_PRECISION_SUPPORT ShaderMinimumPrecisionSupport;

		[SectionName("Direct3D 11.x 옵션 1")] public D3D11_FEATURE_DATA_D3D11_OPTIONS1 D3D11Options1;

		[SectionName("Direct3D 9 단순 인스턴싱")]
		public D3D11_FEATURE_DATA_D3D9_SIMPLE_INSTANCING_SUPPORT D3D9SimpleInstancingSupport;

		[SectionName("마커")] public D3D11_FEATURE_DATA_MARKER_SUPPORT MarkerSupport;
		[SectionName("Direct3D 9 옵션 1")] public D3D11_FEATURE_DATA_D3D9_OPTIONS1 D3D9Options1;
		[SectionName("Direct3D 11.x 옵션 2")] public D3D11_FEATURE_DATA_D3D11_OPTIONS2 D3D11Options2;
		[SectionName("Direct3D 11.x 옵션 3")] public D3D11_FEATURE_DATA_D3D11_OPTIONS3 D3D11Options3;
		[SectionName("GPU 가상 주소")] public D3D11_FEATURE_DATA_GPU_VIRTUAL_ADDRESS_SUPPORT GPUVirtualAddressSupport;
		[SectionName("Direct3D 11.x 옵션 4")] public D3D11_FEATURE_DATA_D3D11_OPTIONS4 D3D11Options4;
		[SectionName("셰이더 캐시")] public D3D11_FEATURE_DATA_SHADER_CACHE ShaderCache;
		[SectionName("Direct3D 11.x 옵션 5")] public D3D11_FEATURE_DATA_D3D11_OPTIONS5 D3D11Options5;

		public D3D11Probe(string deviceName)
		{
			DeviceName = deviceName;
		}

		public struct FeatureLevelProbe
		{
			[ItemDescription("기능레벨", "드라이버에서 지원하는 Direct3D 11 최대 기능레벨")]
			public Version FeatureLevel;
		}
	}

	public class Direct3D11Prober : IProber<D3D11Probe>
	{
		private IDXGIFactory _factory;

		public Direct3D11Prober ()
		{
			if (CreateDXGIFactory (IID_DXGIFactory, out var factory) != 0)
				return;

			_factory = factory as IDXGIFactory;
		}

		public void Dispose ()
		{
			if (_factory != null)
				Marshal.ReleaseComObject (_factory);
			_factory = null;
		}

		public string Name => "Direct3D 11";

		public IEnumerable<D3D11Probe> Probes
		{
			get
			{
				if (_factory == null)
					yield return null;

				D3D_FEATURE_LEVEL [] featureLevels = new []
				{
					D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_12_1,
					D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_12_0,
					D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_1,
					D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0,
					D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_1,
					D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_0,
					D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_3,
					D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_2,
					D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_1,
				};

				for (uint i = 0; ; ++i)
				{
					if (_factory.EnumAdapters (i, out var adapter) != 0)
						break;

					int hr = D3D11CreateDevice (adapter, D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_UNKNOWN, IntPtr.Zero,
						D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_BGRA_SUPPORT, featureLevels,
						(uint) featureLevels.Length,
						D3D11_SDK_VERSION, out var d3dDevice, out var featureLevel, out var immediateContext);
					if (hr != 0)
					{
						if (adapter != null)
							Marshal.ReleaseComObject (adapter);
						continue;
					}

					try
					{
						if (adapter.GetDesc (out var adapterDesc) != 0)
							continue;

						var probe = new D3D11Probe(adapterDesc.Description)
						{
							FeatureLevel =
							{
								FeatureLevel = featureLevel switch
								{
									D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_12_1 => new Version(12, 1),
									D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_12_0 => new Version(12, 0),
									D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_1 => new Version(11, 1),
									D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0 => new Version(11, 0),
									D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_1 => new Version(10, 1),
									D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_0 => new Version(10, 0),
									D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_3 => new Version(9, 3),
									D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_2 => new Version(9, 2),
									D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_1 => new Version(9, 1),
									D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_1_0_CORE => new Version(1, 0),
									_ => new Version()
								}
							},
							Threading = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_THREADING>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_THREADING),
							Doubles = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_DOUBLES>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_DOUBLES),
							D3D10HardwareOptions =
								GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D10_X_HARDWARE_OPTIONS>(d3dDevice,
									D3D11_FEATURE.D3D11_FEATURE_D3D10_X_HARDWARE_OPTIONS),
							D3D11Options = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D11_OPTIONS>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_D3D11_OPTIONS),
							Architecture = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_ARCHITECTURE_INFO>(
								d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_ARCHITECTURE_INFO),
							ShaderMinimumPrecisionSupport =
								GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_SHADER_MIN_PRECISION_SUPPORT>(
									d3dDevice,
									D3D11_FEATURE.D3D11_FEATURE_SHADER_MIN_PRECISION_SUPPORT),
							D3D9Option = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D9_OPTIONS>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_D3D9_OPTIONS),
							D3D9ShadowSupport = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D9_SHADOW_SUPPORT>(
								d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_D3D9_SHADOW_SUPPORT),
							D3D11Options1 = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D11_OPTIONS1>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_D3D11_OPTIONS1),
							D3D9SimpleInstancingSupport =
								GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D9_SIMPLE_INSTANCING_SUPPORT>(
									d3dDevice,
									D3D11_FEATURE.D3D11_FEATURE_D3D9_SIMPLE_INSTANCING_SUPPORT),
							MarkerSupport = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_MARKER_SUPPORT>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_MARKER_SUPPORT),
							D3D9Options1 = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D9_OPTIONS1>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_D3D9_OPTIONS1),
							D3D11Options2 = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D11_OPTIONS2>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_D3D11_OPTIONS2),
							D3D11Options3 = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D11_OPTIONS3>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_D3D11_OPTIONS3),
							GPUVirtualAddressSupport =
								GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_GPU_VIRTUAL_ADDRESS_SUPPORT>(
									d3dDevice,
									D3D11_FEATURE.D3D11_FEATURE_GPU_VIRTUAL_ADDRESS_SUPPORT),
							D3D11Options4 = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D11_OPTIONS4>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_D3D11_OPTIONS4),
							ShaderCache = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_SHADER_CACHE>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_SHADER_CACHE),
							D3D11Options5 = GetFeatureSupportFromD3DDevice<D3D11_FEATURE_DATA_D3D11_OPTIONS5>(d3dDevice,
								D3D11_FEATURE.D3D11_FEATURE_D3D11_OPTIONS5)
						};

						yield return probe;
					}
					finally
					{
						if (immediateContext != null)
							Marshal.ReleaseComObject (immediateContext);
						if (d3dDevice != null)
							Marshal.ReleaseComObject (d3dDevice);
						if (adapter != null)
							Marshal.ReleaseComObject (adapter);
					}
				}
			}
		}

		IEnumerable IProber.Probes => Probes;

		private T GetFeatureSupportFromD3DDevice<T> (ID3D11Device d3dDevice, D3D11_FEATURE feature)
		{
			var structSize = (uint) Marshal.SizeOf<T> ();
			var structPtr = Marshal.AllocCoTaskMem ((int) structSize);
			try
			{
				return d3dDevice.CheckFeatureSupport (feature, structPtr, structSize) != 0 ? default (T) : Marshal.PtrToStructure<T> (structPtr);
			}
			finally
			{
				Marshal.FreeCoTaskMem(structPtr);
			}
		}

		public void GetFeatureSupportFromD3DDevice<T>(ID3D11Device d3dDevice, D3D11_FEATURE feature, ref T inout)
		{
			var structSize = (uint) Marshal.SizeOf<T>();
			var structPtr = Marshal.AllocCoTaskMem((int) structSize);
			Marshal.StructureToPtr(inout, structPtr, false);
			inout = d3dDevice.CheckFeatureSupport(feature, structPtr, structSize) != 0
				? default(T)
				: Marshal.PtrToStructure<T>(structPtr);
			Marshal.FreeCoTaskMem(structPtr);
		}
	}
}
