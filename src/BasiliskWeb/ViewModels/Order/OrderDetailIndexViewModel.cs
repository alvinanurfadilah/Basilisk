using System;

namespace BasiliskWeb.ViewModels.Order;

public class OrderDetailIndexViewModel
{
    public OrderViewModel Order { get; set; }
    public List<OrderDetailViewModel> OrderDetail { get; set; }
    public PaginationViewModel Pagination { get; set; }
}
