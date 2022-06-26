using Livros.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Livros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EbooksController : ControllerBase
    {
        private readonly IEbookService _ebook;
        public EbooksController(IEbookService ebook)
        {
            _ebook = ebook;
        } 
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _ebook.ListEbook();
                if(result is not null)
                    return Ok(result);
                else
                    return NoContent();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying a recovery the books. Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _ebook.GetById(id);
                
                if(result is not null)
                    return Ok(result);
                else
                    return NoContent();

            }
            catch (System.Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying a recovery the books. Error: {ex.Message}"
                );
            }
        } 
    }
}