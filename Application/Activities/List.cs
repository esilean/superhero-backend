using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DTO;
using Application.Interfaces;
using AutoMapper;
using Data;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Activities
{
    public class List
    {

        public class Query : IRequest<ActivitiesEnvelope>
        {
            public int? Limit { get; set; }
            public int? Offset { get; set; }
            public bool IsGoing { get; set; }
            public bool IsHost { get; set; }
            public DateTime? StartDate { get; set; }
            public Query(int? limit, int? offset, bool isGoing, bool isHost, DateTime? startDate)
            {
                StartDate = startDate?.Date.ToUniversalTime() ?? DateTime.UtcNow.Date;
                IsHost = isHost;
                IsGoing = isGoing;
                Limit = limit;
                Offset = offset;
            }
        }

        public class Handler : IRequestHandler<Query, ActivitiesEnvelope>
        {

            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;
            }

            public async Task<ActivitiesEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Activities
                    .Where(x => x.Date >= request.StartDate)
                    .OrderBy(x => x.Date)
                    .AsQueryable();

                if (request.IsGoing && !request.IsHost)
                    queryable = queryable.Where(x =>
                        x.UserActivities.Any(a => a.User.UserName == _userAccessor.GetCurrentUsername()));

                if (request.IsHost && !request.IsGoing)
                    queryable = queryable.Where(x =>
                        x.UserActivities.Any(a => a.User.UserName == _userAccessor.GetCurrentUsername()
                        && a.IsHost));

                var activities = await queryable
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? 3).ToListAsync();

                var activitiesEnvelope = new ActivitiesEnvelope
                {
                    Activities = _mapper.Map<List<Activity>, List<ActivityDto>>(activities),
                    ActivityCount = queryable.Count()
                };

                return activitiesEnvelope;
            }
        }
    }
}