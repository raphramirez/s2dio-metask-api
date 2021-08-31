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

        [HttpPost]
        public async Task<IActionResult> CreateTask(Domain.Task task)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Task = task }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(Guid id, Domain.Task task)
        {
            task.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Task = task }));
        }
    }
}