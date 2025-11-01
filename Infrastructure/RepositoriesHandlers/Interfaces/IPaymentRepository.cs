namespace Infrastructure.RepositoriesHandlers.Interfaces;

public interface IPaymentRepository : IGenericRepositoryAsync<Payment>
{
    Task<Payment?> GetPaymentByTransactionId(string transactionId);
    Task<Payment?> GetPaymentByOrderId(Guid orderId);
}
