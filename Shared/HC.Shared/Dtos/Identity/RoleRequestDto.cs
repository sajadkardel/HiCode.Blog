using System.ComponentModel.DataAnnotations;

namespace HC.Shared.Dtos.Identity;

public class RoleRequestDto
{
    [Required]
    public string Name { get; set; }
}