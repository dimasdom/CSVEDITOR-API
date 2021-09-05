using CSVEDITOR.Models.File;
using MediatR;
using System.Collections.Generic;

namespace CSVEDITOR.MediatR.Command
{
    public class GetTransactionCommand : IRequest<List<TransactionModel>>
    {
    }
}
