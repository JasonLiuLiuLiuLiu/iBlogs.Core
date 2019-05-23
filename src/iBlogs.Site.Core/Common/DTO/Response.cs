using System;

namespace iBlogs.Site.Core.Common.DTO
{
    public class Response<T> : Response
    {
        public T Payload { get; set; }

        public Response()
        {
        }

        public Response(bool success, T payload) : base(success)
        {
            Payload = payload;
        }


        public Response<T> SetPayload(T payload)
        {
            Payload = payload;
            return this;
        }

        public Response<T> SetMessage(string msg)
        {
            Msg = msg;
            return this;
        }

        public static Response<T> Ok(T payload)
        {
            var response = new Response<T>();
            response.SetSuccess(true);
            response.SetPayload(payload);
            return response;
        }

        public static Response<T> Ok(T payload, int code)
        {
            var response = new Response<T>();
            response.SetSuccess(true);
            response.SetPayload(payload);
            response.SetCode(code);
            return response;
        }
    }

    public class Response
    {
        /**
         * The request was successful
         */
        public bool Success { get; set; }

        /**
         * Error message
         */
        public string Msg { get; set; }

        /**
         * Status SetCode
         */
        public int Code { get; set; }

        /**
         * Server response time
         */
        public long Timestamp { get; set; }

        public Response()
        {
            Timestamp = DateTimeOffset.UtcNow.Millisecond;
        }

        public Response(bool success) : this()
        {
            Success = success;
        }


        public Response SetSuccess(bool success)
        {
            Success = success;
            return this;
        }


        public Response SetCode(int code)
        {
            Code = code;
            return this;
        }

        public Response Message(string msg)
        {
            Msg = msg;
            return this;
        }

        public static Response Ok()
        {
            return new Response().SetSuccess(true);
        }
        public static Response Ok(int code)
        {
            return new Response().SetSuccess(true).SetCode(code);
        }

        public static Response Fail()
        {
            return new Response().SetSuccess(false);
        }

        public static Response Fail(string message)
        {
            return new Response().SetSuccess(false).Message(message);
        }

        public static Response Fail(int code, string message)
        {
            return new Response().SetSuccess(false).Message(message).SetCode(code);
        }
    }
}
