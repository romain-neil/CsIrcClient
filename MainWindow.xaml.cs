using System;
using System.Windows;

namespace IrcClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		private IrcClient _irc;
		
		public MainWindow()
		{
			InitializeComponent();

			_irc = new IrcClient();
		}

		private void MenuConnect_Click(object sender, RoutedEventArgs e)
		{
			if (_irc.IsConnected) return;

			MenuConnect.IsEnabled = false;
			MenuDisconnect.IsEnabled = true;
			_irc.Connect();
		}

		private void MenuDisconnect_Click(object sender, RoutedEventArgs e)
		{
			if (!_irc.IsConnected) return;
			
			MenuConnect.IsEnabled = true;
			MenuDisconnect.IsEnabled = false;
			_irc.Disconnect();
		}

        private void Button_Click(object sender, RoutedEventArgs e) {

        }
    }
}