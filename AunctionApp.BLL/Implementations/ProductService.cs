using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Repository;

namespace AunctionApp.BLL.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Bid> _BidRepo;
        private readonly IRepository<Product> _ProductRepo;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _BidRepo = _unitOfWork.GetRepository<Bid>();
            _ProductRepo = _unitOfWork.GetRepository<Product>();
        }
       

        public async Task<IEnumerable<AunctionVMForm>> GetAunctions()
        {
            var aunction = await _ProductRepo.GetAllAsync();
            var aunctionViewModels = aunction.Select(model => new AunctionVMForm
            {
                ProductImage = model.ProductImagePath,
                ProductName = model.ProductName,
                Description = model.Description,
                Status = model.IsSold? "Sold" : "Not Sold"
            });
            return aunctionViewModels;
        }

        public async Task<AunctionVMForm> GetAunction(int productId)
        {
            var user = await _ProductRepo.GetSingleByAsync(u => u.Id == productId);
            var Auser = _mapper.Map<AunctionVMForm>(user);

            return Auser;
        }

        public async Task<IEnumerable<AunctionWithBidVM>> GetAunctionsWithBidsAsync()
        {

            return (await _ProductRepo.GetAllAsync(include: u => u.Include(t => t.BidList))).Select(u => new AunctionWithBidVM
            {
                ProductName = u.ProductName,
                ActualAmount = u.ActualAmount,
                Description = u.Description,
                ProductImage = u.ProductImagePath,
                Bids = u.BidList.Select(t => new BidVM
                {
                    Bidder = t.Bidder,
                    BidPrice = t.BidPrice,
                    BidTime = t.BidTime.ToString("d"),
                })
            });
        }
    }
}
