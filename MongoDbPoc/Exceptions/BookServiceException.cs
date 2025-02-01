namespace MongoDbPoc.Exceptions
{
    public class BookServiceException : BaseException
    {
        public BookServiceException(Exception ex, string message = "")
                  : base(ex, message ?? ex.Message)
        {
        }

        public BookServiceException()
        {
        }

        public BookServiceException(string message)
            : base(message)
        {
        }

        public BookServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
