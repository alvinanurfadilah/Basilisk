using BasiliskWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize(Roles = "Seller")]
public class ProductController : Controller
{
    private readonly ProductService _service;

    public ProductController(ProductService service) {
        _service = service;
    }

    public IActionResult Index(long categoryId, long supplierId, int pageNumber = 1, int pageSize = 9, string name = "") {
        var viewModel = _service.Get(pageNumber, pageSize, name, categoryId, supplierId);
        return View(viewModel);
    }

    [HttpGet("product/insertupdate")]
    public IActionResult Add() 
    {
        var viewModel = _service.Get();
        return View("InsertUpdate", viewModel);
    }

    [HttpPost("product/insertupdate")]
    public IActionResult Insert(ProductInsertUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Insert(viewModel);
            return RedirectToAction("Index");
        }
        var vm = _service.Get();
        return View("InsertUpdate", vm);
    }

    [HttpGet("product/insertupdate/{id}")]
    public IActionResult Edit(long id)
    {
        var viewModel = _service.Get(id);
        return View("InsertUpdate", viewModel);
    }

    [HttpPost("product/insertupdate/{id}")]
    public IActionResult Update(ProductInsertUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Update(viewModel);
            return RedirectToAction("Index");
        }
        var vm = _service.Get();
        return View("InsertUpdate", vm);
    }

    [HttpGet("product/delete")]
    public IActionResult Delete(long id)
    {
        try
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            return View("Delete");
        }
    }
}