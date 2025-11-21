using System;

class Program
{
    static void Main(string[] args)
    {
        // ==== Order 1: USA customer ====
        Address usaAddress = new Address(
            street: "123 Main Street",
            city: "Rexburg",
            stateOrProvince: "ID",
            country: "USA");

        Customer usaCustomer = new Customer("John Smith", usaAddress);

        Order usaOrder = new Order(usaCustomer);
        usaOrder.AddProduct(new Product("Scripture Case", "SC-1001", 12.99m, 2));
        usaOrder.AddProduct(new Product("Notebook", "NB-2002", 4.50m, 3));
        usaOrder.AddProduct(new Product("Blue Pen Pack", "PN-3003", 2.75m, 4));

        // ==== Order 2: International customer ====
        Address intlAddress = new Address(
            street: "45 High Street",
            city: "Accra",
            stateOrProvince: "Greater Accra",
            country: "Ghana");

        Customer intlCustomer = new Customer("Ama Mensah", intlAddress);

        Order intlOrder = new Order(intlCustomer);
        intlOrder.AddProduct(new Product("Study Journal", "SJ-4004", 9.99m, 1));
        intlOrder.AddProduct(new Product("Highlighter Set", "HL-5005", 6.25m, 2));

        // Put orders in a list (if you want to scale to more)
        List<Order> orders = new List<Order> { usaOrder, intlOrder };

        int orderNumber = 1;
        foreach (Order order in orders)
        {
            Console.WriteLine($"================= ORDER {orderNumber} =================");
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order.GetTotalPrice():0.00}");
            Console.WriteLine();

            orderNumber++;
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}