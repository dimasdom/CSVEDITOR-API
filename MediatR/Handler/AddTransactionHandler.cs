using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class AddTransactionHandler : IRequestHandler<AddTransactionCommand, bool>
    {
        public AddTransactionHandler(CsvEditorContext context)
        {
            _context = context;
        }

        private readonly CsvEditorContext _context;
        public async Task<bool> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            //Creating transaction
            //Try to find is there transactions with same id

            var transaction = _context.Transactions.Find(request.Transaction.TransactionId);
            if (transaction != null)
            {
                //Updating data in existing transaction
                transaction.Type = request.Transaction.Type;
                transaction.Status = request.Transaction.Status;
                transaction.ClientName = request.Transaction.ClientName;
                transaction.Amount = request.Transaction.Amount;

                var resulti = await _context.SaveChangesAsync();
                //verify that data were saved
                if (resulti > 0)
                {
                    return true;
                }
                return false;
            }
            //is there isn't transaction with the same id 
            //just add new
            await _context.Transactions.AddAsync(request.Transaction);
            var result = await _context.SaveChangesAsync();
            //verify that data were saved 
            if (result > 0)
            {
                return true;
            }
            return false;

        }
    }
}
