using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgaAPI.DTOs
{
    public class ResponseDTO<T> where T : class
    {
        public T Data { get; set; } // Nullable<T> es válido ahora que T es un tipo de valor

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}