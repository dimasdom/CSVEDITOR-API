using CSVEDITOR.Models.File;
using CSVEDITOR.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSVEDITOR.Models.Context
{
    public class CsvEditorContext : IdentityDbContext<UserModel>
    {
        public CsvEditorContext(DbContextOptions<CsvEditorContext> options)
        : base(options)
        {
        }

        public DbSet<TransactionModel> Transactions { get; set; }
    }
}
