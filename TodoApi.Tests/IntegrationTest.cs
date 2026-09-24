using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

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

var content = await response.Content.ReadAsStringAsync();

using var json = JsonDocument.Parse(content);

Assert.Equal(JsonValueKind.Array, json.RootElement.ValueKind);

}

[Fact]
public async Task CreateTodo_Should_Return_Created()
    {
        var client = _factory.CreateClient();

        var todo = new
{
    Title = "Tester mon API",
    Description = "Créer mon premier test d'intégration"
};

var response = await client.PostAsJsonAsync("/api/todos", todo);


Assert.Equal(HttpStatusCode.Created, response.StatusCode);

var content = await response.Content.ReadAsStringAsync();

using var json = JsonDocument.Parse(content);

Assert.Equal(JsonValueKind.Object, json.RootElement.ValueKind);

var title = json.RootElement.GetProperty("title").GetString();

Assert.Equal("Tester mon API", title);

var id = json.RootElement.GetProperty("id").GetInt32();

Assert.True(id > 0);



    }

}