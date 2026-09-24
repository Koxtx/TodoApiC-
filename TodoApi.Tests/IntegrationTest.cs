using System.Net;

namespace TodoApi.Tests;

public class IntegrationTests
{
   private readonly CustomWebApplicationFactory _factory;

    public IntegrationTests()
    {
        _factory = new CustomWebApplicationFactory();
    }

    [Fact]
    public async Task GetTest_Should_Return_Success()
    {
var client = _factory.CreateClient();
var response = await client.GetAsync("/api/test");
Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
public async Task GetTodos_Should_Return_Success()
{
var client = _factory.CreateClient();
var response = await client.GetAsync("/api/todos");
Assert.Equal(HttpStatusCode.OK, response.StatusCode);
}
}