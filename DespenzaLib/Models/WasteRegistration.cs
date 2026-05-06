namespace DespenzaLib.Models
{
    public class WasteRegistration
    {
        public int Id { get; set; }

        public int WareId { get; set; }
        public Wares Ware { get; set; } = null!;

        public string WareType { get; set; }

        public decimal Quantity { get; set; }

        public string Unit { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public decimal LossInCost { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
    }
}