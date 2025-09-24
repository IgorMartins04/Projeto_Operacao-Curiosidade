namespace ProjetoForlogic.Models
{
    public class Register
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateTime DateRegister { get; set; }
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Info { get; set; }
        public string? Interests { get; set; }
        public string? Feelings { get; set; }
        public string? MValues { get; set; }
        public bool Removed { get; set; } = false;
    }

    public class InactiveCount
    {
        public int Total { get; set; }
        public int UserId { get; set; }
    }
}
