using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Accountdto
{
    public class UserListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Department {  get; set; }
        public bool ActiveState { get; set; }
    }
}
