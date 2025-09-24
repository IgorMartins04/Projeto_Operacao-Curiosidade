namespace ProjetoForlogic.Models
{
    public class Log
    {
        public int Id {  get; set; }
        public int? RegisterId { get; set; } 
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Action {  get; set; } = string.Empty;
        public DateTime DateRegister { get; set; }
    }

    public class LogView
    {
        public int? RegisterId { get; set; }
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Action { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
