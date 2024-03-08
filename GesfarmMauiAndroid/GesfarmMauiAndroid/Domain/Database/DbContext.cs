using GesfarmMauiAndroid.Domain.Model.Inventory;
using SQLite;

namespace GesfarmMauiAndroid.Domain.Database
{
    public class DbContext
    {
        public readonly SQLiteConnection context;
        private const string _DB_NAME = "InventoryDB.db3";

        public DbContext()
        {
            context = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, _DB_NAME));
            context.CreateTable<InventoryItem>();
            context.CreateTable<ListPrinAct>();

        }
    }
}
