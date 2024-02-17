using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KiralamaController : Controller
	{
		private readonly IKiralamaRepository _kiralamaRepository;
		private readonly IKitapRepository _kitapRepository;//foreign key 
		public readonly IWebHostEnvironment _webHostEnvironment;

		public IActionResult Index()
		{
			//List<Kitap> objKitapList=_kitapRepository.GetAll().ToList();//kitap listesini alıyor
			List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();//kiralama listesi 
			return View(objKiralamaList);
		}


		public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
		{
			this._kiralamaRepository = kiralamaRepository;
			this._kitapRepository = kitapRepository;
			_webHostEnvironment = webHostEnvironment;

		}
		//Get

		public IActionResult EkleGuncelle(int? id)
		{
			IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().Select(k => new SelectListItem //kitap  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = k.KitapAdi,
				Value = k.Id.ToString(),



			}) ;
			ViewBag.KitapList = KitapList;

			if (id == null || id == 0)//ekleme
			{
				return View();//ekleme
			}//ekleme
			else
			{						//guncelleme
				//guncelleme
				Kiralama? kiralamavt = _kiralamaRepository.Get(u => u.Id == id);
				if (kiralamavt == null)
				{
					return NotFound();
				}
				return View(kiralamavt);

			}
			//guncelleme
			
		}
		[HttpPost]
		public IActionResult EkleGuncelle(Kiralama kiralama)
		{

			//var errors =ModelState.Values.SelectMany(x => x.Errors);//hata takibi

			if (ModelState.IsValid)
			{
				

				if (kiralama.Id==0)
				{//kitap ekleniyor
					_kiralamaRepository.Ekle(kiralama);
					TempData["basarili"] = "Yeni Kiralama başarıyla oluşturuldu";
				}
				else
				{
					_kiralamaRepository.Guncelle(kiralama);
					TempData["basarili"] = "Kiralama güncelleme başarılı";
				}

				_kiralamaRepository.Kaydet();
				return RedirectToAction("Index","Kiralama");
			}
			return View();
		}

	

		public IActionResult Sil(int? id)
		{
			IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().Select(k => new SelectListItem //kitap  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = k.KitapAdi,
				Value = k.Id.ToString(),



			});
			ViewBag.KitapList = KitapList;////kitapla ilgili verileri cekiyor burdan sonra cshtml dosyasında viewbag action yapıyoruz(kitap id kısmı


			if (id == null || id == 0)
			{
				return NotFound();

			}
			Kiralama kiralama = _kiralamaRepository.Get(u => u.Id == id);
			if (kiralama == null)
			{
				return NotFound();
			}
			return View(kiralama);
		}

		[HttpPost, ActionName("Sil")]
		public IActionResult SilPOST(int? id)
		{
			Kiralama kiralama = _kiralamaRepository.Get(u => u.Id == id);
			if (kiralama == null) return NotFound();
			_kiralamaRepository.Sil(kiralama);
			_kiralamaRepository.Kaydet();
			TempData["basarili"] = "Kayit Silme basarili";
			return RedirectToAction("Index", "Kiralama");

		}


	}
}
