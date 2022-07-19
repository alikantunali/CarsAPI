using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Test
{
    public class TestCar : Car
    {
        //ANNOTATIONS

        //WHEN GENERATED IN DB IT WILL BE GENERATED AS IDENTTIY UNIQUE KEY
        [Key]
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

    }
}
