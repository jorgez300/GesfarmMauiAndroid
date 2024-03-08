using GesfarmMauiAndroid.Domain.ViewModel.Inventory;

namespace GesfarmMauiAndroid.Domain.Views.Inventory;

public partial class InventoryListPage : ContentPage
{
	public InventoryListPage(InventoryListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}