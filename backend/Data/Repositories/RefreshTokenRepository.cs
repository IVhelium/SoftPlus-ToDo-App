using Microsoft.EntityFrameworkCore;
using SoftPlus_ToDo.Interfaces.Repositories;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Data.Repositories
{
    public sealed class RefreshTokenRepository(
        AppDbContext _dbContext
    ) : IRefreshTokenRepository
    {
        public async Task AddAsync(
            RefreshTokenModel refreshToken, 
            CancellationToken cancellationToken
        )
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<RefreshTokenModel?> GetByTokenAsync(
            string token, 
            CancellationToken cancellationToken
        )
        {
            return await _dbContext.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        }

        public async Task DeleteAsync(
            RefreshTokenModel refreshToken, 
            CancellationToken cancellationToken
        )
        {
            _dbContext.RefreshTokens.Remove(refreshToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }    
    }
}