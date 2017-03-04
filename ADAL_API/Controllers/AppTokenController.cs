using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ADAL_Demo;
using System.Threading.Tasks;

namespace ADAL_API.Controllers
{
    public class AppTokenController : ApiController
    {
        public async Task<string> Get()
        {
            return await AuthUtils.GetTokenInAppService("graph.microsoft.com");
        }
    }
}
