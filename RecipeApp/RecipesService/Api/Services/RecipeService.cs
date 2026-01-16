using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace RecipesService.Api.Services;

public class RecipeService : RecipeGrpcService.RecipeGrpcServiceBase
{
    private readonly ILogger<RecipeService> _logger;

    public RecipeService(ILogger<RecipeService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}