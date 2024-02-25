using MathAppHttp.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async(HttpContext context) =>
{
    //pega do corpo da requisição e transforma a query string em um dict
    StreamReader reader = new StreamReader(context.Request.Body);
    string bodyString = await reader.ReadToEndAsync();
    Dictionary<string, StringValues> bodyDict = QueryHelpers.ParseQuery(bodyString);
    Calculation calculation = new();

    //aqui ele pega da prória query string
    if (!context.Request.Query.ContainsKey("firstNumber")
        || !context.Request.Query.ContainsKey("secondNumber")
        || !context.Request.Query.ContainsKey("operator"))
    {
        await context.Response.WriteAsync("Preencha com o parametros firstNumber, secondNumber e operator");
        await context.Response.WriteAsync("Exemplo: '?firstNumber=2&secondNumber=5&operator=add'");
    }

    var isFirst = double.TryParse(context.Request.Query["firstNumber"], out double firstNumber);
    var isSecond = double.TryParse(context.Request.Query["secondNumber"], out double secondNumber);
    var operation = context.Request.Query["operator"].ToString();

    await context.Response.WriteAsync($"{calculation.Calc(firstNumber,secondNumber,operation)}");
    






});

app.Run();
