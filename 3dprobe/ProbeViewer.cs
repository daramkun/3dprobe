using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using _3dprobe.Bridge;
using static _3dprobe.Bridge.Direct3D9;
using static _3dprobe.Bridge.Direct3D11;
using static _3dprobe.Bridge.Direct3D12;
using Color = System.Windows.Media.Color;
using Size = System.Drawing.Size;

namespace _3dprobe
{
	public class ProbeViewer : StackPanel
	{
		public static DependencyProperty SourceProperty =
			DependencyProperty.Register(
				"Source",
				typeof(IProbe),
				typeof(ProbeViewer),
				new PropertyMetadata(SourcePropertyChanged)
			);

		static void SourcePropertyChanged (DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			(obj as ProbeViewer)?.OnSourceChanged(e.OldValue as IProbe, e.NewValue as IProbe);
		}

		public IProbe Source
		{
			get => GetValue(SourceProperty) as IProbe;
			set => SetValue(SourceProperty, value);
		}

		public ProbeViewer()
		{
			Orientation = Orientation.Vertical;
			HorizontalAlignment = HorizontalAlignment.Stretch;
		}

		protected virtual void OnSourceChanged(IProbe oldProbe, IProbe newProbe)
		{
			Children.Clear();

			if (newProbe == null)
				return;

			var probeType = newProbe.GetType();
			foreach (var field in probeType.GetFields())
			{
				var sectionName = field.GetCustomAttribute<SectionNameAttribute>();
				var textBlockSectionName = new TextBlock
				{
					Text = sectionName?.Name ?? field.Name,
					FontSize = 18,
					Margin = new Thickness(0, 0, 0, 4),
				};
				Children.Add(textBlockSectionName);

				var grid = new Grid()
				{
					Margin = new Thickness(0, 0, 0, 8),
					HorizontalAlignment = HorizontalAlignment.Stretch,
				};
				grid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(386, GridUnitType.Star)});
				grid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(256, GridUnitType.Star)});

				var probeFieldValue = field.GetValue(newProbe);
				var probeFieldValueType = probeFieldValue?.GetType();
				var probeFieldValueFields = probeFieldValueType?.GetFields();
				var count = 0;
				foreach (var valueField in probeFieldValueFields ?? new FieldInfo[0])
				{
					grid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength()});

					var itemDesc = valueField.GetCustomAttribute<ItemDescriptionAttribute>();

					var textBlockValueName = new TextBlock
					{
						Text = itemDesc?.ItemName ?? valueField.Name,
						ToolTip = itemDesc?.ItemDescription,
						TextWrapping = TextWrapping.WrapWithOverflow,
						Margin = new Thickness(0, 4, 0, 4),
					};
					grid.Children.Add(textBlockValueName);
					Grid.SetColumn(textBlockValueName, 0);
					Grid.SetRow(textBlockValueName, count);

					var value = valueField?.GetValue(probeFieldValue);
					var textBlockValueValue = new TextBlock
					{
						Text = ValueConverter.Convert(value) ?? value?.ToString() ?? "",
						ToolTip = itemDesc?.ItemDescription,
						TextWrapping = TextWrapping.WrapWithOverflow,
						Margin = new Thickness (0, 4, 0, 4),
					};
					textBlockValueValue.Foreground = ValueToColor(textBlockValueValue.Text);
					grid.Children.Add(textBlockValueValue);
					Grid.SetColumn(textBlockValueValue, 1);
					Grid.SetRow(textBlockValueValue, count);

					var borderValueName = new Border
					{
						BorderBrush = Brushes.DimGray,
						BorderThickness = new Thickness(0, 0, 0, 0.1),
					};
					grid.Children.Add (borderValueName);
					Grid.SetColumn (borderValueName, 0);
					Grid.SetRow (borderValueName, count);

					var borderValueValue = new Border
					{
						BorderBrush = Brushes.DimGray,
						BorderThickness = new Thickness (0, 0, 0, 0.1),
					};
					grid.Children.Add (borderValueValue);
					Grid.SetColumn (borderValueValue, 1);
					Grid.SetRow (borderValueValue, count);

					++count;
				}

				Children.Add(grid);
			}
		}

		private static Brush ValueToColor(string text)
		{
			switch (text)
			{
				case "지원":
					return Brushes.Blue;
				case "미지원":
				case "지원하지 않음":
					return Brushes.Red;
				default:
					return Brushes.Black;
			}
		}
	}

	static class ValueConverter
	{
		public static string Convert<T>(T obj)
		{
			switch (obj)
			{
				case string s:
					return s;

				case bool _:
					return true.Equals(obj) ? "지원" : "미지원";

				case int _:
				case uint _:
					return obj.ToString();

				case Size size:
					return $"{size.Width} * {size.Height}";

				case D3D_FEATURE_LEVEL featureLevel:
					return featureLevel switch
					{
						D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_12_1 => "12.1",
						D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_12_0 => "12.0",
						D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_1 => "11.1",
						D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0 => "11.0",
						D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_1 => "10.1",
						D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_0 => "10.0",
						D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_3 => "9.3",
						D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_2 => "9.2",
						D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_1 => "9.1",
						D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_1_0_CORE => "1.0 Core",
						_ => "Unknown"
					};

				case D3D11_SHADER_MIN_PRECISION_SUPPORT minPrecisionSupport:
				{
					var caches = new List<string>();
					if (minPrecisionSupport.HasFlag(
						D3D11_SHADER_MIN_PRECISION_SUPPORT.D3D11_SHADER_MIN_PRECISION_10_BIT))
						caches.Add("10비트");
					if (minPrecisionSupport.HasFlag(
						D3D11_SHADER_MIN_PRECISION_SUPPORT.D3D11_SHADER_MIN_PRECISION_16_BIT))
						caches.Add("16비트");

					return caches.Count == 0 ? "32비트" : string.Join(", ", caches);
				}

				case D3D11_TILED_RESOURCES_TIER tiledResourcesTier:
					return tiledResourcesTier switch
					{
						D3D11_TILED_RESOURCES_TIER.D3D11_TILED_RESOURCES_NOT_SUPPORTED => "지원하지 않음",
						D3D11_TILED_RESOURCES_TIER.D3D11_TILED_RESOURCES_TIER_1 => "티어 1",
						D3D11_TILED_RESOURCES_TIER.D3D11_TILED_RESOURCES_TIER_2 => "티어 2",
						D3D11_TILED_RESOURCES_TIER.D3D11_TILED_RESOURCES_TIER_3 => "티어 3",
						_ => "Unknown"
					};

				case D3D11_CONSERVATIVE_RASTERIZATION_TIER conservativeRasterizationTier:
					return conservativeRasterizationTier switch
					{
						D3D11_CONSERVATIVE_RASTERIZATION_TIER
							.D3D11_CONSERVATIVE_RASTERIZATION_NOT_SUPPORTED => "지원하지 않음",
						D3D11_CONSERVATIVE_RASTERIZATION_TIER.D3D11_CONSERVATIVE_RASTERIZATION_TIER_1 => "티어 1",
						D3D11_CONSERVATIVE_RASTERIZATION_TIER.D3D11_CONSERVATIVE_RASTERIZATION_TIER_2 => "티어 2",
						D3D11_CONSERVATIVE_RASTERIZATION_TIER.D3D11_CONSERVATIVE_RASTERIZATION_TIER_3 => "티어 3",
						_ => "Unknown"
					};

				case D3D11_SHADER_CACHE_SUPPORT_FLAGS shaderCacheSupportFlags:
				{
					var caches = new List<string>();
					if (shaderCacheSupportFlags.HasFlag(D3D11_SHADER_CACHE_SUPPORT_FLAGS
						.D3D11_SHADER_CACHE_SUPPORT_AUTOMATIC_DISK_CACHE))
						caches.Add("자동 디스크 캐시");
					if (shaderCacheSupportFlags.HasFlag(D3D11_SHADER_CACHE_SUPPORT_FLAGS
						.D3D11_SHADER_CACHE_SUPPORT_AUTOMATIC_INPROC_CACHE))
						caches.Add("자동 메모리 캐시");

					return caches.Count == 0 ? "지원하지 않음" : string.Join(", ", caches);
				}

				case D3D11_SHARED_RESOURCE_TIER sharedResourceTier:
					return sharedResourceTier switch
					{
						D3D11_SHARED_RESOURCE_TIER.D3D11_SHARED_RESOURCE_TIER_0 => "티어 0",
						D3D11_SHARED_RESOURCE_TIER.D3D11_SHARED_RESOURCE_TIER_1 => "티어 1",
						D3D11_SHARED_RESOURCE_TIER.D3D11_SHARED_RESOURCE_TIER_2 => "티어 2",
						D3D11_SHARED_RESOURCE_TIER.D3D11_SHARED_RESOURCE_TIER_3 => "티어 3",
						_ => "Unknown"
					};

				case D3D12_MULTISAMPLE_QUALITY_LEVEL_FLAGS multisampleQualityLevelFlags:
					return multisampleQualityLevelFlags switch
					{
						D3D12_MULTISAMPLE_QUALITY_LEVEL_FLAGS
							.D3D12_MULTISAMPLE_QUALITY_LEVELS_FLAG_TILED_RESOURCE => "타일 리소스",
						D3D12_MULTISAMPLE_QUALITY_LEVEL_FLAGS.D3D12_MULTISAMPLE_QUALITY_LEVELS_FLAG_NONE => "없음",
						_ => "Unknown"
					};

				case D3D12_CROSS_NODE_SHARING_TIER crossNodeSharingTier:
					return crossNodeSharingTier switch
					{
						D3D12_CROSS_NODE_SHARING_TIER.D3D12_CROSS_NODE_SHARING_TIER_NOT_SUPPORTED => "지원하지 않음",
						D3D12_CROSS_NODE_SHARING_TIER.D3D12_CROSS_NODE_SHARING_TIER_1_EMULATED => "에뮬레이트 방식 티어 1",
						D3D12_CROSS_NODE_SHARING_TIER.D3D12_CROSS_NODE_SHARING_TIER_1 => "티어 1",
						D3D12_CROSS_NODE_SHARING_TIER.D3D12_CROSS_NODE_SHARING_TIER_2 => "티어 2",
						D3D12_CROSS_NODE_SHARING_TIER.D3D12_CROSS_NODE_SHARING_TIER_3 => "티어 3",
						_ => "Unknown"
					};

				case D3D12_RESOURCE_HEAP_TIER resourceHeapTier:
					return resourceHeapTier switch
					{
						D3D12_RESOURCE_HEAP_TIER.D3D12_RESOURCE_HEAP_TIER_1 => "티어 1",
						D3D12_RESOURCE_HEAP_TIER.D3D12_RESOURCE_HEAP_TIER_2 => "티어 2",
						_ => "Unknown"
					};

				case D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER programmableSamplePositionsTier:
					return programmableSamplePositionsTier switch
					{
						D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER.D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER_1 => "티어 1",
						D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER.D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER_2 => "티어 2",
						D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER
							.D3D12_PROGRAMMABLE_SAMPLE_POSITIONS_TIER_NOT_SUPPORTED => "지원하지 않음",
						_ => "Unknown"
					};

				case D3D12_VIEW_INSTANCING_TIER viewInstancingTier:
					return viewInstancingTier switch
					{
						D3D12_VIEW_INSTANCING_TIER.D3D12_VIEW_INSTANCING_TIER_1 => "티어 1",
						D3D12_VIEW_INSTANCING_TIER.D3D12_VIEW_INSTANCING_TIER_2 => "티어 2",
						D3D12_VIEW_INSTANCING_TIER.D3D12_VIEW_INSTANCING_TIER_3 => "티어 3",
						D3D12_VIEW_INSTANCING_TIER.D3D12_VIEW_INSTANCING_TIER_NOT_SUPPORTED => "지원하지 않음",
						_ => "Unknown"
					};

				case D3D12_SHADER_MIN_PRECISION_SUPPORT minPrecisionSupport:
				{
					var caches = new List<string>();
					if (minPrecisionSupport.HasFlag(D3D12_SHADER_MIN_PRECISION_SUPPORT
						.D3D12_SHADER_MIN_PRECISION_SUPPORT_10_BIT))
						caches.Add("10비트");
					if (minPrecisionSupport.HasFlag(D3D12_SHADER_MIN_PRECISION_SUPPORT
						.D3D12_SHADER_MIN_PRECISION_SUPPORT_16_BIT))
						caches.Add("16비트");

					return caches.Count == 0 ? "32비트" : string.Join(", ", caches);
				}

				case D3D12_TILED_RESOURCES_TIER tiledResourcesTier:
					return tiledResourcesTier switch
					{
						D3D12_TILED_RESOURCES_TIER.D3D12_TILED_RESOURCES_TIER_1 => "티어 1",
						D3D12_TILED_RESOURCES_TIER.D3D12_TILED_RESOURCES_TIER_2 => "티어 2",
						D3D12_TILED_RESOURCES_TIER.D3D12_TILED_RESOURCES_TIER_3 => "티어 3",
						D3D12_TILED_RESOURCES_TIER.D3D12_TILED_RESOURCES_TIER_4 => "티어 4",
						D3D12_TILED_RESOURCES_TIER.D3D12_TILED_RESOURCES_TIER_NOT_SUPPORTED => "지원하지 않음",
						_ => "Unknown"
					};

				case D3D12_RESOURCE_BINDING_TIER resourceBindingTier:
					return resourceBindingTier switch
					{
						D3D12_RESOURCE_BINDING_TIER.D3D12_RESOURCE_BINDING_TIER_1 => "티어 1",
						D3D12_RESOURCE_BINDING_TIER.D3D12_RESOURCE_BINDING_TIER_2 => "티어 2",
						D3D12_RESOURCE_BINDING_TIER.D3D12_RESOURCE_BINDING_TIER_3 => "티어 3",
						_ => "Unknown"
					};

				case D3D12_CONSERVATIVE_RASTERIZATION_TIER conservativeRasterizationTier:
					return conservativeRasterizationTier switch
					{
						D3D12_CONSERVATIVE_RASTERIZATION_TIER
							.D3D12_CONSERVATIVE_RASTERIZATION_TIER_NOT_SUPPORTED => "지원하지 않음",
						D3D12_CONSERVATIVE_RASTERIZATION_TIER.D3D12_CONSERVATIVE_RASTERIZATION_TIER_1 => "티어 1",
						D3D12_CONSERVATIVE_RASTERIZATION_TIER.D3D12_CONSERVATIVE_RASTERIZATION_TIER_2 => "티어 2",
						D3D12_CONSERVATIVE_RASTERIZATION_TIER.D3D12_CONSERVATIVE_RASTERIZATION_TIER_3 => "티어 3",
						_ => "Unknown"
					};

				case D3D_ROOT_SIGNATURE_VERSION rootSignatureVersion:
					return rootSignatureVersion switch
					{
						D3D_ROOT_SIGNATURE_VERSION.D3D_ROOT_SIGNATURE_VERSION_1_1 => "1.1",
						D3D_ROOT_SIGNATURE_VERSION.D3D_ROOT_SIGNATURE_VERSION_1_0 => "1.0",
						_ => "Unknown"
					};

				case D3D_SHADER_MODEL shaderModel:
					return shaderModel switch
					{
						D3D_SHADER_MODEL.D3D_SHADER_MODEL_6_5 => "6.5",
						D3D_SHADER_MODEL.D3D_SHADER_MODEL_6_4 => "6.4",
						D3D_SHADER_MODEL.D3D_SHADER_MODEL_6_3 => "6.3",
						D3D_SHADER_MODEL.D3D_SHADER_MODEL_6_2 => "6.2",
						D3D_SHADER_MODEL.D3D_SHADER_MODEL_6_1 => "6.1",
						D3D_SHADER_MODEL.D3D_SHADER_MODEL_6_0 => "6.0",
						D3D_SHADER_MODEL.D3D_SHADER_MODEL_5_1 => "5.1",
						_ => "Unknown"
					};

				case D3D12_SHADER_CACHE_SUPPORT_FLAGS shaderCacheSupportFlags:
				{
					var caches = new List<string>();
					if (shaderCacheSupportFlags.HasFlag(D3D12_SHADER_CACHE_SUPPORT_FLAGS
						.D3D12_SHADER_CACHE_SUPPORT_AUTOMATIC_DISK_CACHE))
						caches.Add("자동 디스크 캐시");
					if (shaderCacheSupportFlags.HasFlag(D3D12_SHADER_CACHE_SUPPORT_FLAGS
						.D3D12_SHADER_CACHE_SUPPORT_AUTOMATIC_INPROC_CACHE))
						caches.Add("자동 메모리 캐시");
					if (shaderCacheSupportFlags.HasFlag(D3D12_SHADER_CACHE_SUPPORT_FLAGS
						.D3D12_SHADER_CACHE_SUPPORT_LIBRARY))
						caches.Add("라이브러리 지원");
					if (shaderCacheSupportFlags.HasFlag(D3D12_SHADER_CACHE_SUPPORT_FLAGS
						.D3D12_SHADER_CACHE_SUPPORT_SINGLE_PSO))
						caches.Add("단일 파이프라인 상태 오브젝트");

					return caches.Count == 0 ? "지원하지 않음" : string.Join(", ", caches);
				}

				case D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER sharedResourceCompatibilityTier:
					return sharedResourceCompatibilityTier switch
					{
						D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER.D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_0 => "티어 0",
						D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER.D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_1 => "티어 1",
						D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER.D3D12_SHARED_RESOURCE_COMPATIBILITY_TIER_2 => "티어 2",
						_ => "Unknown"
					};

				case D3D12_HEAP_SERIALIZATION_TIER heapSerializationTier:
					return heapSerializationTier switch
					{
						D3D12_HEAP_SERIALIZATION_TIER.D3D12_HEAP_SERIALIZATION_TIER_10 => "티어 10",
						D3D12_HEAP_SERIALIZATION_TIER.D3D12_HEAP_SERIALIZATION_TIER_0 => "티어 0",
						_ => "Unknown"
					};

				case D3D12_RENDER_PASS_TIER renderPassTier:
					return renderPassTier switch
					{
						D3D12_RENDER_PASS_TIER.D3D12_RENDER_PASS_TIER_0 => "티어 0",
						D3D12_RENDER_PASS_TIER.D3D12_RENDER_PASS_TIER_1 => "티어 1",
						D3D12_RENDER_PASS_TIER.D3D12_RENDER_PASS_TIER_2 => "티어 2",
						_ => "Unknown"
					};

				case D3D12_RAYTRACING_TIER rayTracingTier:
					return rayTracingTier switch
					{
						D3D12_RAYTRACING_TIER.D3D12_RAYTRACING_TIER_NOT_SUPPORTED => "지원하지 않음",
						D3D12_RAYTRACING_TIER.D3D12_RAYTRACING_TIER_1_0 => "1.0",
						D3D12_RAYTRACING_TIER.D3D12_RAYTRACING_TIER_1_1 => "1.1",
						_ => "Unknown"
					};

				case D3D12_VARIABLE_SHADING_RATE_TIER variableShadingRateTier:
					return variableShadingRateTier switch
					{
						D3D12_VARIABLE_SHADING_RATE_TIER.D3D12_VARIABLE_SHADING_RATE_TIER_1 => "티어 1",
						D3D12_VARIABLE_SHADING_RATE_TIER.D3D12_VARIABLE_SHADING_RATE_TIER_2 => "티어 2",
						D3D12_VARIABLE_SHADING_RATE_TIER.D3D12_VARIABLE_SHADING_RATE_TIER_NOT_SUPPORTED => "지원하지 않음",
						_ => "Unknown"
					};

				case D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAGS protectedResourceSessionSupportFlags:
				{
					var caches = new List<string>();
					if (protectedResourceSessionSupportFlags.HasFlag(D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAGS
						.D3D12_PROTECTED_RESOURCE_SESSION_SUPPORT_FLAG_SUPPORTED))
						caches.Add("지원");

					return caches.Count == 0 ? "지원하지 않음" : string.Join(", ", caches);
				}

				case D3D12_COMMAND_LIST_SUPPORT_FLAGS commandListSupportFlags:
				{
					var caches = new List<string>();
					if (commandListSupportFlags.HasFlag(D3D12_COMMAND_LIST_SUPPORT_FLAGS
						.D3D12_COMMAND_LIST_SUPPORT_FLAG_DIRECT))
						caches.Add("직접");
					if (commandListSupportFlags.HasFlag(D3D12_COMMAND_LIST_SUPPORT_FLAGS
						.D3D12_COMMAND_LIST_SUPPORT_FLAG_BUNDLE))
						caches.Add("번들");
					if (commandListSupportFlags.HasFlag(D3D12_COMMAND_LIST_SUPPORT_FLAGS
						.D3D12_COMMAND_LIST_SUPPORT_FLAG_COMPUTE))
						caches.Add("컴퓨트");
					if (commandListSupportFlags.HasFlag(D3D12_COMMAND_LIST_SUPPORT_FLAGS
						.D3D12_COMMAND_LIST_SUPPORT_FLAG_COPY))
						caches.Add("복사");
					if (commandListSupportFlags.HasFlag(D3D12_COMMAND_LIST_SUPPORT_FLAGS
						.D3D12_COMMAND_LIST_SUPPORT_FLAG_VIDEO_DECODE))
						caches.Add("비디오 디코드");
					if (commandListSupportFlags.HasFlag(D3D12_COMMAND_LIST_SUPPORT_FLAGS
						.D3D12_COMMAND_LIST_SUPPORT_FLAG_VIDEO_PROCESS))
						caches.Add("비디오 처리");
					if (commandListSupportFlags.HasFlag(D3D12_COMMAND_LIST_SUPPORT_FLAGS
						.D3D12_COMMAND_LIST_SUPPORT_FLAG_VIDEO_ENCODE))
						caches.Add("비디오 인코드");

					return caches.Count == 0 ? "지원하지 않음" : string.Join(", ", caches);
				}

				default:
					return obj.ToString();
			}
		}
	}
}
