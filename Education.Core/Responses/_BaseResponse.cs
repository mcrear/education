using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class _BaseResponse<T> where T : class
    {

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
        public T Extra { get; set; }

        private _BaseResponse(bool success, string message, T Extra)
        {
            this.Extra = Extra;
            this.ErrorMessage = message;
            this.Success = success;
        }
        public _BaseResponse(T Extra) : this(true, string.Empty, Extra) { }
        public _BaseResponse(string message) : this(false, message, null) { }
    }
}
