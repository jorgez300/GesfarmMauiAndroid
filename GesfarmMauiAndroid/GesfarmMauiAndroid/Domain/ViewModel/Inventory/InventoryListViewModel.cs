using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GesfarmMauiAndroid.Domain.Model.Inventory;
using GesfarmMauiAndroid.Domain.Utilities;
using System.Collections.ObjectModel;


namespace GesfarmMauiAndroid.Domain.ViewModel.Inventory
{
    public partial class InventoryListViewModel : ObservableObject
    {
        const string InventoryDatePrefKey = "InventoryDate";
        private readonly InventoryRepository _inventoryRepository;


        [ObservableProperty]
        string filterValue = string.Empty;

        [ObservableProperty]
        string lastUpdate = string.Empty;

        [ObservableProperty]
        bool busy = true;

        [ObservableProperty]
        bool originVP = false;

        [ObservableProperty]
        bool originFFD = true;

        List<InventoryItem> InventoryFFD = new();
        List<InventoryItem> InventoryVP = new();

        [ObservableProperty]
        ObservableCollection<InventoryItem> filteredInventory = new();

        public InventoryListViewModel(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [RelayCommand]
        async Task Filter()
        {

            if (FilterValue.Length <= 3)
            {

                await ToastHelper.ShowToast("Filtro debe contener mas de 3 caracteres");

                return;
            }


            if (OriginFFD)
            {
                FilteredInventory = _inventoryRepository
                    .FilterInventory(InventoryFFD, FilterValue, "FFD")
                    .ToObservableCollection<InventoryItem>();
            }
            if (OriginVP)
            {
                FilteredInventory = _inventoryRepository
                    .FilterInventory(InventoryVP, FilterValue, "VP")
                    .ToObservableCollection<InventoryItem>();
            }

        }

        [RelayCommand]
        async Task Update()
        {

            if (!NetworkHelper.CheckConnection())
            {
                await ToastHelper.ShowToast("Valide conexion a internet");
                Busy = true;
                return;
            }


            Busy = false;
            InventoryFFD = new();
            InventoryVP = new();

            FilteredInventory.Clear();

            FilterValue = string.Empty;


            InventoryFFD = await _inventoryRepository.GetInventoryAsync("FFD", true);
            InventoryVP = await _inventoryRepository.GetInventoryAsync("VP", true);
            Busy = true;

            PreferencesHelper.Set<string>(InventoryDatePrefKey, DateTime.Now.ToString());

            LastUpdate = PreferencesHelper.Get<string>(InventoryDatePrefKey, string.Empty);

        }

        [RelayCommand]
        async Task Appearing()
        {


            Busy = false;
            InventoryFFD = await _inventoryRepository.GetInventoryAsync("FFD");
            InventoryVP = await _inventoryRepository.GetInventoryAsync("VP");
            Busy = true;

            if (PreferencesHelper.Get<string>(InventoryDatePrefKey, string.Empty) == string.Empty) {
                PreferencesHelper.Set<string>(InventoryDatePrefKey, DateTime.Now.ToString());
            }

            LastUpdate = PreferencesHelper.Get<string>(InventoryDatePrefKey, string.Empty);
        }
    }
}
