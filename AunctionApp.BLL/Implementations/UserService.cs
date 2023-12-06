using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AunctionApp.DAL.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AunctionApp.BLL.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Bid> _BidRepo;
        private readonly IRepository<Product> _ProductRepo;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<User>();
            _BidRepo = _unitOfWork.GetRepository<Bid>();
            _ProductRepo = _unitOfWork.GetRepository<Product>();
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _authenticationService = authenticationService;
        }

        public async Task<(bool successful, string msg)> AddOrUpdateBidAsync(AddOrUpdateBidVM model)
        {
            var product = await _ProductRepo.GetSingleByAsync(u => u.Id.ToString() == model.ProductId, include: u => u.Include(x => x.BidList), tracking: true);
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
            var verify = await _authenticationService.VerifyEmail(register.Email);
            if (verify == false)
            {
                return (false, "Invalid Email Address");
            }

            var newUser = _mapper.Map<User>(register);
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);

            if (result.Succeeded)
            {
                await _authenticationService.RegistrationMail(newUser);

                await _userManager.AddToRoleAsync(newUser, "Admin");
                return result.Succeeded ? (true, "Admin created successfully!, Verification Mail Sent") : (false, "Failed to create Admin, Couldn't Send Mail");
            }
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    return (false, $"Failed to create Admin.{error.Description}");
                }
            }
            return (false, $"Failed to create Admin");
        }

        public async Task<(bool successful, string msg)> RegisterUser(RegisterVM register)
        {
            var verify = await _authenticationService.VerifyEmail(register.Email);
            if (verify == false)
            {
                return (false, "Invalid Email Address");
            }

            var newUser = _mapper.Map<User>(register);
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);

            if (result.Succeeded)
            {
                await _authenticationService.RegistrationMail(newUser);

                await _userManager.AddToRoleAsync(newUser, "User");
                return result.Succeeded ? (true, "User created successfully!, Verification Mail Sent") : (false, "Failed to create User, Couldn't Send Mail");
            }

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    return (false, $"Failed to create User.{error.Description}");
                }
            }
            return (false, $"Failed to create User");
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

            if (user != null && user.EmailConfirmed == true)
            {
                var result = await _signInManager.PasswordSignInAsync(user, signIn.Password, signIn.RememberMe, true);
                return result.Succeeded ? (true, $"{user.UserName} logged in successfully!") : (false, "UserName or password is incorrect");
            }
            return (false, "Unconfirmed Email Address");
        }

        public async Task<(bool successful, string msg)> SignOut()
        {
            await _signInManager.SignOutAsync();
            return (true, $"logged out successfully!");
        }

        public async Task<(bool successful, string msg)> Update(UserVM model)
        {
            var verify = await _authenticationService.VerifyEmail(model.Email);
            if (verify == false)
            {
                return (false, "Invalid Email Address");
            }
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
            var userViewModels = users.OrderByDescending(u => u.UserName).Select(model => new UserVM
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.UserName,
                Address = model.Address,

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
                ProfileImagePath = u.ProfileImagePath
            };
            return useres;
        }

        public async Task<(bool successful, string msg)> UpdateProfileImage(ProfileImageVM model, string userId)
        {
            var user = await _userRepo.GetSingleByAsync(u => u.Id == userId);
            if (user == null)
            {
                return (true, "User Does not exist!");
            }


            var fileName = model.ProfileImagePath.FileName;
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "ProfileImages");


            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            var existing = Path.Combine(imagePath, user.ProfileImagePath);
            if (user.ProfileImagePath != null || user.ProfileImagePath != "Blank-Pfp.jpg")
            {
                File.Delete(existing);
            }

            string picPath = Path.Combine(imagePath, fileName);
            using (var stream = new FileStream(picPath, FileMode.Create))
            {
                await model.ProfileImagePath.CopyToAsync(stream);
            }


            user.ProfileImagePath = model.ProfileImagePath.FileName;
            var result = await _userRepo.UpdateAsync(user);
            return (true, "Profile picture updated!");
        }

    }
}