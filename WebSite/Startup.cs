using BusinessModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Services;
using Services.Implementation;
using Storage.Api;
using Storage.Implementation;
using UseCases;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from Azure App Configuration
IConfigurationRoot azureConfig = GetConfigFromAzure();

// Add services to the container.
builder.Services.AddControllersWithViews();
AddServices(builder.Services, azureConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BookExpert}/{action=Index}/{id?}");

app.Run();

static IConfigurationRoot GetConfigFromAzure()
{
    ConfigurationBuilder? configbuilder = new ConfigurationBuilder();
    configbuilder.AddAzureAppConfiguration(Environment.GetEnvironmentVariable("AzureStoreConnectionString"));
    IConfigurationRoot? config = configbuilder.Build();
    return config;
}

void AddServices(IServiceCollection services, IConfigurationRoot azureConfig)
{
    EmailService emailService = new EmailServiceSendGridImplementation(
        azureConfig["SendGridApiKey"],  
        azureConfig["BookingReceiverEmail"]);
    services.AddSingleton(emailService);
    services.AddScoped<BookExpertUseCase>();
}