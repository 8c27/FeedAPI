using FeedAPI.Models;
using SEPVDB_Api.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SEPVDB_Api.Services
{
    public interface ILoginInfoService
    {
        public short BookId { get; set; }
        public List<LoginInfoViewModel> GetAllDatas();
        public List<LoginRolesViewModel> GetAllRoles();
        bool ValidateUser(LoginViewModel loginViewModel);
        public LoginInfoViewModel? GetData(string Username);
        public int GetRoleId(int user_id);
        public int SwitchRoleId(int user_id, int role_id);
        public void InsertData(LoginInfoViewModel viewModel);
        public bool DeleteData(int id);
        public bool EditData(int id, LoginInfoViewModel viewModel);
        public bool ChangePassword(int id, ChangePasswordViewModel viewModel, bool isAdmin = false);
    }
    public class LoginInfoService : ILoginInfoService
    {
        private readonly FeedingContext _context;
        public LoginInfoService (FeedingContext sEPVContext)
        {
            _context = sEPVContext;
        }

        public short BookId { get; set; }
        public List<LoginInfoViewModel> GetAllDatas()
        {
            return _context.LoginInfo.Select(t => new LoginInfoViewModel
            {
                Id = t.Id,
                Username = t.Username,
                Description = t.Description,
                Disabled = t.Disabled,
                Roles = t.LoginInfoRoles.Select(b => new LoginRolesViewModel
                {
                    Id = b.Role.Id,
                    Role_name = b.Role.RoleName,
                    Description = b.Role.Description,
                    Disabled = b.Role.Disabled,
                    Is_admin = b.Role.IsAdmin,
                }).ToList()
            }).ToList();
        }

        public List<LoginRolesViewModel> GetAllRoles()
        {
            var datas = _context.LoginRoles.Select(t => new LoginRolesViewModel
            {
                Id = t.Id,
                Role_name = t.RoleName,
                Description = t.Description,
                Disabled = t.Disabled,
                Is_admin = t.IsAdmin,
                Menus = new List<LoginMenus>()
            }).ToList();
            foreach (var data in datas)
            {
                var menus = new List<LoginMenus>();
                if (!data.Is_admin)
                    menus = _context.LoginRolesMenus.Where(a => a.RoleId == data.Id).Select(b => b.Menu).ToList();
                else
                    menus = _context.LoginMenus.ToList();
                data.Menus = menus;
            }
            return datas;
        }
        public bool ValidateUser(LoginViewModel loginViewModel)
        {
            var data = _context.LoginInfo
                .Where(p => p.Username == loginViewModel.Username && p.Password == loginViewModel.Password && !p.Disabled)
                .ToList();

            if (data == null)
            {
                return false;
            }

            if (data.Count != 1)
            {
                return false;
            }

            return true;
        }
        public LoginInfoViewModel? GetData(string Username)
        {
            return _context.LoginInfo
                .Where(p => p.Username == Username)
                .Select(
                t => new LoginInfoViewModel
                {
                    Id = t.Id,
                    Username = t.Username,
                    Description = t.Description,
                    Disabled = t.Disabled,
                }
                ).FirstOrDefault();
        }
        public int GetRoleId(int user_id)
        {
            var data = _context.LoginInfoRoles.Where(t => t.InfoId == user_id).Select(t => t.Role)
                .OrderByDescending(a => a.IsAdmin).FirstOrDefault();
            if (data == null) return -1;
            return data.Id;
        }

        public int SwitchRoleId(int user_id, int role_id)
        {
            var data = _context.LoginInfoRoles.Where(t => t.InfoId == user_id && t.RoleId == role_id).FirstOrDefault();
            if (data == null) return -1;
            return data.RoleId;
        }
        public void InsertData(LoginInfoViewModel viewModel)
        {
            var data = new LoginInfo
            {
                Username = viewModel.Username,
                Password = viewModel.Password,
                Description = viewModel.Description,
                Disabled = viewModel.Disabled
            };

            _context.LoginInfo.Add(data);
            _context.SaveChanges();
            if (viewModel.Roles != null)
            {
                InsertRoles(viewModel);
            }
        }
        private void InsertRoles(LoginInfoViewModel viewModel)
        {
            var myId = _context.LoginInfo.Where(a => a.Username == viewModel.Username).Select(b => b.Id).FirstOrDefault();
            foreach (var Role in viewModel.Roles)
            {
                var role_data = new LoginInfoRoles
                {
                    InfoId = myId,
                    RoleId = (int)Role.Id
                };
                _context.LoginInfoRoles.Add(role_data);
            }
            _context.SaveChanges();
        }

        public bool DeleteData(int id)
        {
            var data = _context.LoginInfo.Where(a => a.Id == id).FirstOrDefault();
            var roles = _context.LoginInfoRoles.Where(a => a.InfoId == id).ToList();
            foreach (var role in roles)
            {
                _context.LoginInfoRoles.Remove(role);
            }
            if (data == null) return false;
            _context.LoginInfo.Remove(data);
            _context.SaveChanges();
            return true;
        }

        public bool EditData(int id, LoginInfoViewModel viewModel)
        {
            var data = _context.LoginInfo.Where(a => a.Id == id).FirstOrDefault();
            if (data == null) return false;
            data.Username = viewModel.Username;
            data.Password = viewModel.Password ?? data.Password;
            data.Description = viewModel.Description;
            data.Disabled = viewModel.Disabled;

            var data_roles = _context.LoginInfoRoles.Where(t => t.InfoId == data.Id).ToList();
            _context.RemoveRange(data_roles);

            if (viewModel.Roles != null)
            {
                foreach (var Role in viewModel.Roles)
                {
                    if (!Role.Disabled)
                    {
                        var role_data = new LoginInfoRoles
                        {
                            InfoId = data.Id,
                            RoleId = (int)Role.Id
                        };
                        _context.LoginInfoRoles.Add(role_data);
                    }
                }
            }


            _context.SaveChanges();
            return true;
        }

        public bool ChangePassword(int id, ChangePasswordViewModel viewModel, bool isAdmin = false)
        {
            var data = _context.LoginInfo.Where(t => t.Id == id).FirstOrDefault();
            if (data == null) return false;
            if (data.Password != viewModel.Old_password && !isAdmin) return false;
            data.Password = viewModel.New_password;
            _context.SaveChanges();
            return true;
        }
    }
}
