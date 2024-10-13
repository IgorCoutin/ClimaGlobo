using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClimaGloboApi.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string? email {get; set;} = string.Empty;
        
        [Required]
        public string? senha {get; set;} = string.Empty;
    }
}