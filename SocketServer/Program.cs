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

Console.WriteLine("Server: Client connected");

// get stream of client (needed to read/write data)
var clientStream = client.GetStream();

var streamWriter = new StreamWriter(clientStream){AutoFlush = true}; // autoflush true so we dont need to flush after each writeline

// send welcome message to client
streamWriter.WriteLine("Hello World!");

Console.WriteLine("Waiting for client input...");

// read line of client
var streamReader = new StreamReader(clientStream);

while (true)
{
    var clientLine = streamReader.ReadLine();

    if (clientLine == null)
    {
        Console.WriteLine("Server: Client disconnected unexpectedly");
        break;
    }
    
    Console.WriteLine("Client: " + clientLine);

    if (clientLine.Equals("stop", StringComparison.OrdinalIgnoreCase))
    {
        streamWriter.WriteLine("Server disconnecting");
        break;
    }
    
    streamWriter.WriteLine("Server heeft het volgende ontvangen: " + clientLine);
}

// sluit alle connecties
streamWriter.Close();
streamReader.Close();
clientStream.Close();


client.Close();
listener.Stop();

Console.WriteLine("Server: Stopped");