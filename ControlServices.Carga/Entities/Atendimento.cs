namespace InventoryControl.Domain.Entities
{
    public class Atendimento : Entity
    {
        public DateTime Data { get; set; }

        public int ClienteId { get; set; }

        public bool ClienteAtrasado { get; set; }

        public decimal? ValorAtendimento { get; set; }

        public decimal? ValorPago { get; set; }

        public string? ObservacaoAtendimento { get; set; }

        public int Situacao { get; set; }

        public string IdExterno { get; set; }

        public string ClienteIdExterno { get; set; }

        public Cliente Cliente { get; set; }

        public ICollection<MapServicosAtendimento> MapServicosAtendimentos { get; set; }
    }
}
