using CSVEDITOR.Models.User;
using MediatR;

namespace CSVEDITOR.MediatR.Command
{
    public class RegisterCommand : IRequest<bool>
    {
        public RegisterCommand(RegisterDTOs register)
        {
            this.register = register;
        }

        public RegisterDTOs register { get; set; }
    }
}
