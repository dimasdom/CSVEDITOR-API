using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.Context;
using CSVEDITOR.Models.File;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class ImportCsvFileHandler : IRequestHandler<ImportCsvFileCommand, List<TransactionModel>>
    {
        private readonly CsvEditorContext _context;

        public ImportCsvFileHandler(CsvEditorContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionModel>> Handle(ImportCsvFileCommand request, CancellationToken cancellationToken)
        {
            //checkign requested file
            if (request.File.FileName.EndsWith(".csv"))
            {
                //creating file stream
                using (StreamReader stream = new StreamReader(request.File.OpenReadStream()))
                {
                    //using CsvHelper
                    //configuring
                    //Setting culture info and Delimiter
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ","
                    };
                    //Creating CsvReader instance 
                    //putting file stream and CsvConfiguration
                    CsvReader csvReader = new CsvReader(stream, config);
                    //Getting info from .csv file
                    IEnumerable<TransactionModel> transactions =
                        csvReader.GetRecords<TransactionModel>();
                    //transform that into list 
                    var TransactionsList = transactions.ToList();

                    foreach (TransactionModel transaction in TransactionsList)
                    {
                        //checking is there transaction with the same id 
                        var existingTransaction = await _context.Transactions.FindAsync(transaction.TransactionId);
                        if (existingTransaction != null)
                        {
                            //if it does exist update data from .csv file
                            existingTransaction.Type = transaction.Type;
                            existingTransaction.Status = transaction.Status;
                            existingTransaction.Amount = transaction.Amount;
                            existingTransaction.ClientName = transaction.ClientName;

                            await _context.SaveChangesAsync();

                        }
                        else
                        {
                            //or creating new transaction
                            await _context.Transactions.AddAsync(transaction);
                        }

                    }
                    await _context.SaveChangesAsync();
                    return _context.Transactions.ToList();
                }
            }
            //if requested file isn't .csv returning null
            return null;
        }
    }
}
