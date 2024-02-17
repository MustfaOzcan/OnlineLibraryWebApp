using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Controllers
{
	
	public class KitapController : Controller
	{
		private readonly IKitapRepository _kitapRepository;
		private readonly IKitapTuruRepository _kitapTuruRepository;
		public readonly IWebHostEnvironment _webHostEnvironment;

       /* [Authorize(Roles = "Admin,Ogrenci")]*///ADmin de girer Ogrenci de (roller)
        public IActionResult Index()
		{
			//List<Kitap> objKitapList=_kitapRepository.GetAll().ToList();//kitap listesini alıyor
			List<Kitap> objKitapList = _kitapRepository.GetAll(includeProps:"KitapTuru").ToList();//kitap listesini alıyor
			return View(objKitapList);
		}


		public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository,IWebHostEnvironment webHostEnvironment)
		{
			this._kitapRepository = kitapRepository;
			this._kitapTuruRepository =kitapTuruRepository;
			_webHostEnvironment = webHostEnvironment;

		}

        [Authorize(Roles = UserRoles.Role_Admin)]//sadece admin girebilir
        public IActionResult EkleGuncelle(int? id)
		{
			IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll().Select(k => new SelectListItem //kitaptürü  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = k.Ad,
				Value = k.Id.ToString(),



			}) ;
			ViewBag.KitapTuruList = KitapTuruList;

			if (id == null || id == 0)//ekleme
			{
				return View();//ekleme
			}//ekleme
			else
			{						//guncelleme
				//guncelleme
				Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
				if (kitapVt == null)
				{
					return NotFound();
				}
				return View(kitapVt);

			}
			//guncelleme
			
		}
		[HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]//sadece admin girebilir
        public IActionResult EkleGuncelle(Kitap kitap,IFormFile? file)
		{

			//var errors =ModelState.Values.SelectMany(x => x.Errors);//hata takibi

			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				string kitapPath = Path.Combine(wwwRootPath, @"img");

				//var errors =ModelState.Values.SelectMany(x => x.Errors);//hata takibi
				if (file != null) 
				{
					using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
					{
						file.CopyTo(fileStream);

					}
					kitap.ResimURL = @"\img\" + file.FileName;
				}
				

				if (kitap.Id==0)
				{//kitap ekleniyor
					_kitapRepository.Ekle(kitap);
					TempData["basarili"] = "Yeni Kitap başarıyla oluşturuldu";
				}
				else
				{
					_kitapRepository.Guncelle(kitap);
					TempData["basarili"] = "Kitap güncelleme başarılı";
				}

				_kitapRepository.Kaydet();
				return RedirectToAction("Index","Kitap");
			}
			return View();
		}

        //public IActionResult Guncelle(int? id)
        //{
        //	if (id == null || id == 0)
        //	{
        //		return NotFound();

        //	}
        //	Kitap ?kitapVt = _kitapRepository.Get(u => u.Id == id);
        //	if (kitapVt == null)
        //	{
        //		return NotFound();
        //	}
        //	return View(kitapVt);
        //}

        //[HttpPost]
        //public IActionResult Guncelle(Kitap kitap)
        //{
        //	if (ModelState.IsValid) 
        //	{
        //		_kitapRepository.Guncelle(kitap);
        //		_kitapRepository.Kaydet();
        //		TempData["basarili"] = "Güncelleme basarili!";
        //		return RedirectToAction("Index", "Kitap");
        //	}
        //	return View();
        //}
        [Authorize(Roles = UserRoles.Role_Admin)]//sadece admin girebilir
        public IActionResult Sil(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();

			}
			Kitap kitap = _kitapRepository.Get(u => u.Id == id);
			if (kitap == null)
			{
				return NotFound();
			}
			return View(kitap);
		}

		[HttpPost, ActionName("Sil")]
        [Authorize(Roles = UserRoles.Role_Admin)]//sadece admin girebilir
        public IActionResult SilPOST(int? id)
		{
			Kitap kitap = _kitapRepository.Get(u => u.Id == id);
			if (kitap == null) return NotFound();
			_kitapRepository.Sil(kitap);
			_kitapRepository.Kaydet();
			TempData["basarili"] = "Kayit Silme basarili";
			return RedirectToAction("Index", "Kitap");

		}


		public IActionResult Detay(int? id)
		{
			IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll().Select(k => new SelectListItem //kitaptürü  alıyor
			{                                                                                                   //drop-down şeklinde	
				Text = k.Ad,
				Value = k.Id.ToString(),



			});
			ViewBag.KitapTuruList = KitapTuruList;

			if (id == null || id == 0)//ekleme
			{
				return View();//ekleme
			}//ekleme
			else
			{                       //guncelleme
									//guncelleme
				Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
				if (kitapVt == null)
				{
					return NotFound();
				}
				return View(kitapVt);

			}
			//guncelleme

		}

	}
}
