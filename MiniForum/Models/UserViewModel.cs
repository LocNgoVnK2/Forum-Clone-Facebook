using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace MiniForum.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

 
            [DisplayName("User ID")]
            public string UserID { get; set; }

            [DisplayName("Name")]
            public string Name { get; set; }

            [DisplayName("Avatar")]
            public string Avatar { get; set; }

            [DisplayName("PassWord")]
            public string PassWord { get; set; }
        
    }
}
