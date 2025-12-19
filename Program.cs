
//namespace AI_Campus_Assistant
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.

//            builder.Services.AddControllers();
//            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthorization();


//            app.MapControllers();

//            app.Run();
//        }
//    }
//}



using Microsoft.EntityFrameworkCore;
using Al_Campus_Assistant.Data;

var builder = WebApplication.CreateBuilder(args);

// إضافة DbContext مع SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// إضافة الـControllers
builder.Services.AddControllers();

// إعدادات الـCORS للسماح لـFlutter بالاتصال
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


// تنفيذ Seed Data عند بدء التشغيل
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    SeedData.Initialize(context);
}


// استخدام الـCORS
app.UseCors("AllowFlutterApp");

// استخدام Swagger في التطوير
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// بذل البيانات التلقائية
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedData.Initialize(context);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();




//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Authentication.JwtBearer; // إذا بتستخدم JWT
//using Microsoft.IdentityModel.Tokens; // إذا بتستخدم JWT
//using System.Text; // إذا بتستخدم JWT
//using Al_Campus_Assistant.Data;

//var builder = WebApplication.CreateBuilder(args);

//// 1. إضافة DbContext مع SQL Server
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// 2. إضافة الـControllers
//builder.Services.AddControllers();

//// 3. إعدادات Authentication (إذا بتستخدم JWT)
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//        };
//    });

//// 4. إعدادات الـCORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFlutterApp",
//        policy =>
//        {
//            policy.WithOrigins(
//                    "http://localhost",          // لـ iOS Simulator
//                    "http://localhost:3000",     // للتطوير المحلي
//                    "http://10.0.2.2:5000",      // لـ Android Emulator
//                    "http://127.0.0.1:5000",     // بديل لـ localhost
//                    "https://yourdomain.com"     // للإنتاج
//                )
//                .AllowAnyHeader()
//                .AllowAnyMethod()
//                .AllowCredentials(); // مهم إذا بتستخدم cookies أو tokens
//        });
//});

//// 5. إضافة Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//    {
//        Title = "Al Campus Assistant API",
//        Version = "v1",
//        Description = "API for Campus Assistant Mobile App"
//    });

//    // إضافة JWT Authentication في Swagger
//    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//        Description = "Enter 'Bearer' [space] and then your token"
//    });

//    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
//    {
//        {
//            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//            {
//                Reference = new Microsoft.OpenApi.Models.OpenApiReference
//                {
//                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});

//var app = builder.Build();

//// ============= MIDDLEWARE PIPELINE =============

//// 1. الاستخدام في التطوير
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Campus API v1");
//        c.RoutePrefix = "api-docs"; // افتح على /api-docs
//    });
//}

//// 2. إعادة التوجيه لـ HTTPS
//app.UseHttpsRedirection();

//// 3. مهم: Routing قبل الـ CORS
//app.UseRouting();

//// 4. الـ CORS
//app.UseCors("AllowFlutterApp");

//// 5. Authentication (قبل Authorization)
//app.UseAuthentication(); // ⭐ مهم!

//// 6. Authorization
//app.UseAuthorization();

//// 7. Seed Data (مرة واحدة فقط)
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<ApplicationDbContext>();
//        // تأكد أن قاعدة البيانات موجودة
//        context.Database.EnsureCreated();
//        // Seed البيانات
//        SeedData.Initialize(context);

//        Console.WriteLine("✅ Seed data completed successfully!");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"❌ Error seeding data: {ex.Message}");
//    }
//}

//// 8. Map Controllers
//app.MapControllers();

//// 9. Health Check Endpoint
//app.MapGet("/health", () => "API is running!");
//app.MapGet("/api/test", () => new
//{
//    Status = "Online",
//    Timestamp = DateTime.Now,
//    Message = "Al Campus Assistant API is working!"
//});

//Console.WriteLine("🚀 API Started successfully!");
//app.Run();