﻿using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AunctionApp.DAL.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<AuctionVMForm>> GetAuctions()
        {
            var aunction = await _ProductRepo.GetAllAsync();
            var aunctionViewModels = aunction.OrderBy(x=>x.CreatedAt).Select(model => new AuctionVMForm
            {
                Id = model.Id.ToString(),
                ProductImagePath = model.ProductImagePath,
                ProductName = model.ProductName,
                Description = model.Description,
                ActualAmount = model.ActualAmount,
                Status = model.IsSold ? "Sold" : "Not Sold"
            });
            return aunctionViewModels;
        }

        public async Task<AuctionVMForm> GetAuction(string productId)
        {
            var aunction = await _ProductRepo.GetSingleByAsync(u => u.Id.ToString() == productId);
            var res = new AuctionVMForm
            {
                Id = aunction.Id.ToString(),
                ProductName = aunction.ProductName,
                ActualAmount = aunction.ActualAmount,
                Description = aunction.Description,
                ProductImagePath = aunction.ProductImagePath,
                Status = aunction.IsSold ? "Sold" : "Not sold",
            };
            return res;
        }

        public async Task<IEnumerable<AuctionWithBidVM>> GetAuctionsWithBidsAsync()
        {
            var actions = await _ProductRepo.GetAllAsync(include: u => u.Include(t => t.BidList));
            return actions.OrderBy(x=>x.CreatedAt).Select(u => new AuctionWithBidVM
            {
                Id = u.Id.ToString(),
                ProductName = u.ProductName,
                ActualAmount = u.ActualAmount,
                Description = u.Description,
                ProductImagePath = u.ProductImagePath,
                Status = u.IsSold ? "Sold" : "Not sold",
                Bids = u.BidList.OrderByDescending(u => u.BidPrice).Select(t => new BidVM
                {
                    Bidder = t.Bidder,
                    BidPrice = t.BidPrice,
                    BidTime = t.BidTime.ToString("dd MMMM yyyy HH:mm:ss"),
                }).ToList()
            });
        }

        public async Task<IEnumerable<UserBidsVM>> GetUserBidsAsync(string bidder)
        {
            var actions = await _ProductRepo.GetAllAsync(include: u => u.Include(t => t.BidList));
            var productDetails = actions.Where(p => p.BidList != null && p.BidList.Any(b => b.Bidder == bidder))
                                .Select(p => new UserBidsVM
                                {
                                    ProductName = p.ProductName,
                                    ActualAmount = p.ActualAmount,
                                    BidPrice = p.BidList.First(b => b.Bidder == bidder).BidPrice,
                                    BidTime = p.BidList.First(b => b.Bidder == bidder).BidTime.ToString("dd MMMM yyyy HH:mm:ss")
                                });
            return productDetails;
        }

        public async Task<(bool successful, string msg)> AddOrUpdateAsync(AddOrUpdateBidVM model)
        {

            Product product = await _ProductRepo.GetSingleByAsync(u => u.Id.ToString() == model.ProductId, include: u => u.Include(x => x.BidList), tracking: true);
            if (product == null)
            {
                return (false, $"User with id:{model.ProductId} wasn't found");
            }

            if (int.Parse(model.BidPrice) <= int.Parse(product.ActualAmount))
            {
                return (false, "Bid amount should be more than initial price");
            }

            Bid? bid = product.BidList?.SingleOrDefault(t => t.Bidder == model.Bidder);
            if (bid != null)
            {
                var update = _mapper.Map(model, bid);
                await _BidRepo.UpdateAsync(update);
                return (true, "Update Successful!");
            }


            var newBid = _mapper.Map<Bid>(model);
            product.BidList.Add(newBid);
            var rowChanges = await _unitOfWork.SaveChangesAsync();
            return rowChanges > 0 ? (true, $"Bid: {model.ProductId} was successfully created!") : (false, "Failed To save changes!");
        }
    }
}
