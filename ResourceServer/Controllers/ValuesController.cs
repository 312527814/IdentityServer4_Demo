using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            //var clamis = new Claim[] {
            //    new Claim("client_id","ClientCredentials"),
            //    new Claim("client_Role","admin"),
            //    new Claim("client_Name","zhngsan"),
            //};
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("k762ynnzhzW7m8CFZ90cYIiQVhD/Ljiraosr03wXfUo="));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var token2 = new JwtSecurityToken(
            //     "issuer", "audience", clamis, DateTime.Now, DateTime.Now.AddHours(1), creds
            //    );

            var creds2 = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
            var head= new JwtHeader(creds2);
            //var head = JwtHeader.Deserialize(Newtonsoft.Json.JsonConvert.SerializeObject(new {
            //    alg = "RS256",
            //    kid = "5e03711d37e07f5190f00bb260a5209f",
            //    typ = "JWT",
            //    SigningCredentials=new SigningCredentials(key,"12344")

            //}));
            var body = JwtPayload.Deserialize(Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                nbf = 1550725910,
                exp = 1550729510,
                iss = "http://localhost:5000",
                aud = new string[] { "http://localhost:5000/resources", "api1" },
                client_id = "ClientCredentials",
                client_Role = "admin",
                client_Name = "zhangsan",
                scope = new string[] { "api1" }
            }));
            var token = new JwtSecurityToken(
                 head, body
                );
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
