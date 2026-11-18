using Microsoft.EntityFrameworkCore;
using Learning_WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache(); // Выделяет память под хранение сессий
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Сессия живет 30 минут бездействия
    options.Cookie.HttpOnly = true;                // Безопасность: прячет куку сессии от JS
    options.Cookie.IsEssential = true;             // Обязательно для корректной работы
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=finance.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting(); // 1. Сначала маршрутизация
app.UseSession(); // 2. СТРОГО ТУТ! Сессия должна быть инициализирована до авторизации и контроллеров
app.UseAuthentication(); 
app.UseAuthorization(); 
app.MapControllers();
app.UseHttpsRedirection();
app.Run();