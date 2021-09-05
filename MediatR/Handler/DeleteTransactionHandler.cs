using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, bool>
    {
        private readonly CsvEditorContext _context;

        public DeleteTransactionHandler(CsvEditorContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            //trying to find transaction with requested id 
            var transaction = await _context.Transactions.FindAsync(request.Id);
            if (transaction != null)
            {
                //if transaction exist remove it
                _context.Transactions.Remove(transaction);
                var result = await _context.SaveChangesAsync();
                //verify that operation was completed
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
