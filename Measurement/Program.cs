var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/measurement", () =>
{
    return new[]
    {
        new Measurement.Model.Measurement {UserId = "terje", Type = "x"}
    };
});
app.Run();
