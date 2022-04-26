using System.Data.SqlClient;
using Dapper;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string connStr = 
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TerjeTest;Integrated Security=True";
app.MapGet("/measurement", async () =>
{
    var conn = new SqlConnection(connStr);
    const string sql = "select * from measurement";
    var measurements = await conn.QueryAsync(sql);
    return measurements;
});
app.MapPost("/measurement", async (Measurement.Model.Measurement measurement) =>
{
    var conn = new SqlConnection(connStr);
    const string sql = "insert into measurement values (@Id, @Value, @TimeStamp, @Type, @UserId)";
    var rowsAffected = await conn.ExecuteAsync(sql, measurement);
    return rowsAffected;
});
app.UseStaticFiles();
app.Run();
