using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimaGloboApi.Dtos.User
{
    public class LoggedUserDto
    {
        public int codigo { get; set; }
        public string nome { get; set; }
        public string email {get; set;}
    }
}