using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Tontz.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tontz.Domain.Entities {
    public class TB_EMPRESA_PLANO {
        [Key]
        public int COD { get; set; }
        public int ID_EMPRESA { get; set; }
        public int ID_PLANO { get; set; }
        public DateTime DATA_INICIO { get; set; }
        public DateTime? DATA_FIM { get; set; }

        // Relacionamentos
        public TB_EMPRESA EMPRESA { get; set; }
        public TB_PLANO PLANO { get; set; }
    }
}
