
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static TraincelAPI.Resources.Enums;

namespace TraincelAPI.Models.VM
{
    public class ResultsVM
    {
        public dynamic Data { get; set; }
        public StatusCodes StatusCode { get; set; }
        public string Message { get; set; }

        public ResultsVM() { }
        public ResultsVM(dynamic data, StatusCodes statuscode, string message)
        {
            this.Data = data;
            this.StatusCode = statuscode;
            this.Message = message;
        }
            
    }
}
