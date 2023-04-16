using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TodoList.DAL.Repository;

namespace AunctionApp.BLL.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Bid> _BidRepo;
        private readonly IRepository<Product> _ProductRepo;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<User>();
            _BidRepo = _unitOfWork.GetRepository<Bid>();
            _ProductRepo = _unitOfWork.GetRepository<Product>();
        }
        public async Task<(bool successful, string msg)> CreateAunctionAsync(AunctionVM model)
        {

            var fileName = Path.GetFileName(model.ProductImage.FileName);
            var imagePath = Path.Combine(Environment.CurrentDirectory, "UploadedFiles", fileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await model.ProductImage.CopyToAsync(stream);
            }

            AunctionVMForm form = new()
            {
                ProductImage = "/UploadedFiles/" + fileName, // <-- Use fileName instead of "/images/" + fileName
                ProductName = model.ProductName,
                Description = model.Description,
                ActualAmount = model.ActualAmount,
            };

            var product = _mapper.Map<Product>(form);
            var rowChanges = await _ProductRepo.AddAsync(product);

            return rowChanges != null ? (true, "Aunction created successfully!") : (false, "Failed to create Aunction");
        }

        public async Task<(bool successful, string msg)> UpdateAunctionAsync(AunctionVM model)
        {
            var product = await _ProductRepo.GetSingleByAsync(u => u.Id == model.ProductId);
            if (product == null)
            {
                return (false, $"Product with ID:{model.ProductId} wasn't found");
            }

            var fileName = Path.GetFileName(model.ProductImage.FileName);
            var imagePath = Path.Combine(Environment.CurrentDirectory, "UploadedFiles", fileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await model.ProductImage.CopyToAsync(stream);
            }

            AunctionVMForm form = new()
            {
                ProductImage = "/UploadedFiles/" + fileName, // <-- Use fileName instead of "/images/" + fileName
                ProductName = model.ProductName,
                Description = model.Description,
                ActualAmount = model.ActualAmount,
            };

            var updateproduct = _mapper.Map(form, product);
            var rowChanges = await _ProductRepo.UpdateAsync(updateproduct);

            return rowChanges != null ? (true, "Aunction created successfully!") : (false, "Failed to create Aunction");

        }

        public async Task<(bool successful, string msg)> DeleteAunctionAsync(int productId)
        {
            var aunction = await _ProductRepo.GetSingleByAsync(u => u.Id == productId);

            if (aunction == null)
            {
                return (false, $"Aunction with user:{aunction.Id} wasn't found");
            }

            await _ProductRepo.DeleteAsync(aunction);
            return await _unitOfWork.SaveChangesAsync() >= 0 ? (true, $"{aunction.ProductName} was deleted") : (false, $"Delete Failed");
            
        }

        public async Task<IEnumerable<UserVM>> GetUsers()
        {
            var users = await _userRepo.GetAllAsync();
            var userViewModels = users.Select(model => new UserVM
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.UserName,

            });
            return userViewModels;
        }

        public async Task<IEnumerable<AunctionVMForm>> GetAunctions()
        {
            var aunction = await _ProductRepo.GetAllAsync();
            var aunctionViewModels = aunction.Select(model => new AunctionVMForm
            {
                ProductImage = model.ProductImagePath,
                ProductName = model.ProductName,
                Description = model.Description,

            });
            return aunctionViewModels;
        }

        public async Task<IEnumerable<AunctionWithBidVM>> GetUsersWithTasksAsync()
        {

            return (await _ProductRepo.GetAllAsync(include: u => u.Include(t => t.BidList))).Select(u => new AunctionWithBidVM
            {
                ProductName = u.ProductName,
                ActualAmount = u.ActualAmount,
                Description = u.Description,
                Bids = u.BidList.Select(t => new BidVM
                {
                    Bidder= t.Bidder,
                    BidPrice = t.BidPrice,
                    BidTime = t.BidTime.ToString("d"),                   
                })
            });
        }
        public Task<(bool Done, string msg)> ToggleProductStatus(int productId)
        {
            throw new NotImplementedException();
        }

    }
}
