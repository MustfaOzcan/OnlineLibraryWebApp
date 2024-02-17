using System.ComponentModel.DataAnnotations;

namespace WebUygulamaProje1.Models
{
    public class KitapTuru
    {
        [Key]//primary key
        public int Id { get; set; }


        [Required(ErrorMessage="Kitap Türü Adı alanı boş bırakılamaz.")]//not null
        [MaxLength(30)]
        public string Ad { get; set; }
        
    }
}
