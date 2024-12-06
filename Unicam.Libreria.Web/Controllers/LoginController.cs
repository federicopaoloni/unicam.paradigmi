using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Unicam.Libreria.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        [Route("{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            //STEP 1 Verifico se username e password sono corrette
            if (username!="TEST" || password != "TEST")
            {
                throw new Exception("Utente non valido");
            }

            var bytes = Encoding.UTF8.GetBytes("01234567890123456789012345678901");
            var securityKey = new SymmetricSecurityKey(
               bytes
               );
            var credentials = new SigningCredentials(securityKey
                , SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("name", "Nome di prova"));
            claims.Add(new Claim("surname", "Cognome di prova"));
            var securityToken = new JwtSecurityToken("unicam.libreria.web"
              , "api://unicam.libreria.web"
              ,claims
              , expires: DateTime.Now.AddMinutes(30)
              , signingCredentials: credentials
              );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            
            return Ok(token);
        }
    }
}
