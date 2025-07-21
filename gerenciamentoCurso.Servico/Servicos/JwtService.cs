using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace gerenciamentoCurso.Servico.Servicos
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;

        public JwtService(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        public string Criptografar(string senha)
        {
            using var sha256 = SHA256.Create();
            byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);
            byte[] hashBytes = sha256.ComputeHash(senhaBytes);
            return Convert.ToBase64String(hashBytes);
        }
        public string GenerationJwToken(Usuario user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.Nome + " " + user.Sobrenome)
            };

            foreach (var role in user.UserRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleNome));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII
                                .GetBytes(_settings.Secret));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
