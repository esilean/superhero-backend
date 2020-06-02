using System.Threading.Tasks;
using Application.User;
using Application.User.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost("local")]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("local")]
        public async Task<ActionResult<UserDto>> Get()
        {
            return await Mediator.Send(new Get.Query());
        }
    }
}