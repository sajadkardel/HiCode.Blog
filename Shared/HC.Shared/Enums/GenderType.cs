using System.ComponentModel.DataAnnotations;

namespace HC.Shared.Enums;

public enum GenderType
{
    [Display(Name = "مرد")]
    Male = 1,

    [Display(Name = "زن")]
    Female = 2,

    [Display(Name = "دیگر جنسیت ها")]
    Other = 3,
}
