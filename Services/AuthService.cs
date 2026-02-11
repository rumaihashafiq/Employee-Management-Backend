using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

//         public string CreateToken(Employee employee)
//         {
//             // var claims = new List<Claim>
//             // {
//             //     new Claim(ClaimTypes.Name, employee.Name),
//             //     new Claim(ClaimTypes.Email, employee.Email),
//             //     new Claim("EmployeeId", employee.Id.ToString()),
//             //     new Claim(ClaimTypes.Role, "Employee") // OK even for now
//             // };
// var claims = new List<Claim>
// {
//     // new Claim(ClaimTypes.Name, employee.Name),
//     // new Claim(ClaimTypes.Email, employee.Email),
//     // CHANGE THIS LINE: Use ClaimTypes.NameIdentifier
//     new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()), 
//     new Claim(ClaimTypes.Role, employee.Email.ToLower().Contains("admin") ? "Admin" : "Employee") 
// };
//             var key = new SymmetricSecurityKey(
//                 System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
//             );

//             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//             var token = new JwtSecurityToken(
//                 issuer: _config["Jwt:Issuer"],
//                 audience: _config["Jwt:Audience"],
//                 claims: claims,
//                 expires: DateTime.UtcNow.AddDays(1),
//                 signingCredentials: creds
//             );

//             return new JwtSecurityTokenHandler().WriteToken(token);
//         }

public string CreateToken(Employee employee)
{
    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, employee.Id.ToString()), // Standard user id
        new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),   // .NET user id
        new Claim(ClaimTypes.Name, employee.Name),
        new Claim(ClaimTypes.Email, employee.Email),
        new Claim(ClaimTypes.Role, employee.Role) // MUST come from DB
    };

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
    );

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(5),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}









    }
}
