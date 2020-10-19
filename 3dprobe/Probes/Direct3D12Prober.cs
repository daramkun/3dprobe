using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using _3dprobe.Bridge;
using static _3dprobe.Bridge.DXGI;
using static _3dprobe.Bridge.Direct3D11;
using static _3dprobe.Bridge.Direct3D12;

namespace _3dprobe.Probes
{
	public class D3D12Probe : IProbe
	{
		public string DeviceName { get; }

		[SectionName ("기능레벨")]
		public FeatureLevelProbe FeatureLevel;

		[SectionName ("Direct3D 12 옵션")]
		public D3D12_FEATURE_DATA_D3D12_OPTIONS D3D12Options;
		[SectionName ("아키텍처")]
		public D3D12_FEATURE_DATA_ARCHITECTURE Architecture;
		[SectionName ("GPU 가상 주소")]
		public D3D12_FEATURE_DATA_GPU_VIRTUAL_ADDRESS_SUPPORT GPUVirtualAddress;
		[SectionName ("셰이더 모델")]
		public D3D12_FEATURE_DATA_SHADER_MODEL ShaderModel;
		[SectionName ("Direct3D 12 옵션 1")]
		public D3D12_FEATURE_DATA_D3D12_OPTIONS1 D3D12Options1;
		[SectionName ("보호된 리소스 세션")]
		public D3D12_FEATURE_DATA_PROTECTED_RESOURCE_SESSION_SUPPORT ProtectedResourcedSession;
		[SectionName ("루트 시그니처")]
		public D3D12_FEATURE_DATA_ROOT_SIGNATURE RootSignature;
		[SectionName ("아키텍처 1")]
		public D3D12_FEATURE_DATA_ARCHITECTURE1 Architecture1;
		[SectionName ("Direct3D 12 옵션 2")]
		public D3D12_FEATURE_DATA_D3D12_OPTIONS2 D3D12Options2;
		[SectionName ("셰이더 캐시")]
		public D3D12_FEATURE_DATA_SHADER_CACHE ShaderCache;
		[SectionName ("Direct3D 12 옵션 3")]
		public D3D12_FEATURE_DATA_D3D12_OPTIONS3 D3D12Options3;
		[SectionName ("Existing Heaps")]
		public D3D12_FEATURE_DATA_EXISTING_HEAPS ExistingHeaps;
		[SectionName ("Direct3D 12 옵션 4")]
		public D3D12_FEATURE_DATA_D3D12_OPTIONS4 D3D12Options4;
		[SectionName ("직렬화")]
		public D3D12_FEATURE_DATA_SERIALIZATION Serialization;
		[SectionName ("크로스 노드")]
		public D3D12_FEATURE_DATA_CROSS_NODE CrossNode;
		[SectionName ("Direct3D 12 옵션 5")]
		public D3D12_FEATURE_DATA_D3D12_OPTIONS5 D3D12Options5;
		[SectionName ("Direct3D 12 옵션 6")]
		public D3D12_FEATURE_DATA_D3D12_OPTIONS6 D3D12Options6;

		public D3D12Probe (string deviceName)
		{
			DeviceName = deviceName;
		}

		public struct FeatureLevelProbe
		{
			[ItemDescription ("기능레벨", "드라이버에서 지원하는 Direct3D 12 최대 기능레벨")]
			public Version FeatureLevel;
		}
	}

	public class Direct3D12Prober : IProber<D3D12Probe>
	{
		private IDXGIFactory2 _factory;

		public Direct3D12Prober ()
		{
			if (CreateDXGIFactory2(0, IID_DXGIFactory2, out var factory) != 0)
				return;

			_factory = factory as IDXGIFactory2;
		}

		public void Dispose ()
		{
			if (_factory != null)
				Marshal.ReleaseComObject (_factory);
			_factory = null;
		}

		public string Name => "Direct3D 12";

		public IEnumerable<D3D12Probe> Probes
		{
			get
			{
				if (_factory == null)
					yield return null;

				var featureLevels = new []
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
					if (_factory.EnumAdapters1 (i, out var adapter) != 0)
						break;

					var featureLevel = D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_12_1;
					var hr = -1;
					ID3D12Device d3dDevice = null;
					foreach (var fl in featureLevels)
					{
						hr = D3D12CreateDevice (adapter, featureLevel = fl,
							IID_D3D12Device,
							out var tempDevice);
						if (hr != 0)
							continue;

						d3dDevice = tempDevice as ID3D12Device;
						if (d3dDevice == null)
						{
							Marshal.ReleaseComObject(tempDevice);
							continue;
						}

						break;
					}

					if (hr != 0)
					{
						Marshal.ReleaseComObject (adapter);
						break;
					}

					try
					{
						if (adapter.GetDesc (out var adapterDesc) != 0)
							continue;

						var probe = new D3D12Probe(adapterDesc.Description)
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
							}
						};

						probe.D3D12Options =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_D3D12_OPTIONS>(d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_D3D12_OPTIONS);
						probe.D3D12Options1 =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_D3D12_OPTIONS1> (d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_D3D12_OPTIONS1);
						probe.D3D12Options2 =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_D3D12_OPTIONS2> (d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_D3D12_OPTIONS2);
						probe.D3D12Options3 =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_D3D12_OPTIONS3> (d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_D3D12_OPTIONS3);
						probe.D3D12Options4 =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_D3D12_OPTIONS4> (d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_D3D12_OPTIONS4);
						probe.D3D12Options5 =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_D3D12_OPTIONS5> (d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_D3D12_OPTIONS5);
						probe.D3D12Options6 =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_D3D12_OPTIONS6> (d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_D3D12_OPTIONS6);

						probe.Architecture =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_ARCHITECTURE>(d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_ARCHITECTURE);
						probe.Architecture1 =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_ARCHITECTURE1> (d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_ARCHITECTURE1);

						probe.GPUVirtualAddress =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_GPU_VIRTUAL_ADDRESS_SUPPORT>(d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_GPU_VIRTUAL_ADDRESS_SUPPORT);

						probe.ShaderModel =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_SHADER_MODEL> (d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_SHADER_MODEL);
						probe.ShaderCache =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_SHADER_CACHE> (d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_SHADER_CACHE);

						probe.ProtectedResourcedSession =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_PROTECTED_RESOURCE_SESSION_SUPPORT>(
								d3dDevice, D3D12_FEATURE.D3D12_FEATURE_PROTECTED_RESOURCE_SESSION_SUPPORT);

						probe.RootSignature =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_ROOT_SIGNATURE>(d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_ROOT_SIGNATURE);

						probe.ExistingHeaps =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_EXISTING_HEAPS>(d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_EXISTING_HEAPS);

						probe.Serialization =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_SERIALIZATION>(d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_SERIALIZATION);

						probe.CrossNode =
							GetFeatureSupportFromD3DDevice<D3D12_FEATURE_DATA_CROSS_NODE>(d3dDevice,
								D3D12_FEATURE.D3D12_FEATURE_CROSS_NODE);

						yield return probe;
					}
					finally
					{
						if (d3dDevice != null)
							Marshal.ReleaseComObject (d3dDevice);
						if (adapter != null)
							Marshal.ReleaseComObject (adapter);
					}
				}
			}
		}

		IEnumerable IProber.Probes => Probes;

		private T GetFeatureSupportFromD3DDevice<T> (ID3D12Device d3dDevice, D3D12_FEATURE feature)
		{
			var structSize = (uint) Marshal.SizeOf<T> ();
			var structPtr = Marshal.AllocCoTaskMem ((int) structSize);
			if (typeof(T) == typeof(D3D12_FEATURE_DATA_SHADER_MODEL))
				Marshal.WriteInt32(structPtr, (int) D3D_SHADER_MODEL.D3D_SHADER_MODEL_6_5);
			return d3dDevice.CheckFeatureSupport (feature, structPtr, structSize) != 0 ? default (T) : Marshal.PtrToStructure<T> (structPtr);
		}
	}
}
