using FooCargo.CoreModels;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FooCargoWebUI.APIClient
{
    public partial class ApiClient
    {
        public async Task<List<Manifest>> GetManifests()
        {
            return await httpClient.GetFromJsonAsync<List<Manifest>>($"/api/manifests");
        }
        public async Task<Manifest> GetManifestDetails(string flightNumber, DateTime flightDate)
        {
            return await httpClient.GetFromJsonAsync<Manifest>($"/api/manifests/{flightNumber}/{flightDate.ToString("yyyy-MM-dd")}");
        }

        public async Task<bool> PostManifest(Manifest manifest)
        {
            var res = await httpClient.PostAsJsonAsync<Manifest>($"/api/manifests", manifest);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> PostShipment(Shipment shipment)
        {
            var res = await httpClient.PostAsJsonAsync<Shipment>($"/api/manifests/shipment", shipment);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteShipment(string awbNumber)
        {
            var res = await httpClient.DeleteAsync($"/api/manifests/shipment/{awbNumber}");
            return res.IsSuccessStatusCode;
        }
    }
}
