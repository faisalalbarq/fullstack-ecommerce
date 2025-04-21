namespace Ecommerce.API.Helpers.Errors
{
    public class ResponseAPI
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ResponseAPI(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Success",
                400 => "you have made a bad Request",
                401 => "you are not authorized",
                404 => "Resource not found",
                500 => "An Unhandled error occured",
                _ => null
            };
        }
    }
}
