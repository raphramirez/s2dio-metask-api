using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Tasks
{
    public class Details
    {
        public class Query : IRequest<Result<TaskDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TaskDto>>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly IMapper _mapper;
            public Handler(ITaskRepository taskRepository, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _mapper = mapper;
            }

            public async Task<Result<TaskDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var task = await _taskRepository
                    .FirstOrDefault(task => task.Id.Equals(request.Id),

                    // Includes
                    t => t.CreatedBy,
                    t => t.UserTasks
                );

                var taskToReturn = _mapper.Map<TaskDto>(task);

                return Result<TaskDto>.Success(taskToReturn);
            }
        }
    }
}