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
        [HttpGet]
        [Authorize(Policy = "ReadAccess")]
        public async Task<IActionResult> GetTasks([FromQuery] TaskParams param)
        {
            return HandleResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(Domain.Entities.Task task)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Task = task }));
        }

        [Authorize(Policy = "IsCreator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(Guid id, Domain.Entities.Task task)
        {
            task.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Task = task }));
        }

        [Authorize(Policy = "IsCreator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [Authorize(Policy = "IsAssignee")]
        [HttpPost("{id}/toggle")]
        public async Task<IActionResult> ToggleComplete(Guid id)
        {
            return HandleResult(await Mediator.Send(new ToggleComplete.Command { Id = id }));
        }
    }
}