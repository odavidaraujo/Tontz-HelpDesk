using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tontz.Domain.Entities {
    public class TB_SETOR {
        [Key]
        public int COD { get; set; }
        public string NOME { get; set; } = string.Empty;
    }
}