using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uadec.DTOs
{
    public class UserModel
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String LastNameMother { get; set; }
    }
}
