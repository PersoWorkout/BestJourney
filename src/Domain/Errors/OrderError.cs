using Domain.Abstractions;

namespace Domain.Errors;
public class OrderError
{
    public static readonly Error InvalidPayload = 
        new("Order.InvalidPayload", "The payload is invalid");
    public static readonly Error OrderPaied =
        new("Order.IsPaied", "An order already exist for this journey and you can't update them because it's paied");
    public static readonly Error PaymentError = 
        new("Order.PaymentError", "An error occured during payment");
}
