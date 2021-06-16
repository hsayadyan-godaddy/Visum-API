namespace Product.API.Models.Error
{
    public class ServerError
    {
        #region properties

        public int ErrorCode { get; set; }
        public string Message { get; set; }

        #endregion

        #region ctor

        public ServerError()
        {
        }

        public ServerError(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        #endregion
    }
}
