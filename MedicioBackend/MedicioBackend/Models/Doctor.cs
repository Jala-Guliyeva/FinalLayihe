using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicioBackend.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required] 
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    }
}
