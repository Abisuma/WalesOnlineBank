using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wales_Online_Bank.Models
{
    public enum IdCardType{NIN, InternationalPassport, VotersCard }
    public enum MaritalStatus { Single, Married, Divorce, Widow}
    public enum Gender { Male, Female}
    public enum StatusType { Active, Inactive}


    public class CustomerUser: IdentityUser
    {
        public string FirstName{get; set;}
        public string LastName { get; set; }
        public string Address { get; set; }
        public string IdCardNumber { get; set; }
        public IdCardType IdType { get; set; }
        [Required] 
        public MaritalStatus MaritalStatusOfCustomerUser { get; set; }    
        public Gender GenderType { get; set; }
        //public StatusType StatusOfAccount { get; set; }
        public DateTime DateOfBirth { get; set; }
        public  string PhoneNum { get; set; }
        public string?  ImageUrl { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        [ValidateNever]
        public List<Transaction> Transactions { get; set; }

    }
}
