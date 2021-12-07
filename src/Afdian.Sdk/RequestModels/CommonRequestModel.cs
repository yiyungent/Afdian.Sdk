using System;
using System.Collections.Generic;
using System.Text;

namespace Afdian.Sdk.RequestModels
{
    public class CommonRequestModel
    {
        public string user_id { get; set; }

        public string @params { get; set; }

        public long ts { get; set; }

        public string sign { get; set; }

    }
}
