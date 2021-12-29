using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SuspensionesAPI.Core.Dto
{
    public class DataResult<T>
    {
        public List<T> Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
