using System.ComponentModel.DataAnnotations;
namespace CitiesManager.WebAPI.Models;

public class City
{
    public Guid CityId { get; set; }
    [Required(ErrorMessage = "city name can't be blank")]
    public string? CityName { get; set; }
}