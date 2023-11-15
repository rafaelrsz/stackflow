using StackFlow.Domain.Handlers;
using StackFlow.Domain.Repositories;
using StackFlow.Infra.DataContexts;
using StackFlow.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddCors();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<StackFlowDataContext, StackFlowDataContext>();
builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddTransient<StockHandler, StockHandler>();

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
