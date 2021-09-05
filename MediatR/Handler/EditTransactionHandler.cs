using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class EditTransactionHandler : IRequestHandler<EditTransactionCommand, bool>
    {
        private readonly CsvEditorContext _context;

        public EditTransactionHandler(CsvEditorContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EditTransactionCommand request, CancellationToken cancellationToken)
        {
            //trying to find transaction with requested id
            var transaction = await _context.Transactions.FindAsync(request.Transaction.TransactionId);
            if (transaction != null)
            {
                //if that transaction exist just change that
                transaction.ClientName = request.Transaction.ClientName;
                transaction.Amount = request.Transaction.Amount;
                transaction.Status = request.Transaction.Status;
                transaction.Type = transaction.Type;
                var resulti = await _context.SaveChangesAsync();
                //verify was opperation complete
                if (resulti > 0)
                {
                    return true;
                }
                return false;
            }
            //if transaction with requested id doesn't exist
            //add to db
            _context.Transactions.Add(request.Transaction);
            var result =
            await _context.SaveChangesAsync();
            //verifying that operation were completed successfully
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
