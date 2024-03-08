using CommunityToolkit.Maui;
using GesfarmMauiAndroid.Domain.Database;
using GesfarmMauiAndroid.Domain.Model.Inventory;
using GesfarmMauiAndroid.Domain.ViewModel.Home;
using GesfarmMauiAndroid.Domain.ViewModel.Inventory;
using GesfarmMauiAndroid.Domain.Views.Home;
using GesfarmMauiAndroid.Domain.Views.Inventory;
using Microsoft.Extensions.Logging;

namespace GesfarmMauiAndroid
{
    public static class MauiProgram
    {

        //apk key Gesfarm123
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            builder.Services.AddSingleton<DbContext>();
            builder.Services.AddSingleton<InventoryRepository>();

            builder.Services.AddSingleton<InventoryListPage>();
            builder.Services.AddSingleton<InventoryListViewModel>();

            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<HomeViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
