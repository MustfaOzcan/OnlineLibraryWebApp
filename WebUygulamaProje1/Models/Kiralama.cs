using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUygulamaProje1.Models
{
	
	public class Kiralama
	{
		[Key]
        public int Id { get; set; } //kiralama id

		[Required]
		public int OgrenciId { get; set; }

		[ValidateNever]
		public int KitapId { get; set; }
		[ForeignKey("KitapId")]

		[ValidateNever]
		public Kitap Kitap { get; set; }
		//bir öğrenci birden fazla kitap alabilir
		//bi kitap en çok bir örğ

		

    }
}
