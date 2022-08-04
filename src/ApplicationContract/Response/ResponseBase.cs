namespace ApplicationContract.Response;

    public class ResponseBase<T> : ResponseBase
    {
        public T Data { get; set; }
    }
    public class ResponseBase
    {
        public bool Success { get; set; } = true;
        public string MessageCode { get; set; }
        public string Message { get; set; }
        public string UserMessage { get; set; }
    }
