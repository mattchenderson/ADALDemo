using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADAL_Demo;

namespace ADAL_Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            // I know using .Result makes me a terrible person, and for this I apologize
            string graphToken = AuthUtils.GetTokenInAppService("https://graph.microsoft.com").Result;
        }
    }
}
