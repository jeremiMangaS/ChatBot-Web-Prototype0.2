using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var AllowedSpesificOrigin = "_allowedSpesificOrigin";

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowedSpesificOrigin, policy =>
    {
        policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors(AllowedSpesificOrigin);
app.MapControllers();
app.Run();