using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.Response
{
    public class RestResponse<T>:RestResponse
    {

        /**
         * Server response data
         */
        public T payload { get; set; }

        public RestResponse()
        {
        }

        public RestResponse(bool success, T payload):base(success)
        {
            this.payload = payload;
        }

        public RestResponse<T> Success(bool success)
        {
            this.success = success;
            return this;
        }

        public RestResponse<T> Payload(T payload)
        {
            this.payload = payload;
            return this;
        }

        public RestResponse<T> Code(int code)
        {
            this.code = code;
            return this;
        }

        public RestResponse<T> Message(String msg)
        {
            this.msg = msg;
            return this;
        }

        public static RestResponse<T> ok()
        {
            return new RestResponse<T>().Success(true);
        }

        public static RestResponse<T> ok(T payload)
        {
            return new RestResponse<T>().Success(true).Payload(payload);
        }

        public static RestResponse<T> ok(T payload, int code)
        {
            return new RestResponse<T>().Success(true).Payload(payload).Code(code);
        }

        public static RestResponse<T> fail()
        {
            return new RestResponse<T>().Success(false);
        }

        public static RestResponse<T> fail(string message)
        {
            return new RestResponse<T>().Success(false).Message(message);
        }

        public static RestResponse<T> fail(int code, String message)
        {
            return new RestResponse<T>().Success(false).Message(message).Code(code);
        }

    }

    public class RestResponse
    {
        /**
         * The request was successful
         */
        public bool success { get; set; }

        /**
         * Error message
         */
        public String msg { get; set; }

        /**
         * Status code
         */
        public int code { get; set; } = 0;

        /**
         * Server response time
         */
        public long Timestamp { get; set; }

        public RestResponse()
        {
            Timestamp = DateTimeOffset.UtcNow.Millisecond;
        }

        public RestResponse(bool success) : this()
        {
            this.success = success;
        }

      
        public RestResponse Success(bool success)
        {
            this.success = success;
            return this;
        }


        public RestResponse Code(int code)
        {
            this.code = code;
            return this;
        }

        public RestResponse Message(String msg)
        {
            this.msg = msg;
            return this;
        }

        public static RestResponse ok()
        {
            return new RestResponse().Success(true);
        }
        public static RestResponse ok( int code)
        {
            return new RestResponse().Success(true).Code(code);
        }

        public static RestResponse fail()
        {
            return new RestResponse().Success(false);
        }

        public static RestResponse fail(String message)
        {
            return new RestResponse().Success(false).Message(message);
        }

        public static RestResponse fail(int code, string message)
        {
            return new RestResponse().Success(false).Message(message).Code(code);
        }
    }
}
