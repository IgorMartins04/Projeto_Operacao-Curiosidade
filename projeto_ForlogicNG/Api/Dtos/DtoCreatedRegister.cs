namespace ProjetoForlogic.DTOs
{
    public class RegisterCreatedDTO
    {
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Email { get; set; } = string.Empty;

        public bool Status { get; set; }

        public string Address { get; set; } = string.Empty;

        public string? Info { get; set; }

        public string? Interests { get; set; }

        public string? Feelings { get; set; }  

        public string? MValues { get; set; }
    }

    public class RegisterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Info { get; set; }
        public string? Interests { get; set; }
        public string? Feelings { get; set; }
        public string? MValues { get; set; }
        public DateTime DateRegister { get; set; }
    }
}


