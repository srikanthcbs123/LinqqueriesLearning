using LinqqueriesLearning.Northwind_Connect;
using LinqqueriesLearning.Northwind_DB_DBConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Here builder is the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NorthwindContext>();
builder.Services.AddDbContext<NorthwindDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
