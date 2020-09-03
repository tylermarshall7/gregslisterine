using System.ComponentModel.DataAnnotations;

namespace gregslist_api.Models
{
  public class House
  {
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public string Description { get; set; }
    public string ImgUrl { get; set; } = "no image";
    public string UserId { get; set; }
  }
}