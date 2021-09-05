using MediatR;

namespace CSVEDITOR.MediatR.Command
{
    public class DeleteTransactionCommand : IRequest<bool>
    {
        public DeleteTransactionCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
