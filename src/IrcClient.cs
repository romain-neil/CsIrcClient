#nullable enable
using System;
using System.IO;
using System.Net.Sockets;

namespace IrcClient
{
	public class IrcClient
	{
		private string HostName { get; set; }

		private int Port { get; set; }

		private string User { get; set; }
		
		private string Channel { get; set; }
		
		private TcpClient? _tcp;
		
		private Stream? _stream;
		private StreamReader? _reader;
		private StreamWriter? _writer;

		public bool IsConnected()
		{
			return _tcp is {Connected: true};
		}

		public IrcClient(string host = "localhost", int port = 0, string username = "")
		{
			HostName = host;
			Port = port;
			User = username;
		}

		public void SetHostName(string newHost)
		{
			HostName = newHost;
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
				_writer.WriteLine("USER " + User + " 0 * " + User);
			}
		}

		public void JoinChannel(string channel)
		{
			Channel = channel;
			_writer?.WriteLine("JOIN #" + channel);
		}

		public string? Read()
		{
			return _reader?.ReadToEnd();
		}

		public void Send(string text)
		{
			try
			{
				_writer.WriteLine(text);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		
		public void SendToChat(string inputText)
        {
        	Send("PRIVMSG #" + Channel + " :" + inputText);
        }

		public void Disconnect()
		{
			if (_tcp != null && _writer != null && _reader != null)
			{
				_reader.DiscardBufferedData();
				_writer.WriteLine("QUIT");
				_tcp.Close();
			}
		}

		
	}
}