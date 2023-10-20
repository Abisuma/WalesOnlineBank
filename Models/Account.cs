using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wales_Online_Bank.Models
{

    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [DisplayName("Account Number")]
        public string? Number { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

        [NotMapped]

        [DisplayName("Description")]
        public string? TransactionDescription { get; set; }

        public string? AccountName { get; set; }
        
        //public CustomerUser? CustomerUserId { get; set; }
        ////[ForeignKey("CustomerUserId")]
        //[ValidateNever]
        //public CustomerUser? CustomerUser { get; set; }
    }
}
