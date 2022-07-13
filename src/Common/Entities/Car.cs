using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class Car
    {
        //ANNOTATIONS
        [Key]
        //WHEN GENERATED IN DB IT WILL BE GENERATED AS IDENTTIY UNIQUE KEY
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand Name is required.")]
        [MaxLength(25)]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "Year is mandatory.")]
        [MaxLength(4)]
        public string ManufactureYear { get; set; }

        [Required(ErrorMessage = "Model is mandatory.")]
        [MaxLength(25)]
        public string Model { get; set; }

        public string Summary { get; set; }

    }
}
