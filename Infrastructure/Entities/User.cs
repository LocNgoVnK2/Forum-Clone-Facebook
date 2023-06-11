using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [Table("User")]
    public class User
    {

        [Key]
        public string UserID { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string Avatar { get; set; }
   
    }
}
