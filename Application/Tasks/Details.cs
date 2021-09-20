using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

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
                    .FirstOrDefault(task => task.Id == request.Id, t => t.Assignee, t => t.CreatedBy);

                var taskToReturn = _mapper.Map<TaskDto>(task);

                return Result<TaskDto>.Success(taskToReturn);
            }
        }
    }
}