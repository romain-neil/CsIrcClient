using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IrcClient
{
	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();
		}

		private void Port_OnKeyDown(object sender, KeyEventArgs e)
        {
	        if (char.IsDigit(e.Key.ToString(), 0) && e.Key.ToString() != ".")
	        {
		        e.Handled = true;
	        }
	        else
	        {
		        e.Handled = false;
	        }
        }

		private void Username_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var tb = (TextBox) sender;

			if (Username.Text == SecondUsernameChoice.Text)
			{
				tb.Style = (Style) Resources["ErrorStyle"];
				BtnSavePrefs.IsEnabled = false;
			}
			else
			{
				tb.Style = null;
				BtnSavePrefs.IsEnabled = true;
			}
		}

		private void BtnSavePrefs_OnClick(object sender, RoutedEventArgs e)
		{
			var win = (MainWindow) Application.Current.MainWindow;
			var irc = win.GetIrcClient();
			var port = 0;

			if (int.TryParse(Port.Text, out port))
			{
				irc.SetPort(port);
			}

			if (Username.Text != "")
			{
				irc.SetUsername(Username.Text);
			}

			if (ServerAddress.Text != "")
			{
				irc.SetHostName(ServerAddress.Text);
			}
		}
	}
}