using IdentityMicroservice.repositories;
using Microsoft.Extensions.Configuration;
using Middleware;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddTransient<IEncryptor,Encryptor>();
//builder.Services.AddSingleton<IUserRepository>(serviceProvider =>
//    new UserRepository(serviceProvider.GetService<IMongoDatabase>() ?? throw new Exception("ImongoDatabase not found"))
//);
builder.Services.AddSingleton<IUserRepository>(sp =>
        new UserRepository(sp.GetService<IMongoDatabase>() ??
            throw new Exception("IMongoDatabase not found"))
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
