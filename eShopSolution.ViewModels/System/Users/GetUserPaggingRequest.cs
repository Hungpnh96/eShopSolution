using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System.Users
{
    public class GetUserPaggingRequest : PaggingRequestBase
    {
        public string KeyWord { get; set; }
    }
}
