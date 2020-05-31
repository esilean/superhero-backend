using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.User.DTO;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.User
{
    public class Get
    {
        public class Query : IRequest<UserDto> { }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUsername());

                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "cannot be found" });

                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Username = user.UserName,
                    Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
                };
            }
        }
    }
}