using Models;
using System.Collections.Generic;

namespace Web测试.Models
{
    internal class RespondResult<T>
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
