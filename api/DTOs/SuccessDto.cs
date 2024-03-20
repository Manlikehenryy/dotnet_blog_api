using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.DTOs
{
    public class SuccessDto
    {
        public string Status { get;} = "Success";
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}