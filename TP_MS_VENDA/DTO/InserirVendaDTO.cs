using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class InserirVendaDTO
    {
        public required int CompradorId { get; set; }

        public required int EventoId { get; set; }

        public int Quantidade { get; set; }
    }
}
