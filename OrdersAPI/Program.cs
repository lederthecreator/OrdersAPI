using OrdersAPI.DataStore;
using OrdersAPI.Interfaces;
using OrdersAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrderingContext>();

builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", corsBuilder =>
{
    corsBuilder.WithOrigins("https://localhost:44352/").AllowAnyMethod().AllowAnyHeader();
}));

RegisterServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.UseCors(options => options
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

void RegisterServices()
{
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();  
    builder.Services.AddScoped<IOrderCreateService, OrderCreateService>();
    builder.Services.AddScoped<IOrderDeleteService, OrderDeleteService>();
    builder.Services.AddScoped<IOrderUpdateService, OrderUpdateService>();
}