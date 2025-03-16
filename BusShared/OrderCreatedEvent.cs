using System.Collections.Immutable;

namespace BusShared;

public record OrderCreatedEvent(string OrderCode, string UserId, ImmutableList<OrderItem> Items);

public record OrderItem(string ProductCode, int Quantity, decimal Price);

//public record OrderCreatedEvent2
//{
//    public string OrderCode { get; init; } = null!;
//    public string UserId { get; init; } = null!;

//    public List<OrderItem> Items { get; init; } = null!;

//    public OrderCreatedEvent2(string OrderCode, string UserId, List<OrderItem> Items)
//    {
//        this.OrderCode = OrderCode;
//        this.UserId = UserId;
//        this.Items = Items;

//    }
//}

//public record OrderItem
//{
//    public string ProductCode { get; init; } = null!;
//    public int Quantity { get; init; }
//    public decimal Price { get; init; }
//}