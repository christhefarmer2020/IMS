using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Services
{
    public class ServiceMessage<T>
    {
        public T Data { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }
}