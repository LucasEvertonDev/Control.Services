namespace InventoryControl.Domain.Entities
{
    public class Servico : Entity
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string IdExterno { get; set; }

        public ICollection<MapServicosAtendimento> MapServicosAtendimentos { get; set; }
    }
}
