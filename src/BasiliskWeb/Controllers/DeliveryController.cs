using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Delivery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize(Roles = "Admin")]
public class DeliveryController : Controller
{
    private readonly DeliveryService _service;
    public DeliveryController(DeliveryService service) {
        _service = service;
    }

    public IActionResult Index(int pageNumber = 1, int pageSize = 10, string companyName = "") {
        var ViewModels = _service.Get(pageNumber, pageSize, companyName);
        return View(ViewModels);
    }

    [HttpGet("delivery/insertupdate")]
    public IActionResult Add() {
        return View("InsertUpdate");
    }

    [HttpPost("delivery/insertupdate")]
    public IActionResult Insert(DeliveryInsertUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Insert(viewModel);
            return RedirectToAction("Index");
        }
        return View("InsertUpdate");
    }

    [HttpGet("delivery/insertupdate/{id}")]
    public IActionResult Edit(long id)
    {
        var viewModel = _service.Get(id);
        return View("InsertUpdate", viewModel);
    }

    [HttpPost("delivery/insertupdate/{id}")]
    public IActionResult Update(DeliveryInsertUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Update(viewModel);
            return RedirectToAction("Index");
        }
        return View("InsertUpdate");
    }

    [HttpGet("delivery/delete/{id}")]
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