using SQLite;

namespace GesfarmMauiAndroid.Domain.Model.Inventory
{
    public class InventoryItem
    {
        public string Origen { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Existen { get; set; }
        public int Minimo { get; set; }
        public int Maximo { get; set; }
        public double CostoBs { get; set; }
        public double CostoUsd { get; set; }
        public double PrecioBs { get; set; }

        public string PrinAct { get; set; } = string.Empty;

        [Ignore]
        public List<ListPrinAct> ListPrinAct { get; set; } = new();
    }
}
