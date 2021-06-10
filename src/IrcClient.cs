using System.IO;
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
		
		#region Internal needed vars

		private Stream _stream;
		private StreamReader _reader;
		private StreamWriter _writer;
		
		#endregion
		
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

		public void SetUsername(string user)
		{
			User = user;
		}

		public void SetPort(int newPort)
		{
			Port = newPort;
		}

		public void Connect()
		{
			if (HostName != "" && Port != 0)
			{
				_tcp = new TcpClient(HostName, Port);

				_stream = _tcp.GetStream();
				_reader = new StreamReader(_stream);
				_writer = new StreamWriter(_stream) {NewLine = "\r\n", AutoFlush = true};

				_writer.WriteLine("NICK " + User);
				_writer.WriteLine("JOIN defis");
			}
		}

		public string Read()
		{
			return _reader.ReadLine();
		}

		public void Disconnect()
		{
			_tcp.Close();
		}
	}
}