namespace AMicroservice.Services;

public interface IOrderService
{
    Task CreateOrder();
    Task CreateOrderWithMasstransit();
}