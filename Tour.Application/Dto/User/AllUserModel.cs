using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour.Application.Dto.User
{
    public class AllUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        
        public string PasswordHash { get; set; }
    }
}
