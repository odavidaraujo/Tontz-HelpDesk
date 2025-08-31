using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Tontz.Domain.Entities;

namespace Tontz.Domain.Entities {
    public class TB_EMPRESA {
        [Key]
        public int COD { get; set; }
        public string NOME { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string EMAIL { get; set; } = string.Empty;
        public string? TELEFONE { get; set; }
        public DateTime DataCadastro { get; set; }

        // Relacionamentos
        public TB_EMPRESA_PLANO? EMPRESA_PLANO { get; set; }
        public ICollection<TB_FUNCIONARIO> FUNCIONARIOS { get; set; } = new List<TB_FUNCIONARIO>();
        public ICollection<TB_CHAMADO> CHAMADOS { get; set; } = new List<TB_CHAMADO>();
    }
}


