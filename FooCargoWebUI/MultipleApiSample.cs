using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// sample on how to inject multiple http clients
namespace FooCargoWebUI
{
    public class FooApiClient : HttpClient
    {
    }

    public class BarApiClient : HttpClient
    {

    }
}
