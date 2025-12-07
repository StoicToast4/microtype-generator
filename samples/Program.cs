using samples;

// Test basic microtype functionality
var userId = new UserId("user-123");
Console.WriteLine($"UserId: {userId}");

var productId = new ProductId(42);
Console.WriteLine($"ProductId: {productId}");

var orderId = new OrderId(Guid.NewGuid());
Console.WriteLine($"OrderId: {orderId}");

// Test equality
var userId2 = new UserId("user-123");
Console.WriteLine($"UserId equality: {userId.Equals(userId2)}");
Console.WriteLine($"UserId == operator: {userId == userId2}");

// Test custom ToString
var customStr = new CustomToString(Guid.NewGuid());
Console.WriteLine($"CustomToString: {customStr}");

// Test implicit conversions
string userIdString = userId;
UserId userId3 = "user-456";
Console.WriteLine($"Implicit conversion: {userIdString} -> {userId3}");

// Test sealed type
var accountId = new AccountId(999L);
Console.WriteLine($"AccountId (sealed): {accountId}");

// Test struct
var price = new Price(99.99m);
Console.WriteLine($"Price (struct): {price}");

Console.WriteLine("\n✅ All samples compiled and work correctly!");
