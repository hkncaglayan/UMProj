using DataAccessLayer.EF;
using Entities;
using Entities.ResponseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class UserManager
    {
        private Repository<User> repo_user = new Repository<User>();
        public List<User> GetUsers()
        {
            return repo_user.List(g => g.Deleted == null);
        }

        public ResponseResult<UserModel> GetUserById(int id)
        {
            ResponseResult<UserModel> result = new ResponseResult<UserModel>();
            var user = repo_user.Find(g => g.Id == id && g.Deleted == null);
            if (user == null)
            {
                result.Errors.Add("Kullanıcı bulunamadı.");
                return result;
            }
            result.Result = new UserModel()
            {
                Id = user.Id,
                CreatedByName = user.CreatedByName,
                Address = user.Address,
                CreatedBy = user.CreatedBy,
                Email = user.Email,
                LastUpdatedBy = user.LastUpdatedBy,
                LastUpdatedByName = user.LastUpdatedByName,
                Name = user.Name,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                Surname = user.Surname,
                UserRoles = Enum.GetNames(typeof(UserRoles)).ToHashSet(),
            };
            return result;
        }

        public ResponseResult<User> GetUserUserBy(Expression<Func<User, bool>> expression)
        {
            ResponseResult<User> result = new ResponseResult<User>();
            result.Result = repo_user.Find(expression);
            if (result.Result == null)
            {
                result.Errors.Add("Kullanıcı bulunamadı.");
                return result;
            }
            return result;
        }

        public ResponseResult<User> CreateUser(UserModel model)
        {
            ResponseResult<User> result = new ResponseResult<User>();
            try
            {
                result.Result = repo_user.Find(x => x.Email == model.Email && x.Deleted == null);
                if (result.Result != null)
                {
                    result.Errors.Add("Girilen email adresine ait bir kullanıcı bulunmaktadır.");
                    return result;
                }

                int dbResult = repo_user.Create(new User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Role = model.Role,
                    Email = model.Email,
                    Address = model.Address,
                    Created = DateTime.Now,
                    CreatedBy = model.CreatedBy,
                    CreatedByName = model.CreatedByName,
                    LastUpdated = DateTime.Now,
                    LastUpdatedBy = model.LastUpdatedBy,
                    LastUpdatedByName = model.LastUpdatedByName,
                });
                if (dbResult > 0)
                {
                    result.Result = repo_user.Find(g => g.Id == dbResult);
                }
                else
                {
                    result.Errors.Add("Kullanıcı oluşturulurken bir hata ile karşılaşıldı.");
                }
            }
            catch (Exception)
            {
                //Log Exception
                result.Errors.Add("İşlem sırasında bir hata ile karşılaşıldı.");
            }
            return result;
        }

        public ResponseResult<UserModel> UpdateUser(UserModel user, User activeUser)
        {
            ResponseResult<UserModel> res = new ResponseResult<UserModel>();
            try
            {
                user.UserRoles = Enum.GetNames(typeof(UserRoles)).ToHashSet();
                if (activeUser.Role != UserRoles.Admin.ToString() && user.Id != activeUser.Id)
                {
                    res.Errors.Add("Güncelleme yetkisine sahip değilsiniz.");
                    return res;
                }
                var db_user = repo_user.Find(g => g.Email == user.Email && g.Deleted == null);
                if (db_user == null)
                {
                    res.Errors.Add("Kullanıcı bilgisi bulunamadı.");
                    return res;
                }
                if (db_user != null && db_user.Id != user.Id)
                {
                    res.Errors.Add("Girilen email adresine ait bir kullanıcı bulunmaktadır.");
                    return res;
                }

                var updatedUser = repo_user.Find(g => g.Id == user.Id);
                updatedUser.Address = user.Address;
                updatedUser.Name = user.Name;
                updatedUser.Surname = user.Surname;
                updatedUser.PhoneNumber = user.PhoneNumber;
                updatedUser.Role = user.Role;
                updatedUser.Email = user.Email;
                updatedUser.Password = user.Password;

                if (repo_user.Update(updatedUser) == 0)
                    res.Errors.Add("Güncelleme işlemi sırasında hata ile karşılaşıldı.");

                res.Result = user;
            }
            catch (Exception)
            {
                //Log Exception
                res.Errors.Add("İşlem sırasında bir hata ile karşılaşıldı.");
            }
            return res;
        }

        public ResponseResult<User> DeleteUser(int id, User activeUser)
        {
            ResponseResult<User> res = new ResponseResult<User>();
            try
            {
                res.Result = repo_user.Find(g => g.Id == id && g.Deleted == null);
                if (activeUser.Role != UserRoles.Admin.ToString())
                {
                    res.Errors.Add("Silme yetkisine sahip değilsiniz.");
                    return res;
                }
                if (activeUser.Id == id)
                {
                    res.Errors.Add("Kendi kullanıcı kaydınızı silemezsiniz.");
                    return res;
                }
                if (res.Result == null)
                {
                    res.Errors.Add("Kullanıcı bulunamadı.");
                    return res;
                }

                if (repo_user.Delete(res.Result) == 0)
                    res.Errors.Add("Silme işlemi sırasında hata ile karşılaşıldı.");
            }
            catch (Exception)
            {
                //Log Exception
                res.Errors.Add("İşlem sırasında bir hata ile karşılaşıldı.");
            }
            return res;
        }

        public ResponseResult<User> LoginUser(LoginModel model)
        {
            ResponseResult<User> res = new ResponseResult<User>();
            try
            {
                res.Result = repo_user.Find(g => g.Email == model.Email && g.Password == model.Password && g.Deleted == null);
                if (res.Result == null)
                {
                    res.Errors.Add("Girilen email veya parola hatalıdır. Lütfen tekrar deneyiniz.");
                }
            }
            catch (Exception)
            {
                //Log Exception
                res.Errors.Add("İşlem sırasında bir hata ile karşılaşıldı.");
            }
            return res;
        }
    }
}
