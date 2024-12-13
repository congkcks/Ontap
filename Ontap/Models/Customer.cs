using System.ComponentModel.DataAnnotations;

namespace Ontap.Models
{
    public partial class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Fullname { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mat Khau Khong Khop")]
        public string RePassword { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
