﻿using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Models
{
	public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository
	{
		private UygulamaDbContext _uygulamaDbContext;

		public KiralamaRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
		{
			this._uygulamaDbContext = uygulamaDbContext;
		}

		public void Guncelle(Kiralama kiralama)
		{
			_uygulamaDbContext.Update(kiralama);
		}

		public void Kaydet()
		{
			_uygulamaDbContext.SaveChanges();
		}

	}
}
