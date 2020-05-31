using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.User
{
    public class Create
    {
        public class Command : IRequest
        {
            public string DisplayName { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUsername());

                if (user != null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "already exist" });

                user = new Domain.Entities.User
                {
                    UserName = _userAccessor.GetCurrentUsername(),
                    DisplayName = request.DisplayName,
                    Email = _userAccessor.GetCurrentEmail()
                };

                _context.Users.Add(user);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem creating local user");
            }
        }
    }
}