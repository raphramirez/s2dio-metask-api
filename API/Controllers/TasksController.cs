using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class TasksController : BaseApiController
  {
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
      return HandleResult(await Mediator.Send(new List.Query()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(Guid id)
    {
      return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
    }
  }
}