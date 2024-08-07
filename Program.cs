using FeedAPI.Models;
using FeedAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SEPVDB_Api.Helpers;
using SEPVDB_Api.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    // 注入service
    builder.Services.AddScoped<ILoginInfoService, LoginInfoService>();
    builder.Services.AddScoped<IFeedService, FeedService>();
    builder.Services.AddScoped<IClientService, ClientService>();
    builder.Services.AddScoped<IStockService, StockService>();
    builder.Services.AddScoped<IDeliveryService, DeliveryService>();
    builder.Services.AddDbContext<FeedingContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("FeedContext")));
    builder.Services.AddSingleton<JwtHelpers>();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           // 當驗證失敗時，回應標頭會包含 WWW-Authenticate 標頭，這裡會顯示失敗的詳細錯誤原因
           options.IncludeErrorDetails = true; // 預設值為 true，有時會特別關閉

           options.TokenValidationParameters = new TokenValidationParameters
           {
               // 透過這項宣告，就可以從 "sub" 取值並設定給 User.Identity.Name
               NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
               // 透過這項宣告，就可以從 "roles" 取值，並可讓 [Authorize] 判斷角色
               RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

               // 一般我們都會驗證 Issuer
               ValidateIssuer = true,
               ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),

               // 通常不太需要驗證 Audience
               ValidateAudience = false,
               //ValidAudience = "JwtAuthDemo", // 不驗證就不需要填寫

               // 一般我們都會驗證 Token 的有效期間
               ValidateLifetime = true,

               // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
               ValidateIssuerSigningKey = false,

               // "1234567890123456" 應該從 IConfiguration 取得
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SignKey")))
           };
       });
}
builder.Services.AddCors(p => p.AddPolicy("AllowAll", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));
// Add services to the container.
builder.Services.AddSignalR(e => {
    e.EnableDetailedErrors = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("CorsPolicy");


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
  
    endpoints.MapHub<ChatHub>("/chatHub");
    endpoints.MapControllers();
});

app.Run();