using Microsoft.AspNetCore.Identity;
using StackFlow.Domain.Handlers;
using StackFlow.Domain.Repositories;
using StackFlow.Domain.Utils;
using StackFlow.Infra.DataContexts;
using StackFlow.Infra.Repositories;
using StackFlow.Infra.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddCors();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<StackFlowDataContext, StackFlowDataContext>();

builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<StockHandler, StockHandler>();
builder.Services.AddTransient<UserHandler, UserHandler>();

builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();

var app = builder.Build();
app.UseCors(o =>
  o.AllowAnyHeader()
  .AllowAnyOrigin()
  .AllowAnyMethod()
);
app.UseMvc();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
