using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class ProductImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; } = null!;
    //"img/1.jpeg"
    [Required]
    public int ProductId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; } = null!;

    /*
     Додати зображення до бази даних, та вивести товари через контроллер Product
    в views в таблиці повинні відображатися зображення
    Додати сервіс CRUD для картинок
     */
}
