namespace ProjetoForlogic.Helpers
{
    public class ReturnOp<T>
    {
        public bool Success {  get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ReturnOp<T> Ok (T data) =>
            new ReturnOp<T> { Success = true, Data = data };

        public static ReturnOp<T> Erro (string message) =>
            new ReturnOp<T> { Success  = false, Message = message };

    }
}
