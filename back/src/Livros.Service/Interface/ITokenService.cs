using Livros.Domain.Models;

namespace Livros.Service.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}