using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalsWebApp.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }
        [Required(ErrorMessage = "You have to enter data")]
        [MaxLength(200, ErrorMessage = "Data to long")]
        public string Name { get; set; }
        [MaxLength(200, ErrorMessage = "Data to long")]
        public string Description { get; set; }
        [Required(ErrorMessage = "You have to enter data")]
        [MaxLength(200, ErrorMessage = "Data to long")]
        public string Category { get; set; }
        [Required(ErrorMessage = "You have to enter data")]
        [MaxLength(200, ErrorMessage = "Data to long")]
        public string Area { get; set; }
    }
}
