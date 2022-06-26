using Livros.Domain.Models;

namespace Livros.Data.Interface
{
    public interface IEbookRepository
    {
        Task<IEnumerable<Ebook>> ListEbooks();
        Task<Ebook> GetById(int id);
    }

}