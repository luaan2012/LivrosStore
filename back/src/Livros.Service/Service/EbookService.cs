using Livros.Data.Interface;
using Livros.Domain.Models;
using Livros.Service.Interface;

namespace Livros.Service.Service
{
    public class EbookService : IEbookService
    {
        private readonly IEbookRepository _ebookRepository;
        public EbookService(IEbookRepository ebookRepository)
        {
            _ebookRepository = ebookRepository;
        } 
        public Task<IEnumerable<Ebook>> ListEbook()
        {
            return _ebookRepository.ListEbooks();
        }

        public Task<Ebook> GetById(int id)
        {
            return _ebookRepository.GetById(id);
        }
    }
}