using System.ComponentModel.DataAnnotations;

namespace SimpleWallet.Domain.Enums
{
    public enum MovementType
    {
        [Display(Name = "debit")]
        Debit,
        [Display(Name = "credit")]
        Credit
    }
}