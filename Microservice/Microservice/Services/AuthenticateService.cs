using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microservice.Models;

namespace Microservice.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly string SecretKey;
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;
        private readonly (string USER, string ADMIN) Roles;
        public AuthenticateService(IUserService userService, IAdminService adminService,
            IConfiguration config)
        {
            this._userService = userService;
            this._adminService = adminService; 

            SecretKey = config["SecretKey"];
            Roles = (config["Roles:User"], config["Roles:Admin"]);
        }

        public async Task<(Response?, bool)> AuthenticateUser(UserCred userCred)
        {
            List<User> Users = await _userService.GetAll();
            User? user = Users.FirstOrDefault(option => option.Username == userCred.Username);

            if (user == null)
                return (null, false);

            if (user.Password != userCred.Password)
                return (null, true);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor 
            { 
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userCred.Username),
                    new Claim(ClaimTypes.Role, Roles.USER)
                }),
                Expires = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, 
                                TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")).AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(SecretKey)), 
                            SecurityAlgorithms.HmacSha256Signature
                        )  
            };

            JwtSecurityTokenHandler jwt = new JwtSecurityTokenHandler();
            var token = jwt.CreateJwtSecurityToken(descriptor);

            return (new Response 
                { 
                    User = user,
                    Token = jwt.WriteToken(token),
                    ValidFrom = token.ValidFrom,
                    ValidTo = token.ValidTo
                }, true);
        }

        public async Task<(AdminResponse?, bool)> AuthenticateAdmin(AdminCred adminCred)
        {
            List<Admin> Admins = await _adminService.GetAll();
            Admin? admin = Admins.FirstOrDefault(option => option.Email == adminCred.Email);

            if (admin == null)
                return (null, false);

            if (admin.Password != adminCred.Password)
                return (null, true);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor 
            { 
                Subject = new ClaimsIdentity(new Claim[] 
                { 
                    new Claim(ClaimTypes.Name, adminCred.Email),
                    new Claim(ClaimTypes.Role, Roles.ADMIN)
                }),
                Expires = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, 
                                TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")).AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey)), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler jwt = new JwtSecurityTokenHandler();
            var token = jwt.CreateToken(descriptor);

            return (new AdminResponse 
            { 
                Admin = admin,
                Token = jwt.WriteToken(token),
                ValidFrom = token.ValidFrom,
                ValidTo = token.ValidTo
            }, true);
        }
    }
}

