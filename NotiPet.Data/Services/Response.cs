using System;
using System.Net.Http;

namespace NotiPet.Data.Services
{
    public class Response<TResponse>
    {
        public bool SuccessResult { get; }
        public HttpResponseMessage ResponseMessage { get; set; } = new HttpResponseMessage();
        public TResponse Result { get; set; }

        public Response<TResponse> Success(Action<TResponse> callback)
        {
            if (SuccessResult)
                callback(Result);
            return this;
        }
        public TResult Success<TResult>(Func<TResponse, TResult> callback)
        {
            if (SuccessResult)
                return callback(Result);
            return default(TResult);
        }

        public Response<TResponse> Error(Action<HttpResponseMessage> callback)
        {
            if (!SuccessResult)
                callback(ResponseMessage);
            return this;
        }

        public static Response<TResponse> Error(HttpResponseMessage errors) =>
            new Response<TResponse>(errors);
        public static Response<TResponse> Error() =>
            new Response<TResponse>(new HttpResponseMessage());

        public static Response<TResponse> Ok(TResponse response) =>
            new Response<TResponse>(response);
        public static Response<TResponse> Void() =>
            new Response<TResponse>();
         Response(TResponse response)
        {
            SuccessResult = true;
            Result = response;
        }
        private Response()
        {
        }

         Response(HttpResponseMessage responseMessage)
        {
            SuccessResult = false;
            ResponseMessage = responseMessage;
        }
    }
}