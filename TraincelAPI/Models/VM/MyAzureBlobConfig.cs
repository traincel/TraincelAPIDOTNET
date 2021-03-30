using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Models.VM
{
    public class MyAzureBlobConfig
    {
        public string StorageConnection { get; set; }
        public string Container { get; set; }
    }
}
