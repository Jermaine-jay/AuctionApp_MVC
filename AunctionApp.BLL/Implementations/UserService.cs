using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private RoleManager<IdentityRole> _roleManager { get; }

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<User>();
            _BidRepo = _unitOfWork.GetRepository<Bid>();
            _ProductRepo = _unitOfWork.GetRepository<Product>();
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<(bool successful, string msg)> AddOrUpdateBidAsync(AddOrUpdateBidVM model)
        {
            var product = await _ProductRepo.GetSingleByAsync(u => u.Id == model.ProductId, include: u => u.Include(x => x.BidList), tracking: true);

            if (product == null)
            {
                return (false, $"User with id:{model.ProductId} wasn't found");
            }

            Bid? bid = product?.BidList?.SingleOrDefault(t => t.Bidder == model.Bidder);
            if (bid != null)
            {
                bid.BidPrice = model.BidPrice;
                var updated = _mapper.Map(model, bid);

                await _BidRepo.UpdateAsync(updated);
                return (true, "Update Successful!");
            }

            var newbid = _mapper.Map<Bid>(model);
            product?.BidList?.Add(newbid);
            var rowChanges = await _unitOfWork.SaveChangesAsync();

            return rowChanges > 0 ? (true, $"User: {model.Bidder} bid was successfully created!") : (false, "Failed To save changes!");
        }
        public async Task<(bool successful, string msg)> RegisterAdmin(RegisterVM register)
        {
            var newUser = _mapper.Map<User>(register);
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);

            await _userManager.AddToRoleAsync(newUser, "Admin");
            await _signInManager.SignInAsync(newUser, isPersistent: false);

            return result.Succeeded ? (true, "Admin created successfully!") : (false, "Failed to create Admin");
        }
        public async Task<(bool successful, string msg)> RegisterUser(RegisterVM register)
        {
            var newUser = _mapper.Map<User>(register);
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
            await _userManager.AddToRoleAsync(newUser, "User");
            await _signInManager.SignInAsync(newUser, isPersistent: false);

            return result.Succeeded ? (true, "User created successfully!") : (false, "Failed to create User");
        }
        public async Task<(bool successful, string msg)> SignIn(SignInVM signIn)
        {
            User user;
            if (signIn.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(signIn.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(signIn.UsernameOrEmail);
            }

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, signIn.Password, signIn.RememberMe, true);

                return result.Succeeded ? (true, $"{user.UserName} logged in successfully!") : (false, "Failed to login");
            }
            return (false, "User does not exist");
        }
        public async Task<(bool successful, string msg)> SignOut()
        {
            await _signInManager.SignOutAsync();
            return (true, $"logged out successfully!");
        }
        public async Task<User> CreateAUser(RegisterVM register)
        {
            User newUser = new User
            {
                UserName = register.Username,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                Address = register.Address,
                PhoneNumber = register.PhoneNumber,

            };
            return newUser;
        }
        public async Task<(bool successful, string msg)> Update(UserVM model)
        {
            var user = await _userRepo.GetSingleByAsync(u => u.Id == model.Id);
            if (user == null)
            {
                return (false, $"User with ID:{model.UserName} wasn't found");
            }

            var userupdate = _mapper.Map(model, user);
            var rowChanges = await _userRepo.UpdateAsync(userupdate);

            return rowChanges != null ? (true, $"User detail update was successful!") : (false, "Failed To save changes!");
        }
        public async Task<(bool successful, string msg)> Delete(string userId)
        {
            var user = await _userRepo.GetSingleByAsync(u => u.Id == userId);
            if (user == null)
            {
                return (false, $"User with user:{user?.UserName} wasn't found");
            }
            await _userRepo.DeleteAsync(user);
            return await _unitOfWork.SaveChangesAsync() >= 0 ? (true, $"{user.FirstName} was deleted") : (false, $"Delete Failed");
        }
        public async Task<UserVM> GetUser(string userId)
        {
            var user = await _userRepo.GetSingleByAsync(u => u.Id == userId);
            var Auser = _mapper.Map<UserVM>(user);

            return Auser;
        }
        public async Task<IEnumerable<UserVM>> GetUsers()
        {
            var users = await _userRepo.GetAllAsync();
            var userViewModels = users.Select(model => new UserVM
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.UserName,
                Address = model.Address

            });
            return userViewModels;
        }
        public async Task<UserVM> UserProfileAsync(string userId)
        {
            var u = await _userRepo.GetSingleByAsync(u => u.Id == userId);
            var useres = new UserVM()
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Address = u.Address,
                ProfilePicturePath = u.ProfileImagePath
            };
            return useres;
        }
        public async Task<(bool successful, string msg)> UpdateProfileImage(string userId, ProfileImageVM model)
        {
            var user = await _userRepo.GetSingleByAsync(u => u.Id == userId);
            var fileName = model?.ProfileImagePath?.FileName;
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "ProfileImages");

            Console.WriteLine("filename:{0} image path: {1}", fileName, imagePath);

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            string picPath = Path.Combine(imagePath, fileName);
            using (var stream = new FileStream(picPath, FileMode.Create))
            {
                await model.ProfileImagePath.CopyToAsync(stream);
            }
            if (user != null)
            {
                user.ProfileImagePath = fileName;
                var row = _mapper.Map<User>(user);
                var result = await _userRepo.UpdateAsync(user);
                return (true, "Profile picture updated!");
            }

            return (false, "couldn't update profile picture!");
        }

    }
}
