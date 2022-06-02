#nullable enable
using System;
using System.Net.Http;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public class Response<TResponse>
    {
        public bool SuccessResult { get; }
        public Error ResponseMessage { get; set; } = new Error();
        public TResponse? Result { get; set; }

        public Response<TResponse> Success(Action<TResponse>? callback)
        {
            if (SuccessResult)
                callback(Result);
            return this;
        }
        public TResult Success<TResult>(Func<TResponse, TResult>? callback)
        {
            if (SuccessResult)
                return callback(Result);
            return default(TResult);
        }

        public Response<TResponse> Error(Action<Error>? callback)
        {
            if (!SuccessResult)
                callback(ResponseMessage);
            return this;
        }

        public static Response<TResponse> Error(Error? errors) =>
            new Response<TResponse>(errors);
        public static Response<TResponse> Error() =>
            new Response<TResponse>(new Error());

        public static Response<TResponse> Ok(TResponse? response) =>
            new Response<TResponse>(response);
        public static Response<TResponse> Void() =>
            new Response<TResponse>();
         Response(TResponse? response)
        {
            SuccessResult = true;
            Result = response;
        }
        private Response()
        {
        }

         Response(Error responseMessage)
        {
            SuccessResult = false;
            ResponseMessage = responseMessage;
        }
    }
}