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
    // �`�Jservice
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
           // �����ҥ��ѮɡA�^�����Y�|�]�t WWW-Authenticate ���Y�A�o�̷|��ܥ��Ѫ��Բӿ��~��]
           options.IncludeErrorDetails = true; // �w�]�Ȭ� true�A���ɷ|�S�O����

           options.TokenValidationParameters = new TokenValidationParameters
           {
               // �z�L�o���ŧi�A�N�i�H�q "sub" ���Ȩó]�w�� User.Identity.Name
               NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
               // �z�L�o���ŧi�A�N�i�H�q "roles" ���ȡA�åi�� [Authorize] �P�_����
               RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

               // �@��ڭ̳��|���� Issuer
               ValidateIssuer = true,
               ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),

               // �q�`���ӻݭn���� Audience
               ValidateAudience = false,
               //ValidAudience = "JwtAuthDemo", // �����ҴN���ݭn��g

               // �@��ڭ̳��|���� Token �����Ĵ���
               ValidateLifetime = true,

               // �p�G Token ���]�t key �~�ݭn���ҡA�@�볣�u��ñ���Ӥw
               ValidateIssuerSigningKey = false,

               // "1234567890123456" ���ӱq IConfiguration ���o
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