using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Data;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Attend
{
    public class Attend
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAcessor;
            public Handler(DataContext context, IUserAccessor userAcessor)
            {
                _userAcessor = userAcessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var activity = await _context.Activities.FindAsync(request.Id);
                if (activity == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Activity cannot be found" });

                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAcessor.GetCurrentUsername());

                var attendance = await _context.UserActivities.SingleOrDefaultAsync(x =>
                    x.ActivityId == activity.Id &&
                    x.UserId == user.Id);

                if (attendance != null)
                    throw new RestException(HttpStatusCode.BadRequest, new { Attendance = "Already attending this activity" });

                attendance = new UserActivity
                {
                    Activity = activity,
                    User = user,
                    IsHost = false,
                    DateJoined = DateTime.Now,
                };

                _context.UserActivities.Add(attendance);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("Problem saving changes");
            }
        }
    }
}