using Services;
using Sqlite;
using Storage;
using UseCases.Cart;
using UseCases.Email;
using UseCases.Experts;

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
    pattern: "{controller=Booking}/{action=Index}");

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
    services.AddScoped<GetCartUseCase>();
    services.AddScoped<ListCartsUseCase>();
    services.AddScoped<ListExpertsUseCase>();
    services.AddScoped<GetExpertUseCase>();
    services.AddScoped<EditExpertUseCase>();
    services.AddScoped<AddExpertToCartUseCase>();
    services.AddScoped<RemoveExpertFromCartUseCase>();
    services.AddScoped<BookExpertUseCase>();

    services.AddSingleton(
        new EmailServiceConfiguration
        {
            ApiKey = azureConfig["SendGridApiKey"], 
            ReceiverAddress = azureConfig["BookingReceiverEmail"]
        });
    
    SqliteController sqliteExpertController = new SqliteController("Storage\\expert.db");
    SqliteController sqliteCartController = new SqliteController("Storage\\cart.db");
    ExpertsStorage expertsStorage = new ExpertStorageSqliteImplementation(sqliteExpertController);
    CartStorage cartStorage = new CartStorageSqliteImplementation(sqliteCartController);
    
    services.AddSingleton(expertsStorage);
    services.AddSingleton<EmailService, EmailServiceSendGridImplementation>();
    services.AddSingleton(cartStorage);
    
    
}