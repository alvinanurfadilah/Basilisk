using System;

namespace BasiliskWeb.ViewModels.Order;

public class OrderDetailViewModel
{
    public long Id { get; set; }
    public string ProductName { get; set; }
    public string Price { get; set; }
    public long Qty { get; set; }
    public string Discount { get; set; }
    public string TotalPrice { get; set; }
}
