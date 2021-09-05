using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class LoginHandler : IRequestHandler<LoginCommand, bool>
    {
        //using Microsoft.AspNetCore.Identity;
        //dependency injected in Startup.cs

        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public LoginHandler(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //searching is there a user with that email
            var user = await _userManager.FindByEmailAsync(request.LoginData.Email);
            if (user != null)
            {
                //if there is a user 
                //signing him 
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginData.Password, false);
                if (result.Succeeded)
                {
                    //checking result
                    return true;
                }
                return false;
            }
            //if there isn't user whith that email
            return false;
        }
    }
}
