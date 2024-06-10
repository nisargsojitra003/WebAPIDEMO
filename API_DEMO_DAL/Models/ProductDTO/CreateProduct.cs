using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_DEMO_DAL.Models.ProductDTO
{
    public class CreateProduct
    {
        public int Id { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Desciption is required")]
        public string Description { get; set; } = null!;
    }
}
