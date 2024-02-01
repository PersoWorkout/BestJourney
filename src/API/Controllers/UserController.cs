using Application.Interfaces.Users;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController(
        IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        //[HttpPost]
        //public async Task<IResult> Login([FromBody] LoginUserValidator payload)
        //{
        //    var result = await _userService.Login(payload);

        //    return result.IsSucess ?
        //        Results.Ok(result.Data) :
        //        Results.Problem(
        //            statusCode: StatusCodes.Status400BadRequest,
        //            title: "Bad Request",
        //            extensions: new Dictionary<string, object?>
        //            {
        //                {"errors", new [] {result.Error } }
        //            });
        //}

        [HttpGet]
        public async Task<IResult> Get()
        {
            var result = await _userService.GetUsers();
            return result.IsSucess ?
                Results.Ok(result.Data) :
                Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    extensions: new Dictionary<string, object?>
                    {
                        {"errors", new [] {result.Error } }
                    });
        }

        //[HttpPost]
        //public async Task<IResult> Create([FromBody] CreateUserValidator payload)
        //{
        //    var result = await _userService.Create(payload);

        //    return result.IsSucess ?
        //        Results.Created() :
        //        Results.Problem(
        //            statusCode: StatusCodes.Status400BadRequest,
        //            title: "Bad Request",
        //            extensions: new Dictionary<string, object?>
        //            {
        //                {"errors", new [] {result.Error } }
        //            });
        //}
    }
}
