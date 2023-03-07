using Grpc.Net.Client;
using System;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:44307/");
            var client = new GrpcPopulation.populationProvider.populationProviderClient(channel);
            Console.WriteLine(client.getPopulation(new GrpcPopulation.populationRequest { State="WB"}));
        }
    }
}
