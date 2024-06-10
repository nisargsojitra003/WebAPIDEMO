using API_DEMO;
using API_DEMO_BAL.InterFaces;
using API_DEMO_BAL.Services;
using API_DEMO_DAL.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var provider = builder.Services.BuildServiceProvider();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("API_DEMO_Connection_String")));
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IProductService, ProductService>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
