using FooCargo.CoreModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FooCargoWebUI.APIClient
{
    public partial class ApiClient
    {
        public async Task<List<Airport>> GetAirports()
        {
            return await httpClient.GetFromJsonAsync<List<Airport>>($"/api/lists/airports");
        }
    }
}
