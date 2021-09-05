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
    public class FilterTransactionsByStatusAndTypeHandler : IRequestHandler<FilterTransactionsByStatusAndTypeCommand, List<TransactionModel>>
    {
        private readonly CsvEditorContext _context;

        public FilterTransactionsByStatusAndTypeHandler(CsvEditorContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionModel>> Handle(FilterTransactionsByStatusAndTypeCommand request, CancellationToken cancellationToken)
        {
            //Checking all existing variants
            if (request.Status == "All" && request.Type == "All")
            {
                return _context.Transactions.ToList();
            }
            else
            {
                if (request.Status == "All" && request.Type != "All")
                {
                    return _context.Transactions.Where(x => x.Type == request.Type).ToList();
                }
                else
                {
                    if (request.Status != "All" && request.Type == "All")
                    {
                        return _context.Transactions.Where(x => x.Status == request.Status).ToList();
                    }
                    else
                    {
                        if (request.Status != "All" && request.Type != "All")
                        {
                            return _context.Transactions.Where(x => x.Status == request.Status && x.Type == request.Type).ToList();
                        }
                    }
                }
            }
            return null;
        }
    }
}
