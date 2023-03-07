using Grpc.Core;
using GrpcPopulation;
using System.Threading.Tasks;

namespace Server
{
    public class populationProviderService:populationProvider.populationProviderBase
    {
        public override Task<populationResponse> getPopulation(populationRequest request, ServerCallContext context)
        {
            return Task.FromResult(new populationResponse { Count = "100"});
        }
    }
}
