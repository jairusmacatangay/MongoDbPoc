namespace MongoDbPoc.Exceptions
{
    public class DataGatewayException : BaseException
    {
        public DataGatewayException(Exception ex, string message = "")
                  : base(ex, message ?? ex.Message)
        {
        }

        public DataGatewayException()
        {
        }

        public DataGatewayException(string message)
            : base(message)
        {
        }

        public DataGatewayException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
