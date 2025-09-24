namespace ProjetoForlogic.Helpers
{
    public class ReturnValidation
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public static ReturnValidation Ok()
        {
            return new ReturnValidation { Success = true };
        }

        public static ReturnValidation Erro(string message)
        {
            return new ReturnValidation { Success = false, Message = message };
        }
    }
}

   