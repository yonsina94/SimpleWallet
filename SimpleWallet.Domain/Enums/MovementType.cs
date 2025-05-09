using System.ComponentModel.DataAnnotations;

namespace SimpleWallet.Domain.Enums
{
    public enum MovementType
    {
        [Display(Name = "debit")]
        Debit = 1,
        [Display(Name = "credit")]
        Credit = 2
    }
}