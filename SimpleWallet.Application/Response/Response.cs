namespace SimpleWallet.Application.Response
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; }
        public List<ErrorDetail>? Details { get; set; }

        public Response()
        {
            Success = true;
            Details = [];
        }

        public Response(T data)
        {
            Success = true;
            Data = data;
            Details = null;
        }

        public Response(string message)
        {
            Success = false;
            Message = message;
            Details = null;
            Data = default;
        }
        public Response(string message, T data)
        {
            Success = false;
            Message = message;
            Data = data;
            Details = null;
        }

        public Response(string message, List<ErrorDetail> details)
        {
            Success = false;
            Message = message;
            Details = details;
            Data = default;
        }

        public static Response<T> Ok(T data, string message = null)
        {
            var response = new Response<T>
            {
                Success = true,
                Data = data,
                Message = "Success",
                Details = []
            };

            if (message != null)
            {
                response.Message = message;

            }

            return response;
        }

        public static Response<T> Fail(string message, List<ErrorDetail>? details = null, T? data = default)
        {
            var response = new Response<T>
            {
                Success = false,
                Message = message,
                Data = data,
                Details = details
            };

            return response;
        }
    }
}