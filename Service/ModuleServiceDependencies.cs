using Service.AuthService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.OAuthService.Services.Contract;
using Service.OAuthService.Services;

namespace Service;

public static class ModuleServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IOrderItemService, OrderItemService>();
        services.AddTransient<IReviewService, ReviewService>();
        services.AddTransient<IPaymobService, PaymobService>();
        services.AddTransient<IShippingAddressService, ShippingAddressService>();
        services.AddTransient<IApplicationUserService, ApplicationUserService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IAuthorizationService, AuthorizationService>();
        services.AddTransient<IAuthGoogleService, AuthGoogleService>();
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IEmailsService, EmailsService>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<INotificationsService, NotificationsService>();
        services.AddScoped<ICartService, CartService>();
        services.AddPaymobCashIn(options =>
        {
            options.ApiKey = configuration["Paymob:ApiKey"];
            options.Hmac = configuration["Paymob:HMAC"];
        });
        return services;
    }
}
