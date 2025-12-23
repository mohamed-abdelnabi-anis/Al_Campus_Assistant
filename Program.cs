
using Microsoft.EntityFrameworkCore;
using Al_Campus_Assistant.Data;

var builder = WebApplication.CreateBuilder(args);

// إضافة DbContext مع SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// إضافة الـControllers
builder.Services.AddControllers();

// إعدادات الـCORS للسماح للفلاتر/Flutter بالاتصال
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterApp",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// إضافة Swagger للتوثيق
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// استخدام HTTPS Redirection
app.UseHttpsRedirection();

// استخدام الـCORS
app.UseCors("AllowFlutterApp");

// استخدام Swagger فقط في بيئة التطوير
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedData.Initialize(dbContext);
}
app.UseAuthorization();
app.MapControllers();

app.Run();



