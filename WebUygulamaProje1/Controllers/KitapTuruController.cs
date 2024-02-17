using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Controllers
{
	[Authorize(Roles = UserRoles.Role_Admin)]
    public class KitapTuruController : Controller
    {
        
        private readonly IKitapTuruRepository _kitapTuruRepository;//nesnemiz 
        public KitapTuruController(IKitapTuruRepository context)
        {
			_kitapTuruRepository = context;
        }

         
        public IActionResult Index()
        {
            List<KitapTuru> objKitapTuruList = _kitapTuruRepository.GetAll().ToList();
            return View(objKitapTuruList);
        }

        //EKLE

        public IActionResult Ekle() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Ekle(kitapTuru);
				_kitapTuruRepository.Kaydet();//SaveChanges yapmaz isen bilgiler veri tabanına eklenmez.
                TempData["basarili"] = "Yeni Kitap Türü başarıyla oluşturuldu.";
                return RedirectToAction("Index", "KitapTuru");//KiitapTuru controller'ın Index metodunu cagirir
            }
            return View();

			//_uygulamaDbContext.KitapTurleri.Add(kitapTuru);
			//_uygulamaDbContext.SaveChanges();//SaveChanges yapmaz isen bilgiler veri tabanına eklenmez.
			//return RedirectToAction("Index", "KitapTuru");//KiitapTuru controller'ın Index metodunu cagirir
		}

        //GÜNCELLE

		public IActionResult Guncelle(int? id)
		{
            if (id == null || id == 0) return NotFound();//id 0 null kontrolü
            KitapTuru? kitapTuruVT = _kitapTuruRepository.Get(u=>u.Id==id);//parametre id eşit olan id'yi getir
            if (kitapTuruVT == null) 
            { 
                return NotFound();
            }
			return View(kitapTuruVT);
		}


		[HttpPost]
		public IActionResult Guncelle(KitapTuru kitapTuru)
        {

            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Guncelle(kitapTuru);
                _kitapTuruRepository.Kaydet();//SaveChanges yapmaz isen bilgiler veri tabanına eklenmez.
				TempData["basarili"] = "Yeni Kitap Türü başarıyla güncellendi.";
				return RedirectToAction("Index", "KitapTuru");//KiitapTuru controller'ın Index metodunu cagirir
            }
            return View();

        }

        //SİL

		public IActionResult Sil(int? id)
		{
			if (id == null || id == 0) return NotFound();//id 0 veya null kontrolü (asp-route-id index.html )
			KitapTuru? kitapTuruVT = _kitapTuruRepository.Get(u=>u.Id==id);
			if (kitapTuruVT == null)
			{
				return NotFound();
			}
			return View(kitapTuruVT);
		}


		[HttpPost,ActionName("Sil")]
		public IActionResult SilPOST(int? id)
		{
            KitapTuru? kitapTuru = _kitapTuruRepository.Get(u => u.Id == id);
            if (kitapTuru == null) { return NotFound(); }
            _kitapTuruRepository.Sil(kitapTuru);
            //_uygulamaDbContext.KitapTurleri.Remove(kitapTuru);
            _kitapTuruRepository.Kaydet();
           // _uygulamaDbContext.SaveChanges();
			TempData["basarili"] = "Silme işlemi başarılı.";
			return RedirectToAction("Index", "KitapTuru");

		}

	}
}
