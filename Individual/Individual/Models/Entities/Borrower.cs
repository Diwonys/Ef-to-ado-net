using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Individual.Models.Entities
{
    public class Borrower
    {
        public int Id { get; set; }

        [Display(Name = "Запрошенная сумма")]
        public int AmountRequested { get; set; }

        [Display(Name = "Поручители")]
        public List<Guarantor> Guarantors { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [Display(Name = "Номер паспорта")]
        public string PasportNumber { get; set; }
        [Display(Name = "Адресс")]
        public string Address { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Зарплата")]
        public int Salary { get; set; }

        public int? BankId { get; set; }
        public Bank Bank { get; set; }
    }
}
