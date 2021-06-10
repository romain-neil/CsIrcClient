using System;
using System.Windows;

namespace IrcClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		private readonly IrcClient _irc;
		
		public MainWindow()
		{
			InitializeComponent();

			_irc = new IrcClient();
		}

		public IrcClient GetIrcClient()
		{
			return _irc;
		}

		private void MenuConnect_Click(object sender, RoutedEventArgs e)
		{
			if (_irc.IsConnected()) return;

			MenuConnect.IsEnabled = false;
			MenuDisconnect.IsEnabled = true;
			
			ConnectToServer();
		}

		private void MenuDisconnect_Click(object sender, RoutedEventArgs e)
		{
			if (!_irc.IsConnected()) return;
			
			MenuConnect.IsEnabled = true;
			MenuDisconnect.IsEnabled = false;
			_irc.Disconnect();
		}

        private void MenuSettings_OnClick(object sender, RoutedEventArgs e)
        {
	        var settingsWindow = new SettingsWindow();
	        settingsWindow.Show();
        }

        private void ConnectToServer()
        {
	        _irc.Connect();
            _irc.JoinChannel("defis");
            //string msg;

            //do
            //{
            //	msg = _irc.Read();
            //    Tb.AppendText(msg);
            //    Tb.AppendText(Environment.NewLine);
            //} while (msg != null);
        }

        private void BtnSend_OnClick(object sender, RoutedEventArgs e)
        {
	        if (Input.Text != "")
	        {
		        _irc.SendToChat(Input.Text);
		        Tb.AppendText(Input.Text + Environment.NewLine);
		        Input.Text = "";
	        }
        }
	}
}