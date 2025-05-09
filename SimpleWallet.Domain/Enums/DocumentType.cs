using System.ComponentModel.DataAnnotations;

namespace SimpleWallet.Domain.Enums;

public enum DocumentType
{
    [Display(Name = "Identification Number")]
    IdentificationNumber = 1,
    [Display(Name = "Passport")]
    Passport = 2,
}