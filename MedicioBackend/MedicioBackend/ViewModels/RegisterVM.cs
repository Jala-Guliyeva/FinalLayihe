using System.ComponentModel.DataAnnotations;

namespace MedicioBackend.ViewModels
{
    public class RegisterVM
    {
        [Required,StringLength(20,ErrorMessage ="20den cox ola bilmez!")]
        public string FullName { get; set; }
        [Required, StringLength(20, ErrorMessage = "20den cox ola bilmez!")]
        public string UserName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password),Compare(nameof(Password))]
        public string CheckPassword { get; set; }
    }
}
