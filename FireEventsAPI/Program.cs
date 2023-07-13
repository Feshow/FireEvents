using FireEvents.Data;
using FireEvents.Data.Contexts;
using FireEvents.Domain.Interfaces.IRepository;
using FireEvents.Domain.Interfaces.IServices;
using FireEvents.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region ConfigureServices
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(context =>
{
    context.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection"));
});
builder.Services.AddResponseCaching(); //Enable caching for API requests
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IEventoService, EventoService>();


builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
#endregion

#region Congigure
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(acess => acess.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion