Console.WriteLine("Enter your bill: ");
decimal bill = decimal.Parse(Console.ReadLine()!);

Console.WriteLine("Enter your amount: ");
decimal amount = decimal.Parse(Console.ReadLine()!);

if (amount >= bill)
{
    decimal change = amount - bill;

    Console.WriteLine($"Change: {change:F2}");
}
else
{
    decimal missing = bill - amount;

    Console.WriteLine("Not enough money");
    Console.WriteLine($"Missing: {missing:F2}");
}