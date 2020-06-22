using System.ComponentModel.DataAnnotations;

namespace Vision.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public  Book Book { get; set; }
        public int BookId { get; set; }
    }
}
