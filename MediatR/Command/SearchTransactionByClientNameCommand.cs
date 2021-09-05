using CSVEDITOR.Models.File;
using MediatR;
using System.Collections.Generic;

namespace CSVEDITOR.MediatR.Command
{
    public class SearchTransactionByClientNameCommand : IRequest<List<TransactionModel>>
    {
        public SearchTransactionByClientNameCommand(string clientName)
        {
            ClientName = clientName;
        }

        public string ClientName { get; set; }
    }
}
