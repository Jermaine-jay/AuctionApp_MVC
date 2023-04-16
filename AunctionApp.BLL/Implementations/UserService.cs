using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Repository;

namespace AunctionApp.BLL.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Bid> _BidRepo;
        private readonly IRepository<Product> _ProductRepo;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<User>();
            _BidRepo = _unitOfWork.GetRepository<Bid>();
            _ProductRepo = _unitOfWork.GetRepository<Product>();
        }
        public async Task<(bool successful, string msg)> AddOrUpdateBidAsync(AddOrUpdateBidVM model)
        {
            var product = await _ProductRepo.GetSingleByAsync(u => u.Id == model.ProductId, include: u => u.Include(x => x.Bid), tracking: true);

            if (product == null)
            {
                return (false, $"User with id:{model.ProductId} wasn't found");
            }

            var bid = product.Bid.SingleOrDefault(t => t.Bidder == model.BidderUsername);
            if (bid != null)
            {
                bid.BidPrice = model.BidPrice;

               var updated = _mapper.Map(model, bid);
                await _BidRepo.UpdateAsync(updated);
                return (true, "Update Successful!");
            }

            var newbid = _mapper.Map<Bid>(model);
            product.Bid.Add(newbid);
            var rowChanges = await _unitOfWork.SaveChangesAsync();

            return rowChanges > 0 ? (true, $"User: {model.BidderUsername} bid was successfully created!") : (false, "Failed To save changes!");
        }

        public async Task<(bool successful, string msg)> Create(UserVM model)
        {
            var user = _mapper.Map<User>(model);
            var rowChanges = await _userRepo.AddAsync(user);

            return rowChanges != null ? (true, $"User: {model.FirstName} was successfully created!") : (false, "Failed To create user!");
        }

        public async Task<(bool successful, string msg)> Update(UserVM model)
        {
            var user = await _userRepo.GetSingleByAsync(u => u.Id == model.Id);
            if (user == null)
            {
                return (false, $"User with ID:{model.Id} wasn't found");
            }

            var userupdate = _mapper.Map(model, user);

            var rowChanges = await _userRepo.UpdateAsync(userupdate);

            return rowChanges != null ? (true, $"User detail update was successful!") : (false, "Failed To save changes!");
        }
        public async Task<(bool successful, string msg)> Update(int userId)
        {
            var user = await _userRepo.GetSingleByAsync(u => u.Id == userId);

            if (user == null)
            {
                return (false, $"User with user:{user.Id} wasn't found");
            }

            await _userRepo.DeleteAsync(user);
            return await _unitOfWork.SaveChangesAsync() >= 0 ? (true, $"{user.FirstName} was deleted") : (false, $"Delete Failed");
        }
    }
}
