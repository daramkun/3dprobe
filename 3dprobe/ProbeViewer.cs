using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
					Margin = new Thickness (0, 0, 0, 4),
				};
				Children.Add(textBlockSectionName);

				var grid = new Grid()
				{
					Margin = new Thickness(0, 0, 0, 8),
					HorizontalAlignment = HorizontalAlignment.Stretch,
				};
				grid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(386) });
				grid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength()});

				var probeFieldValue = field.GetValue(newProbe);
				var probeFieldValueType = probeFieldValue.GetType();
				var probeFieldValueFields = probeFieldValueType.GetFields();
				var count = 0;
				foreach (var valueField in probeFieldValueFields)
				{
					grid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength()});

					var itemDesc = valueField.GetCustomAttribute<ItemDescriptionAttribute>();

					var textBlockValueName = new TextBlock
					{
						Text = itemDesc?.ItemName ?? valueField.Name,
						ToolTip = itemDesc?.ItemDescription,
						Margin = new Thickness(0, 0, 0, 6),
					};
					grid.Children.Add(textBlockValueName);
					Grid.SetColumn(textBlockValueName, 0);
					Grid.SetRow (textBlockValueName, count);

					var value = valueField?.GetValue(probeFieldValue);
					var textBlockValueValue = new TextBlock
					{
						Text = itemDesc?.Converter?.Invoke(value) ?? value?.ToString() ?? "",
						ToolTip = itemDesc?.ItemDescription,
						TextWrapping = TextWrapping.WrapWithOverflow,
					};
					textBlockValueValue.Foreground = new SolidColorBrush(ValueToColor(textBlockValueValue.Text));
					grid.Children.Add (textBlockValueValue);
					Grid.SetColumn (textBlockValueValue, 1);
					Grid.SetRow(textBlockValueValue, count);

					++count;
				}

				Children.Add(grid);
			}
		}

		static Color ValueToColor(string text)
		{
			if (text == "True") return Colors.Blue;
			else if (text == "False") return Colors.Red;
			return Colors.Black;
		}
	}
}
