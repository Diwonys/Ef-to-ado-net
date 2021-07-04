using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Individual.Models.Entities
{
    public class CreditType
    {
        public int Id { get; set; }

        [Display(Name = "Тип кредита")]
        public string Name { get; set; }

        public int? BankId { get; set; }
        public Bank Bank { get; set; }
    }
}
