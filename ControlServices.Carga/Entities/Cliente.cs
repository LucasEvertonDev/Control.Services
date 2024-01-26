namespace InventoryControl.Domain.Entities
{
    public class Cliente : Entity
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string IdExterno { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<Atendimento> Atendimento { get; set; }
    }
}
