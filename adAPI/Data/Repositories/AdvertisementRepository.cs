using adAPI.Data.Models;

namespace adAPI.Data.Repositories
{
    public class AdvertisementRepository : IRepository<Advertisement>
    {
        private ApplicationDbContext _context;
        public AdvertisementRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Advertisement> GetItems()
        {
           return _context.Advertisements.ToList();
        }

        public Advertisement GetItemById(Guid itemId)
        {
            return _context.Advertisements.Find(itemId);
        }

        public void AddItem(Advertisement item)
        {
            _context.Advertisements.Add(item);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
