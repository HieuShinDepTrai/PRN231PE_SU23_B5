using Microsoft.EntityFrameworkCore;
using Q1.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<PE_PRN231_Sum23B5Context>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));
builder.Services.AddScoped<PE_PRN231_Sum23B5Context>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowed(origin => true);
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
