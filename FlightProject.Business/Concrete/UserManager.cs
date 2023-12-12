using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Business.Abstract;
using FlightProject.Core.Entities;
using FlightProject.Core.Utilities.Results;
using FlightProject.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FlightProject.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IResult> AddAsync(User user)
        {
            var isSuccess = await _userDal.AddAsync(user);
            if (isSuccess)
            {
                await _userDal.SaveAsync();
                return new SuccessResult("Başarılı");
            }
            return new ErrorResult("Kullanıcı Eklenirken Hata Meydana Geldi");
        }

        public async Task<IDataResult<User>> FindByIdAsync(string id)
        {
            var result = await _userDal.GetByIdAsync(id);
            if (result is null)
                return new ErrorDataResult<User>("Kullanıcı Bulunamadı");
            return new SuccessDataResult<User>(result, "Başarılı");
        }

        public async Task<IDataResult<User?>> FindByEmailAsync(string email)
        {
            var result = await _userDal.GetWhere(u => u.Email == email).FirstOrDefaultAsync();
            if (result == null)
            {
                return new ErrorDataResult<User?>(null, "Kullanıcı Bulunamadı!");
            }

            return new SuccessDataResult<User?>(result);
        }
    }
}
