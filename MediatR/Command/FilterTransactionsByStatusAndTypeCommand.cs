using CSVEDITOR.Models.File;
using MediatR;
using System.Collections.Generic;

namespace CSVEDITOR.MediatR.Command
{
    public class FilterTransactionsByStatusAndTypeCommand : IRequest<List<TransactionModel>>
    {
        public FilterTransactionsByStatusAndTypeCommand(string status, string type)
        {
            Status = status;
            Type = type;
        }
        //Can be "All" "Pending" "Completed" "Canceled"
        public string Status { get; set; }
        //Can be "All" "Withdrawal" "Refill" 
        public string Type { get; set; }
    }
}
