using System.ComponentModel.DataAnnotations;

namespace HC.Shared.Dtos.Identity;

public class RoleDto
{
    [Required]
    public string Name { get; set; }
}