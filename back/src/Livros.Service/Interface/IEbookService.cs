using Livros.Domain.Models;

namespace Livros.Service.Interface
{
    public interface IEbookService
    {
        Task<IEnumerable<Ebook>> ListEbook();
        Task<Ebook> GetById(int id);

    }
}