using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace gerenciamentoCurso.Dominio.Entidades.Auth
{
	public class Usuario
    {
		public Usuario()
		{
		}

		public Usuario(string nome, string sobrenome, string email,DateTime dataDenascimento, string passwordHash, string cpf)
		{
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            PasswordHash = passwordHash;
            Cpf = cpf;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome deve ser preeenchido")]
        public string Nome { get;  set; }

        [Required(ErrorMessage = "O sobrenome deve ser preeenchido")]
        public string Sobrenome { get;  set; }
        [Required(ErrorMessage = "O cpf deve ser preeenchido")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A senha deve ser preeenchido")]
        public string PasswordHash { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "O email deve ser preeenchido")]
        public string Email { get;  set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ssZ}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public List<UserRoles> UserRole { get;  set; }

    }
}

