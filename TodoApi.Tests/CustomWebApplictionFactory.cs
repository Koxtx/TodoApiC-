using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

namespace TodoApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
{
var descriptor = services.SingleOrDefault(
    d => d.ServiceType == typeof(DbContextOptions<TodoDbContext>));
if (descriptor != null)
{
services.Remove(descriptor);
}
services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite("Data Source=test_todos.db"));

var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

db.Database.Migrate();

});


    }
}