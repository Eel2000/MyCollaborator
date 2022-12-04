using System.ComponentModel.DataAnnotations;

namespace MyCollaborator.Shared.DTOs;

public class AuthenticationQuery
{
    [Required, DataType(DataType.PhoneNumber, ErrorMessage = "Please specify the phone number")]
    public string Telephone { get; set; }

    [Required(ErrorMessage = "The username is required")]
    public string Username { get; set; }
}