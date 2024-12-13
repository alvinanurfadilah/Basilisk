using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize(Roles = "Seller")]
public class CustomerController : Controller
{
    private readonly CustomerService _service;
    public CustomerController(CustomerService service) {
        _service = service;
    }

    public IActionResult Index(int pageNumber = 1, int pageSize = 10, string companyName = "", string contactPerson = "") {
        var viewModel = _service.Get(pageNumber, pageSize, companyName, contactPerson);
        return View(viewModel);
    }

    [HttpGet("customer/insertupdate")]
    public IActionResult Add() 
    {
        return View("InsertUpdate");
    }

    [HttpPost("customer/insertupdate")]
    public IActionResult Insert(CustomerInsertUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Insert(viewModel);
            return RedirectToAction("Index");
        }
        return View("InsertUpdate");
    }

    [HttpGet("customer/insertupdate/{id}")]
    public IActionResult Edit(long id)
    {
        var viewModel = _service.Get(id);
        return View("InsertUpdate", viewModel);
    }

    [HttpPost("customer/insertupdate/{id}")]
    public IActionResult Update(CustomerInsertUpdateViewModel viewModel) 
    {
        if (ModelState.IsValid)
        {
            _service.Update(viewModel);
            return RedirectToAction("Index");   
        }
        return View("InsertUpdate");
    }

    [HttpGet("customer/delete/{id}")]
    public IActionResult Delete(long id)
    {
        try
        {
            _service.Update(id);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            return View("Delete");
        }
    }
}
