using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize(Roles = "Seller")]
public class OrderController : Controller
{
    private readonly OrderService _service;

    public OrderController(OrderService service)
    {
        _service = service;
    }

    public IActionResult Index(long customerId, long deliveryId, string salesEmployeeNumber = "", int pageNumber = 1, int pageSize = 5, string invoiceNumber = "", DateTime orderDate = new DateTime())
    {
        var viewModel = _service.Get(pageNumber, pageSize, invoiceNumber, customerId, salesEmployeeNumber, deliveryId, orderDate);
        return View(viewModel);
    }

    [HttpGet("order/insertupdate")]
    public IActionResult Add()
    {
        var viewModel = _service.Get();
        return View("InsertUpdate", viewModel);
    }

    [HttpPost("order/insertupdate")]
    public IActionResult Insert(OrderInsertUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Insert(viewModel);
            return RedirectToAction("Index");
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        var vm = _service.Get();
        return View("InsertUpdate", vm);
    }

    [HttpGet("order/insertupdate/{invoiceNumber}")]
    public IActionResult Edit(string invoiceNumber)
    {
        var viewModel = _service.Get(invoiceNumber);
        return View("InsertUpdate", viewModel);
    }

    [HttpPost("order/insertupdate/{invoiceNumber}")]
    public IActionResult Update(OrderInsertUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Update(viewModel);
            return RedirectToAction("Index");
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        var vm = _service.Get();
        return View("InsertUpdate", vm);
    }

    [HttpGet("order/delete")]
    public IActionResult Delete(string invoiceNumber)
    {
        try
        {
            _service.Delete(invoiceNumber);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            return View("Delete");
        }
    }

    [HttpGet("order/detail/{invoiceNumber}")]
    public IActionResult OrderDetail(string invoiceNumber, int pageNumber = 1, int pageSize = 5)
    {
        var model = _service.GetOrderDetail(invoiceNumber, pageNumber, pageSize);
        return View("OrderDetail", model);
    }

    [HttpGet("order/detail/insertupdate/{invoiceNumber}")]
    public IActionResult AddOrderDetail(string invoiceNumber)
    {
        var viewModel = _service.GetOrderDetail();
        viewModel.InvoiceNumber = invoiceNumber;
        return View("OrderDetailInsertUpdate", viewModel);
    }

    [HttpPost("order/detail/insertupdate/{invoiceNumber}")]
    public IActionResult InsertOrderDetail(OrderDetailInsertUpdateViewModel viewModel, string invoiceNumber)
    {
        if (ModelState.IsValid)
        {
            viewModel.InvoiceNumber = invoiceNumber;
            _service.InsertOrderDetail(viewModel);
            return RedirectToAction("OrderDetail", new { invoiceNumber = viewModel.InvoiceNumber });
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        var vm = _service.GetOrderDetail();
        vm.InvoiceNumber = invoiceNumber;
        return View("OrderDetailInsertUpdate", vm);
    }

    [HttpGet("order/detail/insertupdate/{invoiceNumber}/{id}")]
    public IActionResult EditOrderDetail(long id)
    {
        var viewModel = _service.GetOrderDetail(id);
        return View("OrderDetailInsertUpdate", viewModel);
    }

    [HttpPost("order/detail/insertupdate/{invoiceNumber}/{id}")]
    public IActionResult UpdateOrderDetail(OrderDetailInsertUpdateViewModel viewModel, string invoiceNumber)
    {
        if (ModelState.IsValid)
        {
            _service.UpdateOrderDetail(viewModel);
            return RedirectToAction("OrderDetail", new { invoiceNumber = viewModel.InvoiceNumber });
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        var vm = _service.GetOrderDetail(viewModel.Id);
        return View("OrderDetailInsertUpdate", viewModel);
    }

    [HttpGet("order/detail/delete/{invoiceNumber}/{id}")]
    public IActionResult DeleteOrderDetail(string invoiceNumber, long id)
    {

        _service.DeleteOrderDetail(id);
        return RedirectToAction("OrderDetail", new { invoiceNumber });
    }
}