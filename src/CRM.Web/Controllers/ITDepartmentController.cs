using CRM.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers;

[Authorize(Policy = PolicyNames.RequireITDepartment)]
public class ITDepartmentController : Controller
{
  public IActionResult Index()
  {
    return View();
  }

  [Authorize(Policy = PolicyNames.RequireDirectorLevel)]
  [HttpGet]
  public IActionResult ManageTeam()
  {
    return View();
  }
}