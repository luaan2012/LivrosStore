namespace Livros.Domain.Models
{
    public class Ebook
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Author { get; set; }
        public double Evaluation { get; set; }
        public string? Synopsis { get; set; }
        public string? Description { get; set; }
        public DateTime? YearLaunch { get; set; }
        public int Code { get; set; }
        public string? Image { get; set; }
        public int UserId { get; set; }

    }
}