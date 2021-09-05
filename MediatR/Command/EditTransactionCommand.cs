using CSVEDITOR.Models.File;
using MediatR;

namespace CSVEDITOR.MediatR.Command
{
    public class EditTransactionCommand : IRequest<bool>
    {
        public EditTransactionCommand(TransactionModel transaction)
        {
            Transaction = transaction;
        }

        public TransactionModel Transaction { get; set; }
    }
}
