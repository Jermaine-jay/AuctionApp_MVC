using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AutoMapper;
using TodoList.DAL.Repository;

namespace AunctionApp.BLL.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _ProductRepo;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

        public async Task<(bool Done, string msg)> ToggleProductStatus(int productId)
        {
            var aunction = await _ProductRepo.GetSingleByAsync(u => u.Id == productId, tracking: true);

            if (aunction != null)
            {
                //_mapper.Map<AunctionVMForm>(aunction);

                aunction.IsSold = !aunction.IsSold;
                var rowChanges = await _unitOfWork.SaveChangesAsync();
                return rowChanges > 0 ? (true, "Status updated") : (false, "Status not updated");
            }
            throw new NotImplementedException();
        }

    }
}
