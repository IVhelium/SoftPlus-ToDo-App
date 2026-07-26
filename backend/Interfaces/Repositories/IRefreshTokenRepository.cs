using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(
            RefreshTokenModel refreshToken,
            CancellationToken cancellationToken
        );

        Task<RefreshTokenModel?> GetByTokenAsync(
            string token, 
            CancellationToken cancellationToken
        );

        Task DeleteAsync(
            RefreshTokenModel refreshToken,
            CancellationToken cancellationToken
        );
    }
}