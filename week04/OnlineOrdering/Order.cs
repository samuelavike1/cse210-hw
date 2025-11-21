public class Order
{
    private List<Product> _products = new List<Product>();
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal GetTotalPrice()
    {
        decimal productsTotal = 0m;

        foreach (Product product in _products)
        {
            productsTotal += product.GetTotalCost();
        }

        decimal shippingCost = _customer.LivesInUSA() ? 5m : 35m;

        return productsTotal + shippingCost;
    }

    public string GetPackingLabel()
    {
        // One line per product with name and ID
        // Example:
        // Product: Mouse (ID: MSE-001)
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("PACKING LABEL:");

        foreach (Product product in _products)
        {
            sb.AppendLine($"- {product.GetName()} (ID: {product.GetProductId()})");
        }

        return sb.ToString();
    }

    public string GetShippingLabel()
    {
        // Customer name + full address
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("SHIPPING LABEL:");
        sb.AppendLine(_customer.GetName());
        sb.AppendLine(_customer.GetAddress().GetFullAddress());

        return sb.ToString();
    }
}