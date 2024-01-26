namespace InventoryControl.Domain.Entities
{
    public class Custo : Entity
    {
        public DateTime Data { get; set; }

        public decimal Valor { get; set; }

        public string Descricao { get; set; }
    }
}
