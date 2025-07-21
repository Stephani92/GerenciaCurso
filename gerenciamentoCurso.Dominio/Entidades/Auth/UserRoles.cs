using System.Data;

namespace gerenciamentoCurso.Dominio.Entidades.Auth
{
	public class UserRoles
	{
        public UserRoles()
        {
        }

        public UserRoles(Guid usuarioId, string role)
		{
            UsuarioId = usuarioId;
			RoleNome = role;
        }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string RoleNome { get; set; }
        public Roles Role { get; set; }
    }
}