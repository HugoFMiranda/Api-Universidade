using Microsoft.EntityFrameworkCore;
using Universidade_Api;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<UniversidadeContext>(opt =>
    opt.UseMySql("server=localhost;port=3306;Database=Universidade;user=root;password=root;", new MySqlServerVersion(new Version(8, 0, 11))));

builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("allowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();