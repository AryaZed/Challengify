using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Challengify.Api.Data;
using Challengify.Api.Models;
using Challengify.Api.Services;
using Challengify.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity Configuration
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Bind Google Auth Settings from Configuration
builder.Services.Configure<GoogleAuthSettings>(
    builder.Configuration.GetSection("Authentication:GoogleAuth"));

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        var googleAuthSettings = builder.Configuration.GetSection("Authentication:GoogleAuth").Get<GoogleAuthSettings>();
        if (googleAuthSettings != null)
        {
            options.ClientId = googleAuthSettings.ClientId;
            options.ClientSecret = googleAuthSettings.ClientSecret;
        }
    });

// Register Services
builder.Services.AddScoped<GoogleAuthService>();
builder.Services.AddSingleton<ChallengeService>();
builder.Services.AddScoped<LeaderboardService>();
builder.Services.AddScoped<ChallengeCompletionService>();
builder.Services.AddScoped<AchievementService>();
builder.Services.AddScoped<StreakService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
