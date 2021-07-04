using Individual.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Individual.Models
{
    public class ApplicationContext
    {
        public List<Bank> Banks { get; set; }
        public List<Borrower> Borrowers { get; set; }
        public List<CreditType> CreditTypes { get; set; }
        public List<Guarantor> Guarantors { get; set; }

        public DatabaseProvider DatabaseProvider { get; private set; }
        
        public ApplicationContext() 
        {
            DatabaseProvider = new DatabaseProvider("Server=(localdb)\\mssqllocaldb;Database=banksdb;Trusted_Connection=True;");

            Banks = DatabaseProvider
                .GetCollection<Bank>(typeof(Bank), nameof(Banks))
                .ToList();
            Borrowers = DatabaseProvider
                .GetCollection<Borrower>(typeof(Borrower), nameof(Borrowers))
                .ToList();
            CreditTypes = DatabaseProvider
                .GetCollection<CreditType>(typeof(CreditType), nameof(CreditTypes))
                .ToList();
            Guarantors = DatabaseProvider
                .GetCollection<Guarantor>(typeof(Guarantor), nameof(Guarantors))
                .ToList();
        }

    }
}
