using System.Text.Json;
using BusinessModels;
using Services.Implementation;
using Storage.Implementation;
using UseCases.Cart;
using UseCases.Email;
using UseCases.Experts;
using WebSite.Helpers;

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
    pattern: "{controller=BookExpert}/{action=Index}");

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
    
    Expert[] experts = JsonSerializer.Deserialize<Expert[]>(ReadJsonFromFileHelper.ReadJsonFromTextFile(azureConfig["ExpertsJsonFilePath"]));
    ExpertsStorage expertsStorage = new ExpertsStorageInMemoryImplementation(experts);
    services.AddSingleton(expertsStorage);
    CartStorage cartStorage = new CartStorageInMemoryImplementation();
    services.AddSingleton(cartStorage);
    
    services.AddScoped<GetCartUseCase>();
    services.AddScoped<ListCartsUseCase>();
    
    services.AddScoped<ListExpertsUseCase>();
    services.AddScoped<GetExpertUseCase>();
    services.AddScoped<AddExpertToCartUseCase>();
    services.AddScoped<RemoveExpertFromCartUseCase>();
    services.AddScoped<BookExpertUseCase>();
}