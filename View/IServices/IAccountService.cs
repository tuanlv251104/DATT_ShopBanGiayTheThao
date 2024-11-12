using Data.ViewModels;
using DataProcessing.Models;
using Microsoft.AspNetCore.Identity;

namespace View.IServices
{
    public interface IAccountService
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        public Task<string> SignInAsync(SignInModel signInModel);
        public Task SignOutAsync();
        public Task CreateEmployee(CreateAccountModelcs models);
        public Task<List<ApplicationUser>> GetAllEmployee();
        public Task<ApplicationUser> GetById(Guid idAccount);
        public Task<ApplicationUser> Update(ApplicationUser account , Guid idAccount);
        public Task Delete(Guid idAccount);
        public Task CreateCustomer(CreateAccountModelcs models);
        public Task<List<ApplicationUser>> GetAllCustomer();
    }
}
