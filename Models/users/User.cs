using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClimaGloboApi.Models.users
{
    public class User
    {
        [Key]
        public int codigo { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public string email { get; set;}
    }
}