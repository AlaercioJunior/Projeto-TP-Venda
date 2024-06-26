namespace Servicos  
{
    public class EntidadeComprador
    {

        public int Id { get; set; }
        public required string CPF { get; set; }

        public required string Nome { get; set; }

        public required string Sexo { get; set; }

        public DateTime DtNasc { get; set; }

        public Boolean Cancelado { get; set; } = false;

    }
}