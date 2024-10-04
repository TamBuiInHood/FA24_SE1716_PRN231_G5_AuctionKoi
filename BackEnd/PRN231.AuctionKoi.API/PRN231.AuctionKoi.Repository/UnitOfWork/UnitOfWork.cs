using PRN231.AuctionKoi.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using KoiAuction.Repository.Entities;
using KoiAuction.Repository.Repositories;


namespace PRN231.AuctionKoi.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private Fa24Se1716Prn231G5KoiauctionContext _context;

        private PaymentRepository _paymentRepo;
        private ProposalRepository _proposalRepo;
        private UserAuctionRepository _userAuctionRepo;
        private UserRepository _userRepo;
        private DetailProposalRepository _detailProposalRepo;

        private OrderRepository _orderRepo;
        private OrderDetailRepository _orderDetailRepo;

        //private GenericRepository<Category> _categoryRepo;

        public UnitOfWork(Fa24Se1716Prn231G5KoiauctionContext context, IConfiguration configuration)
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
                if (_proposalRepo == null)
                {
                    this._proposalRepo = new ProposalRepository(_context);
                }
                return _proposalRepo;
            }
        }

        public UserAuctionRepository UserAuctionRepository
        {
            get
            {
                if (_userAuctionRepo == null)
                {
                    this._userAuctionRepo = new UserAuctionRepository(_context);
                }
                return _userAuctionRepo;
            }
        }
        public OrderRepository OrderRepository
        {
            get
            {
                if (_orderRepo == null)
                {
                    _orderRepo = new OrderRepository(_context);
                }
                return _orderRepo;
            }

        }
        public OrderDetailRepository OrderDetailRepository
        {
            get
            {
                if (_orderDetailRepo == null)
                {
                    _orderDetailRepo = new OrderDetailRepository(_context);
                }
                return _orderDetailRepo;
            }

        }



        public UserRepository UserRepository
        {
            get
            {
                if (_userRepo == null)
                {
                    this._userRepo = new UserRepository(_context);
                }
                return _userRepo;
            }
        }

        public DetailProposalRepository DetailProposalRepository
        {
            get
            {
                if (_detailProposalRepo == null)
                {
                    this._detailProposalRepo = new DetailProposalRepository(_context);
                }
                return _detailProposalRepo;
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