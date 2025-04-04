using EmployeeManagement.Api;
using EmployeeManagement.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace EmployeeManagement.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly EmployeeDbContext _context;

        public AuthService(EmployeeDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var result = new LoginResponse() { };
            var user = _context.Users.FirstOrDefault(x => x.UserName == request.Username);
            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Không tìm thấy user"
                };
            }
            if (!BC.Verify(request.Password, user.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Mật khẩu không chính xác"
                };
            }
            var token = CreateToken(user);
            return new LoginResponse
            {
                Message = "Đăng nhập thành công",
                Success = true,
                Token = token,
            };
        }

        private string CreateToken(User user)
        {
            var jwtSetting = _configuration.GetSection("JwtSettings");
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName)
            };

            var value = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSetting["SecretKey"]!));

            var creds = new SigningCredentials(value, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: jwtSetting["Issuer"],
                audience: jwtSetting["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }


    }
}
