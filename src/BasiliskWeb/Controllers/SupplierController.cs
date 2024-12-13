using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Supplier;
using Microsoft.AspNetCore.Authorization;

namespace BasiliskWeb.Controllers;

[Authorize(Roles = "Admin")]
public class SupplierController : Controller
{
    private readonly SupplierService _service;

    public SupplierController(SupplierService service) 
    {
        _service = service;
    }

    public IActionResult Index(int pageNumber = 1, int pageSize = 10, string companyName = "", string contactPerson = "", string jobTitle = "") 
    {
        var viewModel = _service.Get(pageNumber, pageSize, companyName, contactPerson, jobTitle);
        return View(viewModel);
    }

    [HttpGet("supplier/insertupdate")]
    public IActionResult Add()
    {
        return View("InsertUpdate");
    }

    [HttpPost("supplier/insertupdate")]
    public IActionResult Insert(SupplierInsertUpdateViewModel viewModel) 
    {
        if (ModelState.IsValid)
        {
            _service.Insert(viewModel);
            return RedirectToAction("Index");
        }
        return View("InsertUpdate");
    }

    [HttpGet("supplier/insertupdate/{id}")]
    public IActionResult Edit(long id) 
    {
        var viewModel = _service.Get(id);
        return View("InsertUpdate", viewModel);
    }

    [HttpPost("supplier/insertupdate/{id}")]
    public IActionResult Update(SupplierInsertUpdateViewModel viewModel) 
    {
        if (ModelState.IsValid)
        {
            _service.Update(viewModel);
            return RedirectToAction("Index");   
        }
        return View("InsertUpdate");
    }

    [HttpGet("supplier/delete/{id}")]
    public IActionResult Delete(long id)
    {
        _service.Update(id);
        return RedirectToAction("Index");
    }
}