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
    public class SearchTransactionByClientNameHandler : IRequestHandler<SearchTransactionByClientNameCommand, List<TransactionModel>>
    {
        private readonly CsvEditorContext _context;

        public SearchTransactionByClientNameHandler(CsvEditorContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionModel>> Handle(SearchTransactionByClientNameCommand request, CancellationToken cancellationToken)
        {
            var transaction = _context.Transactions.Where(x => x.ClientName == request.ClientName).ToList();
            return transaction;
        }
    }
}
