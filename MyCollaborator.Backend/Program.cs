using Microsoft.EntityFrameworkCore;
using MyCollaborator.Backend.Contexts;
using MyCollaborator.Backend.Hubs;
using MyCollaborator.Backend.Services;
using MyCollaborator.Backend.Services.Interfaces;
using MyCollaborator.Backend.Workers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddTransient<ICachingService, CachingService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddHostedService<Synchronizer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
    x.AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyMethod());

app.UseHttpsRedirection();
app.MapHub<ChattingHub>("/chat");

app.UseAuthorization();

app.MapControllers();

app.Run();