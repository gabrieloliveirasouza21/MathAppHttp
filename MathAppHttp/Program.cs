using MathAppHttp.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async(HttpContext context) =>
{
    //pega do corpo da requisição e transforma a query string em um dict
    context.Response.ContentType = "text/html";
    StreamReader reader = new StreamReader(context.Request.Body);
    string bodyString = await reader.ReadToEndAsync();
    Dictionary<string, StringValues> bodyDict = QueryHelpers.ParseQuery(bodyString);
    Calculation calculation = new();

    #region Query String
    //aqui ele pega da prória query string
    //if (!context.Request.Query.ContainsKey("firstNumber")
    //    || !context.Request.Query.ContainsKey("secondNumber")
    //    || !context.Request.Query.ContainsKey("operator"))
    //{
    //    await context.Response.WriteAsync("Preencha com o parametros firstNumber, secondNumber e operator");
    //    await context.Response.WriteAsync("Exemplo: '?firstNumber=2&secondNumber=5&operator=add'");
    //}

    //var isFirst = double.TryParse(context.Request.Query["firstNumber"], out double firstNumber);
    //var isSecond = double.TryParse(context.Request.Query["secondNumber"], out double secondNumber);
    //var operation = context.Request.Query["operator"].ToString();

    #endregion

    #region Request Body
    //aqui a requisição é tratada pelo body dela

    if (bodyDict.ContainsKey("firstNumber")
        && bodyDict.ContainsKey("secondNumber")
        && bodyDict.ContainsKey("operator"))
    {
        var isFirstNumber = double.TryParse(bodyDict["firstNumber"], out double firstNumber);
        var isSecondNumber = double.TryParse(bodyDict["secondNumber"], out double secondNumber);
        var operation = bodyDict["operator"].ToString();

        await context.Response.WriteAsync(
            $"{calculation.Calc(
                firstNumber,
                secondNumber,
                operation
                )}");
    }
    else
    {
        await context.Response.WriteAsync("<h3>Preencha com o parametros firstNumber, secondNumber e operator</h3>");
        await context.Response.WriteAsync("<h5>Exemplo: '?firstNumber=2&secondNumber=5&operator=add'</h5>");
    }
    #endregion
});

app.Run();
