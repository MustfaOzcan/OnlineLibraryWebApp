using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUygulamaProje1.Models;
//veri tabanında EF Tablo olusturması için ilgili model sınıflarınnızı buraya eklemelisin
//ekledikten sonra add migration yap sonra update database yap
namespace WebUygulamaProje1.Utility
{
	//veri tabanı ile entities arasında olusturulan köprü
	public class UygulamaDbContext : IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<KitapTuru> KitapTurleri { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Kiralama>Kiralamalar{ get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
       
        public DbSet<State> States { get; set; }

		public DbSet<Country> Countries { get; set; }

		public DbSet<City> Cities { get; set; }


        
    }
}
