using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Models
{
    public class UpdateAunctionVM
    {
        [Required, StringLength(50, ErrorMessage = "character limit of 3 and 50 is exceeded", MinimumLength = 3)]
        public int ProductName { get; set; }

        [Required, StringLength(3000, ErrorMessage = "character limit of 3 and 1000 is exceeded", MinimumLength = 3)]
        public string Discription { get; set; }
        public byte[] Image { get; set; }
        public string Status { get; set; }
    }
}
