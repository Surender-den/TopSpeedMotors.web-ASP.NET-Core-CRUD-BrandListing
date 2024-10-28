using System.ComponentModel.DataAnnotations;
namespace TopSpeedMotors.Web.Models
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }= Guid.NewGuid();
        [Required]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Establish Year")]
        public int EstablishYear { get; set; }

        [Display(Name = "Brand Logo")]
        public string BrandLogo { get; set; } = string.Empty;

    }
}
