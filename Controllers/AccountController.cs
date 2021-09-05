using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CSVEDITOR.Controllers
{
    //Account Responsible Controller
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private UserManager<UserModel> _userManager;
        private SignInManager<UserModel> _signInManager;

        public AccountController(IMediator mediator, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _mediator = mediator;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        [HttpPost("Login")]

        public async Task<ActionResult> Login(LoginDTOs loginDTOs)
        {
            var user = await _userManager.FindByEmailAsync(loginDTOs.Email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDTOs.Password, false, false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return NotFound("Ivalid Data"); ;
            
            
        }
        
        [HttpPost("Register")]
        public async Task<ActionResult<UserModel>> Register(RegisterDTOs registerDTOs)
        {
            var newUser = new UserModel
            {
                Email = registerDTOs.Email,
                UserName = registerDTOs.UserName
            };
            var result = await _userManager.CreateAsync(newUser, registerDTOs.Password);

            if (result.Succeeded)
            {
                return newUser;
            }
            return NotFound();
            
        }

        [Authorize]
        [HttpGet("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            //Using MediatR command
            var command = new LogOutCommand();
            //Details in LogOutHandler
            await _mediator.Send(command);
            return Ok();
        }
    }
}
