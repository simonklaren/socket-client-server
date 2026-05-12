using System.Net;
using System.Net.Sockets;


Console.Write("Socket server starting...");

// connect to localhost (socketServer)
const string host = "localhost";
const int port = 5000;

var client = new TcpClient();

client.Connect(host, port);

Console.WriteLine("Client: connected to server");

// get stream and read it 
var stream = client.GetStream();
var reader = new StreamReader(stream);

// read server message
var serverMessage = reader.ReadLine();

Console.WriteLine("Server: " + serverMessage);

// add writer so we can send a message back to server
var writer = new StreamWriter(stream) { AutoFlush = true };

while (true)
{
    // send input from console to writer
    Console.Write("Client: ");
    var input = Console.ReadLine();
    writer.WriteLine(input);

    var serverResponse = reader.ReadLine();
    Console.WriteLine("Server: " + serverResponse);

    if (input == "stop")
    {
        break;
    }
}

client.Close(); // finally close client