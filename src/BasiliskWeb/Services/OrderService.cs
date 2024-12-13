using BasiliskBusiness.Interface;
using BasiliskBusiness.Interfaces;
using BasiliskDataAccess.Models;
using BasiliskWeb.ViewModels;
using BasiliskWeb.ViewModels.Order;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb.Services;

public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ISalesmanRepository _salesmanRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository repository, ICustomerRepository customerRepository, ISalesmanRepository salesmanRepository, IDeliveryRepository deliveryRepository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository)
    {
        _repository = repository;
        _customerRepository = customerRepository;
        _salesmanRepository = salesmanRepository;
        _deliveryRepository = deliveryRepository;
        _orderDetailRepository = orderDetailRepository;
        _productRepository = productRepository;
    }

    private List<SelectListItem> GetCustomer()
    {
        var model = _customerRepository.Get()
        .Select(cus => new SelectListItem() {
            Text = cus.CompanyName,
            Value = cus.Id.ToString()
        }).ToList();

        return model;
    }

    private List<SelectListItem> GetSalesman()
    {
        var model = _salesmanRepository.Get()
        .Select(sal => new SelectListItem() {
            Text = sal.FirstName + " " + sal.LastName,
            Value = sal.EmployeeNumber
        }).ToList();

        return model;
    }

    private List<SelectListItem> GetDelivery()
    {
        var model = _deliveryRepository.Get()
        .Select(del => new SelectListItem() {
            Text = del.CompanyName,
            Value = del.Id.ToString()
        }).ToList();

        return model;
    }

    private List<SelectListItem> GetProduct()
    {
        var model = _productRepository.Get()
        .Select(pro => new SelectListItem()
        {
            Text = pro.Name,
            Value = pro.Id.ToString()
        }).ToList();

        return model;
    }

    public OrderIndexViewModel Get(int pageNumber, int pageSize, string invoiceNumber, long customerId, string salesEmployeeNumber, long deliveryId, DateTime orderDate)
    {
        var model = _repository.Get(pageNumber, pageSize, invoiceNumber, customerId, salesEmployeeNumber, deliveryId, orderDate)
        .Select(order => new OrderViewModel() {
            InvoiceNumber = order.InvoiceNumber,
            CustomerCompanyName = order.Customer.CompanyName,
            SalesmanName = order.SalesEmployeeNumberNavigation.FirstName + " " + order.SalesEmployeeNumberNavigation.LastName,
            OrderDate = order.OrderDate,
            CompanyName = order.Delivery.CompanyName
        });

        return new OrderIndexViewModel()
        {
            Orders = model.ToList(),
            Pagination = new PaginationViewModel() {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _repository.Count(invoiceNumber, customerId, salesEmployeeNumber, deliveryId, orderDate)
            },
            InvoiceNumber = invoiceNumber,
            CustomerId = customerId,
            SalesEmployeeNumber = salesEmployeeNumber,
            DeliveryId = deliveryId,
            OrderDate = orderDate,
            Customers = GetCustomer(),
            Salesman = GetSalesman(),
            Deliveries = GetDelivery()
        };
    }

    public OrderInsertUpdateViewModel Get()
    {
        return new OrderInsertUpdateViewModel()
        {
            Customers = GetCustomer(),
            Salesman = GetSalesman(),
            Deliveries = GetDelivery()
        };
    }

    public OrderInsertUpdateViewModel Get(string invoiceNumber)
    {
        var model = _repository.Get(invoiceNumber);

        return new OrderInsertUpdateViewModel()
        {
            InvoiceNumber = model.InvoiceNumber,
            CustomerId = model.CustomerId,
            SalesEmployeeNumber = model.SalesEmployeeNumber,
            OrderDate = model.OrderDate,
            ShippedDate = model.ShippedDate,
            DueDate = model.DueDate,
            DeliveryId = model.DeliveryId,
            DestinationAddress = model.DestinationAddress,
            DestinationCity = model.DestinationCity,
            DestinationPostalCode = model.DestinationPostalCode,
            Customers = GetCustomer(),
            Salesman = GetSalesman(),
            Deliveries = GetDelivery()
        };
    }

    public void Insert(OrderInsertUpdateViewModel viewModel)
    {
        var model = new Order()
        {
            CustomerId = viewModel.CustomerId,
            SalesEmployeeNumber = viewModel.SalesEmployeeNumber,
            OrderDate = viewModel.OrderDate,
            ShippedDate = viewModel.ShippedDate,
            DueDate = viewModel.DueDate,
            DeliveryId = viewModel.DeliveryId,
            DestinationAddress = viewModel.DestinationAddress,
            DestinationCity = viewModel.DestinationCity,
            DestinationPostalCode = viewModel.DestinationPostalCode,
            // tambahan
            InvoiceNumber = $"{DateTime.Now.ToString("dd-MM")}-0001",
            DeliveryCost = _repository.GetCost(viewModel.DeliveryId)
        };

        _repository.Insert(model);
    }

    public void Update(OrderInsertUpdateViewModel viewModel)
    {
        var model = _repository.Get(viewModel.InvoiceNumber);
        model.CustomerId = viewModel.CustomerId;
        model.SalesEmployeeNumber = viewModel.SalesEmployeeNumber;
        model.OrderDate = viewModel.OrderDate;
        model.ShippedDate = viewModel.ShippedDate;
        model.DueDate = viewModel.DueDate;
        model.DeliveryId = viewModel.DeliveryId;
        model.DestinationAddress = viewModel.DestinationAddress;
        model.DestinationCity = viewModel.DestinationCity;
        model.DestinationPostalCode = viewModel.DestinationPostalCode;
        model.DeliveryCost = _repository.GetCost(viewModel.DeliveryId);

        _repository.Update(model);
    }

    public void Delete(string invoiceNumber)
    {
        var model = _repository.Get(invoiceNumber);
        _repository.Delete(model);
    }


    public OrderDetailIndexViewModel GetOrderDetail(string invoiceNumber, int pageNumber, int pageSize)
    {
        var model = _repository.GetOrderDetail(invoiceNumber);
        var getOrderDetail = _orderDetailRepository.Get(pageNumber, pageSize, model.InvoiceNumber);
        var listOrderDetail = new List<OrderDetailViewModel>();

        foreach (var item in getOrderDetail)
        {
            var orderDetail = new OrderDetailViewModel()
            {
                Id = item.Id,
                ProductName = item.Product.Name,
                Price = item.Product.Price.ToString("C", new System.Globalization.CultureInfo("id-ID")),
                Qty = item.Quantity,
                Discount = (item.Discount / 100).ToString("P2", new System.Globalization.CultureInfo("id-ID")),
                TotalPrice = (item.Product.Price * item.Quantity * (1 - item.Discount / 100)).ToString("C", new System.Globalization.CultureInfo("id-ID"))
            };

            listOrderDetail.Add(orderDetail);
        }

        return new OrderDetailIndexViewModel()
        {
            Order = new OrderViewModel()
            {
                InvoiceNumber = model.InvoiceNumber,
                CustomerCompanyName = model.Customer.CompanyName,
                SalesmanName = model.SalesEmployeeNumberNavigation.FirstName + " " + model.SalesEmployeeNumberNavigation.LastName,
                OrderDate = model.OrderDate
            },
            OrderDetail = listOrderDetail,
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _orderDetailRepository.Count(invoiceNumber)
            }
        };
    }

    public OrderDetailInsertUpdateViewModel GetOrderDetail()
    {
        return new OrderDetailInsertUpdateViewModel()
        {   
            Products = GetProduct()
        };
    }

    public OrderDetailInsertUpdateViewModel GetOrderDetail(long id)
    {
        var model = _orderDetailRepository.Get(id);

        return new OrderDetailInsertUpdateViewModel()
        {
            Id = model.Id,
            ProductId = model.ProductId,
            Quantity = model.Quantity,
            Discount = model.Discount,
            Products = GetProduct()
        };
    }

    public void InsertOrderDetail(OrderDetailInsertUpdateViewModel viewModel)
    {
        var getProduct = _productRepository.Get(viewModel.ProductId);
        var model = new OrderDetail()
        {
            InvoiceNumber = viewModel.InvoiceNumber,
            ProductId = viewModel.ProductId,
            UnitPrice = getProduct.Price,
            Quantity = viewModel.Quantity,
            Discount = viewModel.Discount
        };

        _orderDetailRepository.Insert(model);
    }

    public void UpdateOrderDetail(OrderDetailInsertUpdateViewModel viewModel)
    {
        var model = _orderDetailRepository.Get(viewModel.Id);
        model.ProductId = viewModel.ProductId;
        model.Quantity = viewModel.Quantity;
        model.Discount = viewModel.Discount;

        _orderDetailRepository.Update(model);
    }

    public void DeleteOrderDetail(long id)
    {
        var model = _orderDetailRepository.Get(id);
        _orderDetailRepository.Delete(model);
    }
}
