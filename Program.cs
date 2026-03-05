using System;

// Configure and build web application.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Start randomizer object
var random = new Random();

var greeting_strings = new[]
{
    "Hello", "Hi", "Welcome", "Greetings"
};

app.MapGet("/greeting", (HttpRequest request) =>
{
    // Greeting has a random start
    int index = random.Next(0, greeting_strings.Length);
    string greeting = greeting_strings[index];

    var name = request.Query["name"];

    // Concatenates name or defaults to "visitor"
    greeting += (name.Count == 0) ? ", visitor!" : $", {name[0]}!";

    return greeting;
})
.WithName("GetGreeting");

app.Run();