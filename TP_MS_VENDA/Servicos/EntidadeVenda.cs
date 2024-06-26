namespace Servicos
{
    public class EntidadeVenda
    {
        public int Id { get; set; }

        public required int CompradorId { get; set; }

        public required int EventoId { get; set; }

        public int Quantidade { get; set; }

        public Boolean Estornado { get; set; } = false;

    }

}
