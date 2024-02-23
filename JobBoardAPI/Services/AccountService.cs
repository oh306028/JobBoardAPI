using AutoMapper;
using JobBoardAPI.Entities;
using JobBoardAPI.Exceptions;
using JobBoardAPI.Models;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobBoardAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly JobOffertsDbContext _dbContext;

        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(JobOffertsDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
           _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
           _authenticationSettings = authenticationSettings;
        }


        public void RegisterUser(RegisterUserDto dto)
        {

            var foundUser = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (!(foundUser is null))
                throw new BadRequestException("Email already used");  


            var newUser = new User()
            {
                Email = dto.Email,
                RoleId = dto.RoleId,              
               
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;


            if (!(dto.Name is null))
                newUser.Name = dto.Name;


            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();


        }


        public List<UserDto> GetUsers()
        {
            var users = _dbContext.Users.ToList();
            var results = _mapper.Map<List<UserDto>>(users);

            return results;
        }



        public string Login(LoginUserDto dto)
        {

            var userLogging = _dbContext.Users
                .Include(r => r.Role)
                .FirstOrDefault(e => e.Email == dto.Email);

            if (userLogging is null)
                throw new BadRequestException("Invalid email or password");


            var result = _passwordHasher.VerifyHashedPassword(userLogging, userLogging.PasswordHash, dto.Password);

            if (result != PasswordVerificationResult.Success)
                throw new BadRequestException("Invalid email or password");


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userLogging.Id.ToString()),
                new Claim(ClaimTypes.Name, userLogging.Name.ToString()),
                new Claim(ClaimTypes.Role, userLogging.Role.Name)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);


            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);


            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);

        }


    }
}
