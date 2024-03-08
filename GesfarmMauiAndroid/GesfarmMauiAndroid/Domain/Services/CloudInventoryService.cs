using GesfarmMauiAndroid.Domain.Model.Inventory;
using Newtonsoft.Json;

namespace GesfarmMauiAndroid.Domain.Services
{
    public static class CloudInventoryService
    {
        public static async Task<List<InventoryItem>> GetDataAsync(string Origin)
        {


            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync($"https://almcomp.blob.core.windows.net/farmafd/Inventario/{Origin}.json");
            string content = await response.Content.ReadAsStringAsync();
            List<InventoryItem> result = JsonConvert.DeserializeObject<List<InventoryItem>>(content);

            return result;
        }

    }
}
