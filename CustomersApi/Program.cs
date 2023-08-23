using Middlewares.ExceptionHandling;
using CustomersApi;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Just For the sake of the demo
// In Production all the options should be specified for security purposes
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();