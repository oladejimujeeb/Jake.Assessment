using Jake.Assessment.DTO;
using Jake.Assessment.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Jake.Assessment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseModel),200)]
        public async Task< IActionResult> CreateUser(CreateUserRequestModel model)
        {
            if (model == null) 
            {
                return BadRequest("Invalid Model");
            }
            var addUser = await _userService.CreateNewUser(model);
            if(!addUser.Status)
            {
                return BadRequest();
            }
            return Ok(addUser);
        }
        [HttpGet]
        [ProducesResponseType(typeof(AllUserOrders), 200)]
        public async Task<IActionResult> GetUserOrders([Required] int userId)
        {
            var userOrder = await _userService.AllUserOrder(userId);
            if (userOrder.Status)
            {
                return Ok(userOrder.Data);
            }
            return BadRequest(userOrder);
        }
    }
}
