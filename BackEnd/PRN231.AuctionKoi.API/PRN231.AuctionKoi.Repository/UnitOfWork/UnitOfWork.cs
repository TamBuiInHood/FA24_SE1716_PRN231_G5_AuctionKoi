using PRN231.AuctionKoi.Repository.Entities;
using PRN231.AuctionKoi.Repository.Repositories;
using Microsoft.Extensions.Configuration;


namespace PRN231.AuctionKoi.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private  AuctionKoiOfficialContext _context;

        private PaymentRepository _paymentRepo;
        private ProposalRepository _proposalRepo;
        //private GenericRepository<Category> _categoryRepo;

        public UnitOfWork(AuctionKoiOfficialContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
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

        public PaymentRepository PaymentRepository
        {
            get
            {
                if (_paymentRepo == null)
                {
                    this._paymentRepo = new PaymentRepository(_context);
                }
                return _paymentRepo;
            }
        }

        public ProposalRepository ProposalRepository
        {
            get
            {
                if(_proposalRepo == null)
                {
                    this._proposalRepo = new ProposalRepository(_context);
                }
                return _proposalRepo;
            }
        }

        //GenericRepository<Category> IUnitOfWork.CategoryRepository
        //{
        //    get
        //    {
        //        if (_categoryRepo == null)
        //        {
        //            this._categoryRepo = new GenericRepository<Category>(_context);
        //        }
        //        return _categoryRepo;
        //    }
        //}

    }
}
