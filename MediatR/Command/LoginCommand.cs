using CSVEDITOR.Models.User;
using MediatR;

namespace CSVEDITOR.MediatR.Command
{
    public class LoginCommand : IRequest<bool>
    {
        public LoginCommand(LoginDTOs loginData)
        {
            LoginData = loginData;
        }

        public LoginDTOs LoginData { get; set; }
    }
}
