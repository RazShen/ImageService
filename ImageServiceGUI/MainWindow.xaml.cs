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

namespace ImageServiceGUI
	{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
		{
		public bool SomeConditionalProperty
			{
			get { return SomeConditionalProperty; }
			set
				{
				//...

				//OnPropertyChanged("SomeConditionalProperty");
				////Because Background is dependent on this property.
				//OnPropertyChanged("Background");
				}
			}

		public Brush Background
			{
			get
				{
				return SomeConditionalProperty ? Brushes.Pink : Brushes.LightGreen;
				}
			}

		public MainWindow()
			{
			InitializeComponent();

			}

		private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
			{

			}
		}
	}
