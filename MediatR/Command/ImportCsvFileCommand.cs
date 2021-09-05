using CSVEDITOR.Models.File;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CSVEDITOR.MediatR.Command
{
    public class ImportCsvFileCommand : IRequest<List<TransactionModel>>
    {
        public ImportCsvFileCommand(IFormFile file)
        {
            File = file;
        }

        public IFormFile File { get; set; }
    }
}
