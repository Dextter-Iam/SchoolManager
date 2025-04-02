namespace MVCCamiloMentoria.Models
{
    public class Responsável
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public int EnderecoId { get; set; }

        public Endereco? Endereco { get; set; }  
        
        public int Telefone { get; set; }

    }
}
