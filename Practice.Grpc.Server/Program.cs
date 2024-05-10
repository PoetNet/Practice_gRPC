using Metanit;
using Practice.Grpc.Server.Services;

namespace Practice.Grpc.Server;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpc();

        var app = builder.Build();

        app.MapGrpcService<UserApiService>();
        app.MapGet("/", () => "Practice gRPC project");

        app.Run();
    }
}
