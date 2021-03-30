using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraincelAPI.Resources
{
    public class Enums
    {
        [Flags]
        public enum StatusCodes {Success = 200, NotFound = 404, InternalServerError = 500};
        public enum Roles { User = 1, Admin = 2};
    }
}
