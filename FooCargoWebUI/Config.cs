using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooCargoWebUI
{
    public static class Config
    {
#if DEBUG
        public const string ApiHost = "http://localhost:5000";
#else
        public const string ApiHost = "https://cargoapi.mea.com.lb";
#endif
    }
}
