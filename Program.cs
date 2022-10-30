using Microsoft.EntityFrameworkCore;
using Universidade_Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<UniversidadeContext>(opt =>
    opt.UseMySql("server=localhost;port=3306;Database=Universidade;user=root;password=root;", new MySqlServerVersion(new Version(8, 0, 11))));

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();