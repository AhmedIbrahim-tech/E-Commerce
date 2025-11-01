namespace Service.ServicesHandlers.Interfaces;

public interface IOrderItemService
{
    IQueryable<OrderItem> GetOrderItemsByOrderIdQueryable(Guid orderId);
}
