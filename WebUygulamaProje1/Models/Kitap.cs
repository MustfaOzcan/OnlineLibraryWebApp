using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUygulamaProje1.Models
{
	public class Kitap
	{
		[Key]
        public int Id { get; set; }

		[Required]
		public string KitapAdi { get; set; }

		[Required]
		public string Yazar {  get; set; }

		[Required]
		[Range(0,5000)]
		public double Fiyat { get; set; }	

		public string Tanim { get; set; }

		//Foreign Key iliskisi
		[ValidateNever]//validation yapmasın
		public int KitapTuruId { get; set; }
		
		[ForeignKey("KitapTuruId")]
		[ValidateNever]//validation yapmasın
		public KitapTuru KitapTuru { get; set; }

		[ValidateNever]
		public string ResimURL { get; set; }

    }
}
