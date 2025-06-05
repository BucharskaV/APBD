using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tutorial10.Application.Contracts.Requests;
using Tutorial10.Application.Contracts.Responses;
using Tutorial10.Application.Exceptions;
using Tutorial10.Application.Services.Abstraction;
using Tutorial10.Core.Models;
using Tutorial10.Helpers;

namespace Tutorial10.Application.Services.Implementation;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly ClinicDBContext _context;

    public UserService(IConfiguration configuration, ClinicDBContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task RegisterUserAsync(RegisterUserRequest request)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(e => e.Username == request.Username);
        if (userExists != null)
            throw new UserAlreadyExistsException(request.Username);
        
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(request.Password);
        var user = new AppUser()
        {
            Username = request.Username,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.UtcNow.AddDays(1)
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<TokenResponse> LoginUserAsync(LoginUserRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(e => e.Username == request.Login);
        if (user == null)
            throw new UserNotFoundException(request.Login);
        
        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(request.Password, user.Salt);
        if (passwordHashFromDb != curHashedPassword)
        {
            throw new InvalidPasswordException();
        }
        var userclaim = new[]
        {
            new Claim(ClaimTypes.Name, $"{user.Username}"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "client")
        };
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:SecretKey"]));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Auth:ValidIssuer"],
            audience: _configuration["Auth:ValidAudience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials
        );
        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.UtcNow.AddDays(1);
        await _context.SaveChangesAsync();

        return new TokenResponse()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = user.RefreshToken
        };
    }

    public async Task<TokenResponse> RefreshTokensAsync(RefreshTokenRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }
        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
        var userclaim = new[]
        {
            new Claim(ClaimTypes.Name, $"{user.Username}"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "client")
        };
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:SecretKey"]));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Auth:ValidIssuer"],
            audience: _configuration["Auth:ValidAudience"],
            claims: userclaim,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: credentials
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.UtcNow.AddDays(1);
        await _context.SaveChangesAsync();

        return new TokenResponse()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = user.RefreshToken
        };
    }
}