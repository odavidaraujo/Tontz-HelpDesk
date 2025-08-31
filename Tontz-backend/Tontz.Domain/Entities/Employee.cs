using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tontz.Domain.Entities {
    public class TB_FUNCIONARIO {
        [Key]
        public string CPF { get; set; } = string.Empty;
        public string NOME { get; set; } = string.Empty;
        public string ENDERECO { get; set; } = string.Empty;
        public string CIDADE { get; set; } = string.Empty;
        public int NUMERO { get; set; }
        public string CEP { get; set; } = string.Empty;
        public string BAIRRO { get; set; } = string.Empty;
        public string ESTADO { get; set; } = string.Empty;
        public string TELEFONE { get; set; } = string.Empty;
        public string EMAIL { get; set; } = string.Empty;
        public string SENHA { get; set; } = string.Empty;
        public byte TIPO_USUARIO { get; set; }

        public int ID_EMPRESA { get; set; }

        // Relacionamentos
        [ForeignKey("ID_EMPRESA")]
        public TB_EMPRESA EMPRESA { get; set; }
        public ICollection<TB_CHAMADO> CHAMADOS { get; set; } = new List<TB_CHAMADO>();
        public ICollection<TB_HISTORICO_CHAMADO> HISTORICOS { get; set; } = new List<TB_HISTORICO_CHAMADO>();
    }
}
