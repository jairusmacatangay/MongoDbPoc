namespace MongoDbPoc.Exceptions
{
    public class BaseException : Exception
    {
        protected BaseException() 
        {
        }

        protected BaseException(Exception inner, string message)
            : base(message, inner) 
        { 
        }

        protected BaseException(string message)
            : base(message) 
        {
        }

        protected BaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
