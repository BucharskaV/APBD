using Tutorial10.Application.Contracts.Requests;
using Tutorial10.Application.Contracts.Responses;

namespace Tutorial10.Application.Services.Abstraction;

public interface IUserService
{
    Task RegisterUserAsync(RegisterUserRequest request);
    Task<TokenResponse> LoginUserAsync(LoginUserRequest request);
    Task<TokenResponse> RefreshTokensAsync(RefreshTokenRequest request);
}