using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
using ImageServiceGUI.ViewModel;

namespace ImageServiceGUI
	{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	//// </summary>
	public partial class MainWindow : INotifyPropertyChanged
		{
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string propName = null)
			{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasConnection"));
			}

		private string _hasConnection;
		public string HasConnection
			{
			get
				{
				return _hasConnection;
				}
			set
				{
				if (_hasConnection != value)
					{
					_hasConnection = value;
					OnPropertyChanged();
					}
				}
			}
		/// <summary>
		/// Creates a main window view model.
		/// </summary>
		public MainWindow()
			{
			this.DataContext = new MainWindowViewModel();
			InitializeComponent();
			}


		}
	}
