using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.Context;
using CSVEDITOR.Models.File;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class GetTransactionHandler : IRequestHandler<GetTransactionCommand, List<TransactionModel>>
    {
        private readonly CsvEditorContext _context;

        public GetTransactionHandler(CsvEditorContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionModel>> Handle(GetTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = _context.Transactions.ToList();

            return transaction;
        }
    }
}
