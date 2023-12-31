﻿namespace PoolbetIntegration.API.Features.Transactions;

public class TransactionRequest
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public decimal Value { get; set; }
    public DateTime? Date { get; set; }
    public int Type { get; set; }
    public string? BetId { get; set; }
    public Guid TransactionId { get; set; }
    public string Currency { get; set; }
}
