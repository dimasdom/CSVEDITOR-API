using ClosedXML.Excel;
using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.Context;
using CSVEDITOR.Models.DTOs;
using CSVEDITOR.Models.File;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CSVEDITOR.Controllers
{
    //User must authorize for use this part of the API
    //Authorize details in AccountController
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CsvEditorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CsvEditorContext _context;

        public CsvEditorController(IMediator mediator, CsvEditorContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet("GetTransactions")]
        public async Task<ActionResult<List<TransactionModel>>> Index()
        {
            //Getting all transactions from DB
            var command = new GetTransactionCommand();
            //Details in GetTransactionHandler
            var result = await _mediator.Send(command);


            return result;
        }




        [HttpPost("Edit")]
        public async Task<ActionResult<List<TransactionModel>>> Edit(TransactionModel transaction)
        {
            //Getting updated transaction data from form in CsvEditor/Edit
            var command = new EditTransactionCommand(transaction);
            //Details in EditTransactionHandler
            //Handler return boolean for notify app for succes or error
            var result = await _mediator.Send(command);
            var command2 = new GetTransactionCommand();
            var transtaction = await _mediator.Send(command2);
            if (result) return transtaction;
            //in case of error it redirect user to error page
            else return NotFound();

        }




        [HttpPost("Create")]
        public async Task<ActionResult<List<TransactionModel>>> Create(TransactionModel transaction)
        {
            //Getting data from form at CsvEditor/Create
            //Insert Data to command
            var command = new AddTransactionCommand(transaction);
            //Details in AddTransactionHandler
            var result = await _mediator.Send(command);
            //Getting All transactions
            var command2 = new GetTransactionCommand();
            var transtaction = await _mediator.Send(command2);
            if (result)
                return transtaction;
            else
                return NotFound();
        }


        [HttpPost("Import")]

        public async Task<ActionResult<List<TransactionModel>>> Import(IFormFile File)
        {
            //Import .csv file from form at CsvEditor/Index
            //Putting File Data to command
            var command = new ImportCsvFileCommand(File);
            //Details in ImportCsvFileHandler
            var result = await _mediator.Send(command);
            //In Result we get updated transaction list 
            return result;
        }
        [HttpGet("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteTransactionCommand(id);
            var result = await _mediator.Send(command);
            return Ok();
        }
        [HttpPost("Search/{clientName}")]
        public async Task<ActionResult<List<TransactionModel>>> Search(string clientName)
        {
            //Getting Client Name from form at CsvEditor/Index
            //Putting Client Name into command
            var command = new SearchTransactionByClientNameCommand(clientName);
            //Details in SearchTransactionByClientNameHandler
            //Getting transaction list where client name coincided
            var result = await _mediator.Send(command);

            return result;
        }
        [HttpPost("Filter")]
        public async Task<ActionResult<List<TransactionModel>>> Filter(FilterDTOs filterDTOs)
        {
            //Filtering transactions by status and type
            //Getting data from form at CsvEditor/Index
            //Putting data into command
            var command = new FilterTransactionsByStatusAndTypeCommand(filterDTOs.Status, filterDTOs.Type);
            //Detail in FilterTransactionsByStatusAndTypeHandler
            var result = await _mediator.Send(command);
            //Getting new transaction list 

            return result;
        }
        [HttpPost("Export")]
        public async Task<ActionResult> Export(ExportDTOs exportDTOs)
        {
            //Exporting Excel file 
            //creating metadata
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "transactions.xlsx";
            //using try catch for catching erors
            try
            {
                //creating class instanse
                //Using XLWorkbook service for creating tables
                using (var workbook = new XLWorkbook())
                {
                    //creating command for getting filtered informations
                    var command = new FilterTransactionsByStatusAndTypeCommand(exportDTOs.Status, exportDTOs.Type);
                    //Details ing FilterTransactionsByStatusAndTypeHandler
                    var result = await _mediator.Send(command);

                    var transactions = result;
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Transactions");
                    //Getting checkbox info from form at CsvEdit/Index
                    //Data from checkbox coming in string or null
                    //Checked checkbox is "on"
                    //Checking wich columns will be exported 

                    worksheet.Cell(1, 1).Value = exportDTOs.IdBool ? "TransactionId" : "";
                    worksheet.Cell(1, 2).Value = exportDTOs.StatusBool ? "Status" : "";
                    worksheet.Cell(1, 3).Value = exportDTOs.TypeBool ? "Type" : "";
                    worksheet.Cell(1, 4).Value = exportDTOs.ClientBool ? "Client" : "";
                    worksheet.Cell(1, 5).Value = exportDTOs.AmountBool ? "Amount" : "";
                    //Putting filtered data into cells
                    for (int index = 1; index <= transactions.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value =
                            //Checking which data will be puted into cells
                            exportDTOs.IdBool ?
                        transactions[index - 1].TransactionId : "";
                        worksheet.Cell(index + 1, 2).Value =
                            exportDTOs.StatusBool ?
                        transactions[index - 1].Status : "";
                        worksheet.Cell(index + 1, 3).Value =
                            exportDTOs.TypeBool ?
                        transactions[index - 1].Type : "";
                        worksheet.Cell(index + 1, 4).Value =
                            exportDTOs.ClientBool ?
                        transactions[index - 1].ClientName : "";
                        worksheet.Cell(index + 1, 5).Value =
                            exportDTOs.AmountBool ?
                        transactions[index - 1].Amount : "";
                    }
                    //Creating memory stream for saving file data 
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        //Returning File to user
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                //in case of error returning 404 error
                return NotFound(ex);
            }
        }
    }
}
