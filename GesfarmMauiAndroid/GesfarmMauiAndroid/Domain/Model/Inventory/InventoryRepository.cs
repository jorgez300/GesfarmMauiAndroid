
using GesfarmMauiAndroid.Domain.Database;
using GesfarmMauiAndroid.Domain.Services;
using GesfarmMauiAndroid.Domain.Utilities;

namespace GesfarmMauiAndroid.Domain.Model.Inventory
{

    public class InventoryRepository
    {
        private readonly DbContext _connection;

        public InventoryRepository(DbContext connection)
        {
            _connection = connection;
        }

        public async Task<List<InventoryItem>> GetInventoryAsync(string origin, bool force = false)
        {
            List<InventoryItem> data = new();

            if (force || (_connection.context.Table<InventoryItem>().Where(t => t.Origen == origin).Count() == 0))
            {

                if (!NetworkHelper.CheckConnection())
                {
                    await ToastHelper.ShowToast("Valide conexion a internet");
                    return data;
                }

                if (force)
                {
                    _connection.context.DeleteAll<InventoryItem>();
                    _connection.context.DeleteAll<ListPrinAct>();
                }


                List<InventoryItem> items = await CloudInventoryService.GetDataAsync(origin);

                foreach (InventoryItem item in items)
                {
                    item.Origen = origin;

                    foreach (ListPrinAct pa in item.ListPrinAct)
                    {
                        pa.Origen = origin;
                        pa.Codigo = item.Codigo;

                        _connection.context.Insert(pa);

                        item.PrinAct += pa.Dsc.ToUpper() + ", ";

                    }
                }

                _connection.context.InsertAll(items);
            }

            data = _connection.context
                .Table<InventoryItem>()
                .Where(t => t.Origen == origin)
                .ToList();


            return data;
        }

        public List<InventoryItem> FilterInventory(List<InventoryItem> items, string filter, string origin)
        {

            List<InventoryItem> inventoryItems = new List<InventoryItem>();

            foreach (InventoryItem inventory in items)
            {
                if (
                    inventory.Codigo.ToUpper().Contains(filter.ToUpper()) ||
                    inventory.Descripcion.ToUpper().Contains(filter.ToUpper()) ||
                    SearchComponents(inventory, filter)
                   )
                {
                    inventory.ListPrinAct = _connection.context
                        .Table<ListPrinAct>()
                        .Where(t => t.Codigo == inventory.Codigo)
                        .Where(y => y.Origen == origin)
                        .ToList();
                    inventoryItems.Add(inventory);
                }
            }

            return inventoryItems;

        }

        static bool SearchComponents(InventoryItem item, string filter)
        {

            bool flag = false;

            if (item.PrinAct.ToUpper().Contains(filter.ToUpper()))
            {
                flag = true;
            }

            return flag;

        }

    }
}
