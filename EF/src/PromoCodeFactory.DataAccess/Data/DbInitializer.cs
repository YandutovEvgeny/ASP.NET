using PromoCodeFactory.DataAccess.Data.Abstractions;

namespace PromoCodeFactory.DataAccess.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DataContext _context;

        public DbInitializer(DataContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.AddRange(FakeDataFactory.Employees);
            _context.SaveChanges();

            _context.AddRange(FakeDataFactory.Preferences);
            _context.SaveChanges();

            _context.AddRange(FakeDataFactory.Customers);
            _context.SaveChanges();
        }
    }
}
