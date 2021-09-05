using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSVEDITOR.Models.File
{
    public class TransactionModel
    {

        [Key]
        //getting ability to set id which api want 
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransactionId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string ClientName { get; set; }
        public string Amount { get; set; }
    }
}
