using System.ComponentModel.DataAnnotations;

namespace ProfileService.Business.Models;

public class UpdateProfileRequest
{
    [RegularExpression(
        @"^[A-Za-zÅÄÖåäö\s-]{2,50}$",
        ErrorMessage = "Förnamn får bara innehålla bokstäver och måste vara 2–50 tecken."
    )]
    public string? FirstName { get; set; }

    [RegularExpression(
        @"^[A-Za-zÅÄÖåäö\s-]{2,50}$",
        ErrorMessage = "Efternamn får bara innehålla bokstäver och måste vara 2–50 tecken."
    )]
    public string? LastName { get; set; }

    [RegularExpression(
        @"^[0-9+\-\s]{6,20}$",
        ErrorMessage = "Telefonnummer får bara innehålla siffror, mellanslag, + och -."
    )]
    public string? Phone { get; set; }

    [Url(ErrorMessage = "Bild måste vara en giltig URL.")]
    public string? ImageUrl { get; set; }
}