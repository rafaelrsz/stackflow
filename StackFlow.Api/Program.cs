using Microsoft.AspNetCore.Identity;
using Rotativa.AspNetCore;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Handlers;
using StackFlow.Domain.Repositories;
using StackFlow.Domain.Utils;
using StackFlow.Infra.DataContexts;
using StackFlow.Infra.Repositories;
using StackFlow.Infra.Utils;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddCors();

builder.Services.Configure<CookiePolicyOptions>(options =>
 {
   options.CheckConsentNeeded = context => true; // consent required
   options.MinimumSameSitePolicy = SameSiteMode.None;
 });

builder.Services.AddSession(options =>
{
  options.Cookie.Name = ".StackFlow.Session";
  options.IdleTimeout = TimeSpan.FromSeconds(1000);
  options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<StackFlowDataContext, StackFlowDataContext>();

builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

builder.Services.AddTransient<StockHandler, StockHandler>();
builder.Services.AddTransient<UserHandler, UserHandler>();

builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();


var app = builder.Build();
app.UseCors(o =>
  o.AllowAnyHeader()
  .AllowAnyOrigin()
  .AllowAnyMethod()
);
app.UseSession();
app.UseMvc();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
RotativaConfiguration.Setup(app.Environment.WebRootPath);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();