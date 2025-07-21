namespace gerenciamentoCurso.Dominio.Entidades.Auth
{
	public class Roles
	{
		public Roles()
		{
		}

		public Roles(string name)
		{
			Nome = name;
		}
        public string Nome { get; set; } = string.Empty;
        public List<UserRoles> UserRoles { get; set; } = new();
    }
}