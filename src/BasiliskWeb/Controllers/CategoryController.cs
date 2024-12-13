using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BasiliskWeb.Controllers;
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service) {
        _service = service;
    }

    public IActionResult Index(int pageNumber = 1, int pageSize = 10, string name ="") {
        var viewModel = _service.Get(pageNumber, pageSize, name);
        return View(viewModel);
    }

    [HttpGet("category/insertupdate")]
    public IActionResult Add() {
        return View("InsertUpdate");
    }

    [HttpPost("category/insertupdate")]
    public IActionResult Insert(CategoryInsertUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Insert(viewModel);
            return RedirectToAction("Index");
        }
        return View("InsertUpdate");
    }

    [HttpGet("category/insertupdate/{id}")]
    public IActionResult Edit(long id)
    {
        var viewModel = _service.Get(id);
        return View("InsertUpdate", viewModel);
    }

    [HttpPost("category/insertupdate/{id}")]
    public IActionResult Update(CategoryInsertUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Update(viewModel);
            return RedirectToAction("Index");
        }
        return View("InsertUpdate");
    }

    [HttpGet("category/delete/{id}")]
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
