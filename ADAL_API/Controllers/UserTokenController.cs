using ADAL_Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ADAL_API.Controllers
{
    [Authorize]
    public class UserTokenController : ApiController
    {
        public async Task<string> Get()
        {
            return await AuthUtils.GetTokenInAppService("graph.microsoft.com", this.Request);
        }
    }
}
