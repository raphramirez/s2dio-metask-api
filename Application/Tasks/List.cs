using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Tasks
{
    public class List
    {
        public class Query : IRequest<Result<List<TaskDto>>>
        {
            public TaskParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<TaskDto>>>
        {
            private readonly ILogger<List> _logger;
            private readonly ITaskRepository _taskRepository;
            private readonly IMapper _mapper;
            private readonly IUsernameAccessor _usernameAccessor;

            public Handler(ITaskRepository taskRepository, IMapper mapper, IUsernameAccessor usernameAccessor)
            {
                _taskRepository = taskRepository;
                _mapper = mapper;
                _usernameAccessor = usernameAccessor;
            }

            public async Task<Result<List<TaskDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tasks = await _taskRepository.GetByDate(
                    DateTime.Now,
                    t => t.Assignee,
                    t => t.CreatedBy
                );

                var tasksToReturn = _mapper.Map<List<TaskDto>>(tasks);
                return Result<List<TaskDto>>.Success(tasksToReturn);
            }
        }
    }
}