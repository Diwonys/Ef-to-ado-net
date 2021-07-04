using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Individual.Models.Entities
{
    public class Bank
    {
        public int Id { get; set; }

        [Display(Name="Название банка")]
        public string Name { get; set; }

        [Display(Name = "Типы кредитов")]
        public List<CreditType> CreditTypes { get; set; }

        [Display(Name = "Заемщики")]
        public List<Borrower> Borrowers { get; set; }
    }
}
