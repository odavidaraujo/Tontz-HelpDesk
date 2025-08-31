using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tontz.Domain.Entities {
    public class TB_PLANO {
        [Key]
        public int COD { get; set; }
        public string NOME { get; set; } = string.Empty;
        public decimal VALOR { get; set; }
        public int? LIMITE_CHAMADO { get; set; }
        public bool SUPORTE_PRIORITARIO { get; set; }

        // Relacionamentos
        public ICollection<TB_EMPRESA_PLANO> EMPRESAS_PLANOS { get; set; } = new List<TB_EMPRESA_PLANO>();
    }
}

