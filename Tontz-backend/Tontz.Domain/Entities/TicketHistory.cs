using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontz.Domain.Entities;

namespace Tontz.Domain.Entities {
    public class TB_HISTORICO_CHAMADO {
        [Key]
        public int COD { get; set; }
        public string ALTERACAO { get; set; } = string.Empty;
        public DateTime DATA_ALTERACAO { get; set; }

        public string ID_RESPONSAVEL { get; set; } = string.Empty;
        public int ID_CHAMADO { get; set; }

        // Relacionamentos
        public TB_FUNCIONARIO RESPONSAVEL { get; set; }
        public TB_CHAMADO CHAMADO { get; set; }
    }
}
