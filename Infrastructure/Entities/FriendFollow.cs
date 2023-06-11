using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("FriendFollow")]
    public class FriendFollow
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("User1")]
        public string UserID1 { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("User2")]
        public string UserID2 { get; set; }

        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
    }
}
