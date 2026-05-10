using System.Net;
using System.Net.Sockets;

Console.Write("Server: Server starting...");

const int port = 5000;

// add listener (any local ip and defined port)
var listener = new TcpListener(IPAddress.Any, port);

listener.Start();

Console.WriteLine("Server: Listening on port " + port);

// wait until client connects
var client = listener.AcceptTcpClient();

if (client.Connected)
{
    Console.WriteLine("Server: Client connected");
    
    // get stream of client (needed to read/write data)
    var clientStream = client.GetStream();
    
    var streamWriter = new StreamWriter(clientStream);
    // write hello world line
    streamWriter.WriteLine("Hello World!");
    // write buffered lines to stream
    streamWriter.Flush();
    
    streamWriter.Close();
    
    clientStream.Close();
}

client.Close();
listener.Stop();