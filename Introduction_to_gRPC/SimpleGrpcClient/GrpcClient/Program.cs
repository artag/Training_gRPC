using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Ignore the invalid certificate.
            // WARNING:
            // Untrusted certificates should only be used during app development.
            // Production apps should always use valid certificates.
            var httpClientHandler = new HttpClientHandler();

            // Return `true` to allow certificates that are untrusted/invalid.
            httpClientHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var httpClient = new HttpClient(httpClientHandler);

            // Create gRPC channel.
            var address = "https://localhost:5001";
            // Option to ignore ntrusted/invalid certificate.
            var channelOptions = new GrpcChannelOptions { HttpClient = httpClient }; 
            using var channel = GrpcChannel.ForAddress(address, channelOptions);

            // Create gRPC client.
            var client = new Greeter.GreeterClient(channel);

            Console.Write("Enter your first name: ");
            var name = Console.ReadLine();

            var request = new HelloRequest { Name = name };
            var reply = await client.SayHelloAsync(request);

            Console.WriteLine("Server reply: " + reply.Message);

            Console.WriteLine("\nPress any key to quit...");
            Console.ReadKey();
        }
    }
}
