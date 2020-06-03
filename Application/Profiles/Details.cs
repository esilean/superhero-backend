using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Profiles.DTO;
using Application.Profiles.Interfaces;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Profiles
{
    public class Details
    {
        public class Query : IRequest<ProfileDto>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProfileDto>
        {

            private readonly IProfileReader _profileReader;

            public Handler(IProfileReader profileReader)
            {
                _profileReader = profileReader;

            }

            public async Task<ProfileDto> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _profileReader.ReadProfile(request.Username);
            }
        }
    }
}