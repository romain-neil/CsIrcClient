using System.Net;
using System.Net.Sockets;

namespace IrcClient
{
	public class IrcClient
	{
		public string HostName { get; set; }
		
		public int Port { get; set; }
		
		public string User { get; set; }
		
		private TcpClient _tcp;
		
		public bool IsConnected()
		{
			return _tcp.Connected;
		}

		public IrcClient(string host = "localhost", int port = 0, string username = "")
		{
			HostName = host;
			Port = port;
			User = username;
		}

		public void Connect()
		{
			if (HostName != "" && Port != 0)
			{
				_tcp = new TcpClient(HostName, Port);
			}
		}

		public void Disconnect()
		{
			_tcp.Close();
		}
	}
}