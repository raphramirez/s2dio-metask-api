using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TasksController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Domain.Task>>> GetTaskAsync()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}