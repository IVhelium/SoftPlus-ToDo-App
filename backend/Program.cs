using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SoftPlus_ToDo.Data;
using SoftPlus_ToDo.Options;
using SoftPlus_ToDo.Interfaces.Services;
using SoftPlus_ToDo.Services;
using SoftPlus_ToDo.Extensions;
using SoftPlus_ToDo.Interfaces.Repositories;
using SoftPlus_ToDo.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApi()
    .AddAuthorization()
    .AddControllers();

// Connection String
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


#region Extensions
builder.Services.AddApiIdentity();
builder.Services.AddApiAuthentication(builder.Configuration);
#endregion


#region Options
builder.Services.AddOptions<JwtOptions>().BindConfiguration(nameof(JwtOptions));
#endregion


#region Dependency Injection
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

#region Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
