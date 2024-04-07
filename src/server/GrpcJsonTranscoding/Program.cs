using GrpcJsonTranscoding.Services;
using GrpcJsonTranscoding.Utilities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "gRPC transcoding", Version = "v1" });

    // Read generated XML file
    var filePath = Path.Combine(AppContext.BaseDirectory, AppDomain.CurrentDomain.FriendlyName + ".xml");
    c.IncludeXmlComments(filePath);
    c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseGrpcWeb();
// for all services
// app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
});
app.MapGrpcService<GreeterService>().EnableGrpcWeb();

app.UseWebSockets();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var ws = await context.WebSockets.AcceptWebSocketAsync();

            // handle
            byte[] buffer = new byte[1024];
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                // Handle received data

                // Echo back the received data
                await ws.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await ws.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});

var port = EnvironmentAccessor.GetAsInt("BACKEND_PORT", 8081);
app.Run($"http://0.0.0.0:{port}");
