using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Models.Responses;

namespace Unicam.Libreria.Application.Factories
{
    public class ResponseFactory
    {
        public static BaseResponse<T> WithSuccess<T>(T data)
        {
            return new BaseResponse<T>()
            {
                Success = true,
                Data = data
            };
        }

        public static BaseResponse<T> WithError<T>(string msg)
        {
            return new BaseResponse<T>()
            {
                Success = false,
                Errors = new List<string>() { msg}
            };
        }

        public static BaseResponse<string> WithError(Exception ex)
        {
            return WithError<string>(ex.Message);
        }
    }
}
