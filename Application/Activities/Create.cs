using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Data;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime Date { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Category).NotEmpty();
                RuleFor(x => x.Date).NotEmpty();
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.Venue).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAcessor _userAcessor;
            public Handler(DataContext context, IUserAcessor userAcessor)
            {
                _userAcessor = userAcessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = new Activity
                {
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category,
                    Date = request.Date,
                    City = request.City,
                    Venue = request.Venue
                };

                _context.Activities.Add(activity);

                //get user from local db with the identityuser _userAcessor
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAcessor.GetCurrentUsername());
                if (user == null)
                {
                    //we create this user, cause we do NOT have access to Identity Project
                    var newUser = new Domain.Entities.User
                    {
                        UserName = _userAcessor.GetCurrentUsername(),
                        DisplayName = _userAcessor.GetCurrentDisplayName(),
                        Email = _userAcessor.GetCurrentEmail()
                    };

                    _context.Users.Add(newUser);
                    user = newUser;
                }

                var attendee = new UserActivity
                {
                    User = user,
                    Activity = activity,
                    IsHost = true,
                    DateJoined = DateTime.Now
                };

                _context.UserActivities.Add(attendee);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}