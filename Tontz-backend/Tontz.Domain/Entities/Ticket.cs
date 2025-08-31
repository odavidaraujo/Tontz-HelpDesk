using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tontz.Domain.Entities;

namespace Tontz.Domain.Entities {
    public class TB_CHAMADO {
        [Key]
        public int COD { get; set; }
        public string OCORRENCIA { get; set; } = string.Empty;
        public string DESCRICAO { get; set; } = string.Empty;
        public string SUG_ATENDIMENTO { get; set; } = string.Empty;

        public int ID_EMPRESA { get; set; }
        public string ID_SOLICITANTE { get; set; } = string.Empty;
        public int ID_CATEGORIA { get; set; }
        public byte ID_PRIORIDADE { get; set; }
        public byte ID_STATUS { get; set; }

        public DateTime CREATE_AT { get; set; }
        public DateTime? FINISH_AT { get; set; }
        public DateTime? UPDATE_AT { get; set; }
        public DateTime? DELETE_AT { get; set; }

        // Relacionamentos
        public TB_EMPRESA EMPRESA { get; set; }
        public TB_FUNCIONARIO SOLICITANTE { get; set; }
        public TB_CATEGORIA CATEGORIA { get; set; }
        public TB_PRIORIDADE PRIORIDADE { get; set; }
        public TB_STATUS STATUS { get; set; }

        public ICollection<TB_HISTORICO_CHAMADO> HISTORICOS { get; set; } = new List<TB_HISTORICO_CHAMADO>();
    }
}

