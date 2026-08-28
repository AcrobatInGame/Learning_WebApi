// ReSharper disable UnnecessaryWhitespace
using System.ComponentModel.DataAnnotations;

namespace Learning_WebApi.Model;

public class UserValidator
{
    public static IEnumerable<string> ValidateUsersRegistration(UserDto userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.FirstName))
        {
            yield return "The first name was Entered wrong";
        }

        if (string.IsNullOrWhiteSpace(userDto.LastName))
        {
            yield return "The last name was Entered wrong";
        }

        var detector = new EmailAddressAttribute();
        if (!detector.IsValid(userDto.Email))
        {
            yield return "The email address was entered wrong";
        }

        if (userDto.Password.Length <= 4 || string.IsNullOrWhiteSpace(userDto.Password))
        {
            yield return "The password has to be at least 5 characters long.";
        }
    }

    public static IEnumerable<string> ValidateUsersLogin(LoginDto loginDto)
    {
        var detector = new EmailAddressAttribute();
        if (!detector.IsValid(loginDto.Email))
        {
            yield return "The email address was entered wrong";
        }

        if (loginDto.Password.Length <= 4 || string.IsNullOrWhiteSpace(loginDto.Password))
        {
            yield return "The password has to be at least 5 characters long.";
        }
    }
}