using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tontz.Domain.Entities {
    public class TB_PRIORIDADE {
        [Key]
        public byte COD { get; set; }
        public string NOME { get; set; } = string.Empty;

        // Relacionamentos
        public ICollection<TB_CHAMADO> CHAMADOS { get; set; } = new List<TB_CHAMADO>();
    }
}
