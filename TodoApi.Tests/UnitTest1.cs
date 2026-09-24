namespace TodoApi.Tests;
using TodoApi.Models;
using TodoApi.DTOs;
using System.ComponentModel.DataAnnotations;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
 var todo = new Todo
        {
            Title = "Test",
            Description = "Description de test",
            IsCompleted = false,
            CreatedAt = new DateTime(2026, 1, 1)
        };

        Assert.Equal("Test", todo.Title);
       Assert.False( todo.IsCompleted);
        Assert.Equal("Description de test", todo.Description);

    }

    [Fact]
public void Todo_Should_Be_Completed()
{
 var todo = new Todo
        {
            Title = "Test",
            IsCompleted = true,
            CreatedAt = new DateTime(2026, 1, 1)
        };

        Assert.True(todo.IsCompleted);
}


[Fact]
public void Todo_Should_Require_Title()
{
var todo = new Todo
        {
            Title = "",
            IsCompleted = false,
            CreatedAt = new DateTime(2026, 1, 1)
        };

       bool isTitleValid = !string.IsNullOrWhiteSpace(todo.Title);

        Assert.False(isTitleValid);
}

[Fact]
public void CreateTodoDto_Should_Require_Title()
{
var dto = new CreateTodoDto
{
    Title=""
};
var context = new ValidationContext(dto);
var results = new List<ValidationResult>();
bool isValid = Validator.TryValidateObject(
    dto,
    context,
    results,
    true
);
Assert.False(isValid);
Assert.NotEmpty(results);
Assert.Contains(
    results,
    result => result.MemberNames.Contains(nameof(CreateTodoDto.Title))
);
}

[Fact]
public void CreateTodoDto_Should_Reject_Title_Too_Long()
{
 var dto = new CreateTodoDto
    {
        Title = new string('a', 101),
    };
    var context = new ValidationContext(dto);
var results = new List<ValidationResult>();
bool isValid = Validator.TryValidateObject(
    dto,
    context,
    results,
    true
);
Assert.False(isValid);
Assert.NotEmpty(results);
Assert.Contains(
    results,
    result => result.MemberNames.Contains(nameof(CreateTodoDto.Title))
);
}

}


