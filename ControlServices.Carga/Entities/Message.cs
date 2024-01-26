namespace InventoryControl.Domain.Entities
{
    public class Message : Entity
    {
        public string? JsonMessage { get; set; }
        public int? TypeMessage { get; set; }
        public int? Situacao { get; set; }
    }
}
