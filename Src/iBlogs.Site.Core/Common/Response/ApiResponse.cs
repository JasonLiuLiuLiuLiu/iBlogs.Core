using System;

namespace iBlogs.Site.Core.Common.Response
{
    public class ApiResponse<T> : ApiResponse
    {
        public T Payload { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(bool success, T payload) : base(success)
        {
            Payload = payload;
        }

        public ApiResponse<T> SetPayload(T payload)
        {
            Payload = payload;
            return this;
        }

        public ApiResponse<T> SetMessage(string msg)
        {
            Msg = msg;
            return this;
        }

        public static ApiResponse<T> Ok(T payload)
        {
            var response = new ApiResponse<T>();
            response.SetSuccess(true);
            response.SetPayload(payload);
            return response;
        }

        public static ApiResponse<T> Ok(T payload, int code)
        {
            var response = new ApiResponse<T>();
            response.SetSuccess(true);
            response.SetPayload(payload);
            response.SetCode(code);
            return response;
        }

        public static ApiResponse<T> Ok()
        {
            return new ApiResponse<T>() { Success = true };
        }

        public static ApiResponse<T> Ok(int code)
        {
            return new ApiResponse<T>() { Success = true, Code = code };
        }

        public static ApiResponse<T> Fail()
        {
            return new ApiResponse<T>() { Success = false };
        }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T>() { Success = false, Msg = message };
        }

        public static ApiResponse<T> Fail(int code, string message)
        {
            return new ApiResponse<T>() { Success = false, Msg = message, Code = code };
        }
    }

    public class ApiResponse
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

        public ApiResponse()
        {
            Timestamp = DateTimeOffset.UtcNow.Millisecond;
        }

        public ApiResponse(bool success) : this()
        {
            Success = success;
        }

        public ApiResponse SetSuccess(bool success)
        {
            Success = success;
            return this;
        }

        public ApiResponse SetCode(int code)
        {
            Code = code;
            return this;
        }

        public ApiResponse Message(string msg)
        {
            Msg = msg;
            return this;
        }

        public static ApiResponse Ok()
        {
            return new ApiResponse().SetSuccess(true);
        }

        public static ApiResponse Ok(int code)
        {
            return new ApiResponse().SetSuccess(true).SetCode(code);
        }

        public static ApiResponse Fail()
        {
            return new ApiResponse().SetSuccess(false);
        }

        public static ApiResponse Fail(string message)
        {
            return new ApiResponse().SetSuccess(false).Message(message);
        }

        public static ApiResponse Fail(int code, string message)
        {
            return new ApiResponse().SetSuccess(false).Message(message).SetCode(code);
        }
    }
}