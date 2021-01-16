using FooCargo.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FooCargoWebUI.APIClient
{
    public partial class ApiClient
    {
        public async Task<PagedResult<Rate>> GetRates(int page)
        {
            return await httpClient.GetFromJsonAsync<PagedResult<Rate>>($"/api/rates?page={page}");
        }

        public async Task<bool> PutRate(Rate rateToEdit)
        {
            var res = await httpClient.PutAsJsonAsync($"/api/rates/{rateToEdit.MailType}/{rateToEdit.Origin}/{rateToEdit.Destination}", rateToEdit);
            return res.IsSuccessStatusCode;
        }
    }

    // in case you need to know more information about the result of the api call, wrap in the ApiClientResult instead of return simple values
    //public class ApiClientResult
    //{
    //    public int StatusCode { get; set; }
    //    public Exception Exception { get; set; }
    //}

    //public class ApiClientResult<T> : ApiClientResult
    //{
    //    public T Data { get; set; }
    //}
}
