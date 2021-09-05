using CSVEDITOR.Models.File;
using MediatR;

namespace CSVEDITOR.MediatR.Command
{
    public class AddTransactionCommand : IRequest<bool>
    {
        public AddTransactionCommand(TransactionModel transaction)
        {
            this.Transaction = transaction;
        }

        public TransactionModel Transaction
        {
            get; set;
        }
    }
}