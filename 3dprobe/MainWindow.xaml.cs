using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _3dprobe.Probes;

namespace _3dprobe
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private IProber d3d9Prober;
		private IProber d3d11Prober;
		private IProber d3d12Prober;

		public MainWindow ()
		{
			InitializeComponent ();

			TabControlProbes.ItemsSource = new []
			{
				d3d12Prober = new Direct3D12Prober(),
				d3d11Prober = new Direct3D11Prober(),
				d3d9Prober = new Direct3D9Prober(),
			};
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			d3d9Prober?.Dispose();
			d3d11Prober?.Dispose();
			d3d12Prober?.Dispose();
		}
	}
}
