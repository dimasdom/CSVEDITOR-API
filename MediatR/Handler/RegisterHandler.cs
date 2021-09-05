using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
    {
        //using Microsoft.AspNetCore.Identity;
        //dependency injected in Startup.cs
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public RegisterHandler(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            //creating new user with requested data
            var user = new UserModel
            {
                Email = request.register.Email,
                UserName = request.register.UserName
            };
            //putting user into db
            var result = await _userManager.CreateAsync(user, request.register.Password);
            if (result.Succeeded)
            {
                //checking result
                return true;
            }
            return false;
        }
    }
}
