using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SuspensionesAPI.Core.Dto
{
    public class DataResultListas<T>
    {
        public List<T> Data { get; set; } //recibe datos de tipo lksta en formato JSON
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
    }
    public class DataResult<D>
    {
        public D Data { get; set; } //recibe datos de tipo lksta en formato JSON
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
