using BusinessModels;
using Storage.Api;
using Storage.Implementation;
using UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
AddServices(builder.Services);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddServices(IServiceCollection services)
{
    Expert[] experts = new Expert[]{
        new Expert(),
        new Expert()

    };
    ExpertsStorage expertsStorage = new ExpertsStorageInMemoryImplementation(experts);
    services.AddSingleton(expertsStorage);
    services.AddScoped<ListExpertsUseCase>();
}