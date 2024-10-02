namespace KoiAuction.API.Payloads.Requests.PaymentRequest
{
    public class UpdatePayementRequest
    {
        public double? PaymentAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? Status { get; set; }

        public string? PaymentMethod { get; set; }

        public string TransactionId { get; set; }
    }
}
