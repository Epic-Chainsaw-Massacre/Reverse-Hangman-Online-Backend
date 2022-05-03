using Microsoft.AspNetCore.Mvc;
using Reverse_Hangman_Online_Backend.Classes;
using System.Web.Http;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "https://localhost:7054")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



// Properties
List<string> _differentLettersInWord = new List<string>();

// ROUTING -> https://localhost:7054
app.MapGet("/test", () =>
{
    WordClass word = new WordClass("hello", true);
    return word;
}).WithName("GetTest");

app.MapGet("/test2", ([System.Web.Http.FromBody] string word) =>
{
    WordClass word1 = new WordClass(word, false);
    return word1;
}).WithName("GetTest2");

app.MapGet("/test3", ([FromUri] string word) =>
{
    WordClass word1 = new WordClass(word, false);
    return word1;
}).WithName("GetTest3");

app.MapGet("/Lives", ([FromUri] string word) =>
{
    _differentLettersInWord = WordClass.CountDifferentLetters(word);
    return _differentLettersInWord.Count;
}).WithName("GetLives");

app.Run();