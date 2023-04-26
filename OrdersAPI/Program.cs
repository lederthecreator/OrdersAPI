using OrdersAPI.Context;
using OrdersAPI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderingContext>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", corsBuilder =>
{
    corsBuilder.WithOrigins("https://localhost:44352/").AllowAnyMethod().AllowAnyHeader();
}));

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