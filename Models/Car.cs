using System.ComponentModel.DataAnnotations;

namespace gregslist_api.Models
{
  public class Car
  {
    public int Id { get; set; }
    [Required]
    public string Make { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public int Price { get; set; }
    public string Description { get; set; }
    [Required]
    public string ImgUrl { get; set; }
    public string UserId { get; set; }
  }
}