using Livros.Domain.Models.Enum;
using Microsoft.AspNetCore.Identity;

namespace Livros.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }    
        public string? LastName { get; set; }    
        public string? Address { get; set; }
        public string? Number  { get; set; }      
        public string? Email { get; set; }   
        public string? Password { get; set; }
        public ClientType ClientType { get; set; }
    }
}