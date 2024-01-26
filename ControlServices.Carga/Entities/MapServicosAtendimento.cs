namespace InventoryControl.Domain.Entities
{
    public class MapServicosAtendimento : Entity
    {
        public int ServicoId { get; set; }
        public int AtendimentoId { get; set; }
        public decimal? ValorCobrado { get; set; }
        public string IdExterno { get; set; }
        public string ServicoIdExterno { get; set; }
        public string AtendimentoIdExterno { get; set; }
        public Servico Servico { get; set; }
        public Atendimento Atendimento { get; set; }
    }
}
