using System.Reflection;
using System.Text;
using Messenger.Core.Infrastructure;
using Messenger.Core.IServices;
using Messenger.Core.Services;
using Messenger.db.Bridge;
using Messenger.db.EF;
using Messenger.Hubs;
using Messenger.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

#region ConfigureServices

builder.Services.AddDbContext<MessengerContext>(options =>
{
    Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableServiceProviderCaching(false);
});
builder.Services.AddAutoMapper(typeof(UserMapperProfile).GetTypeInfo().Assembly);
var signingKey = new SigningSymmetricKey(builder.Configuration.GetValue<string>("JWTOptions:SecretKey"));
builder.Services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddScoped<ISecurityContext, SecurityContextService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, SignalRUserIdProvider>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWTOptions"));
#endregion


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();



var app = builder.Build();

app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSwagger();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");


app.Run();