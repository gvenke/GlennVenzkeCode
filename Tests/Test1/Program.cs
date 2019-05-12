using System;
using System.Linq;
using Test1.Entities;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var salesOrderHelper = new SalesOrderHelper())
            {
                Console.WriteLine("------------------------");
                foreach (var curSalesOrder in salesOrderHelper.GetOrdersByCustomerName("Walter"))
                {
                    Console.WriteLine($"Sales Order Id: {curSalesOrder.SalesOrderID}");
                    Console.WriteLine($"Customer Id: {curSalesOrder.CustomerID}");
                    Console.WriteLine($"Shipping Method: {curSalesOrder.ShipMethod}");
                    Console.WriteLine($"Phone: {curSalesOrder.Customer.Phone}");
                    Console.WriteLine("");
                }
                Console.WriteLine("------------------------");
                Console.ReadLine();
                var groupedResults = salesOrderHelper.GetSalesOrderDetails().GroupBy(o => o.ProductID);
                SalesOrderDetail firstItem;
                foreach (var group in groupedResults)
                {
                    firstItem = group.First();
                    Console.WriteLine($"Product ID: {group.Key}");
                    Console.WriteLine($"LineTotal (Sum): {group.Sum(o => o.LineTotal)}");
                    Console.WriteLine($"LineTotal (Max): {group.Max(o => o.LineTotal)}");
                    Console.WriteLine($"LineTotal (Min): {group.Min(o => o.LineTotal)}");
                    Console.WriteLine($"LineTotal (Avg): {group.Average(o => o.LineTotal)}");
                    Console.WriteLine($"Order Qty (Sum): {group.Sum(o => o.OrderQty)}");
                    Console.WriteLine($"Product name: {firstItem.Product.Name}");
                    Console.WriteLine($"Product color: {firstItem.Product.Color}");
                    Console.WriteLine($"Product cat name: {firstItem.Product.ProductCategory.Name}");
                    Console.WriteLine("");
                }
            }
            Console.ReadLine();

        }

    }
}

