using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    [Serializable]
    public class GenericResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
