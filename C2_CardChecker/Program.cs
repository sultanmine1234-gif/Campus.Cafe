Console.WriteLine("Enter your cafe card: ");
string userCafeCard = Console.ReadLine()!;

if (userCafeCard.Length != 6)
{
    Console.WriteLine("Invalid: must be 6 characters");
}
else if (!char.IsLetter(userCafeCard[0]) ||
         !char.IsLetter(userCafeCard[1]))
{
    Console.WriteLine("Invalid: must start with two letters");
}
else if (!char.IsDigit(userCafeCard[2]) ||
         !char.IsDigit(userCafeCard[3]) ||
         !char.IsDigit(userCafeCard[4]) ||
         !char.IsDigit(userCafeCard[5]))
{
    Console.WriteLine("Invalid: last four must be digits");
}
else
{
    Console.WriteLine("Valid");
}
