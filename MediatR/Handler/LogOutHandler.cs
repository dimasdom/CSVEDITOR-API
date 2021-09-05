using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class LogOutHandler : IRequestHandler<LogOutCommand, bool>
    {
        private readonly SignInManager<UserModel> _signInManager;

        public LogOutHandler(SignInManager<UserModel> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return true;
        }
    }
}
