using System.Data.SqlClient;
using Dapper;
using Measurement.Model;
using MeasurementX=Measurement.Model.Measurement;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string connStr = 
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TerjeTest;Integrated Security=True";
app.MapGet("/measurement", async () =>
{
    var conn = new SqlConnection(connStr);
    const string sql = "select * from measurement";
    var measurements = await conn.QueryAsync<MeasurementX>(sql);
    return measurements;
});
app.MapGet("/measurementStats", async () =>
{
    var conn = new SqlConnection(connStr);
    const string sql = @"
        select 
        Year(TimeStamp) as Year
        , Max(Value) as MaxValue
        , count(Value) as ValueCount
        from Measurement
        group by Year(TimeStamp)
        ";
    var measurements = await conn.QueryAsync<MeasurementStats>(sql);
    return measurements;
});
app.MapPost("/measurement", async (MeasurementX measurement) =>
{
    var conn = new SqlConnection(connStr);
    const string sql = "insert into measurement values (@Id, @Value, @TimeStamp, @Type, @UserId)";
    var rowsAffected = await conn.ExecuteAsync(sql, measurement);
    return rowsAffected;
});
app.MapDelete("/measurement/{id}", async (Guid id) =>
{
    var conn = new SqlConnection(connStr);
    const string sql = "delete from measurement where id = @Id";
    var rowsAffected = await conn.ExecuteAsync(sql, new {Id=id});
    return rowsAffected;
});

app.UseStaticFiles();
app.Run();
