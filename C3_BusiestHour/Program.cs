int hoursOpen = int.Parse(Console.ReadLine()!);

int totalSales = 0;
int highestSales = -1;
int busiestHour = 0;

for (int hour = 1; hour <= hoursOpen; hour++)
{
    int sales = int.Parse(Console.ReadLine()!);

    totalSales += sales;

    if (sales > highestSales)
    {
        highestSales = sales;
        busiestHour = hour;
    }
}

Console.WriteLine($"Total sales: {totalSales}");
Console.WriteLine($"Busiest hour: {busiestHour}");
Console.WriteLine($"Sales in that hour: {highestSales}");