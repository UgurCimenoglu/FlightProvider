using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Business.Abstract;
using FlightProject.Core.Entities;
using FlightProject.Core.Utilities.Results;
using FlightProject.Core.Utilities.Security.Hashing;
using FlightProject.Core.Utilities.Security.JWT;
using FlightProject.Entities.Dtos;


namespace FlightProject.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IResult> RegisterAsync(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var result = await _userService.AddAsync(user);
            if (!result.Success)
                return new ErrorResult("Kullanici Kayit Edilirken Hata Meydana Geldi.");
            return new SuccessResult("Kullanici Kayit Edildi.");

        }

        public async Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.FindByEmailAsync(userForLoginDto.Email);

            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>("Kullanici Bulunamadi.");
            }

            if (!HashingHelper.VerifyPaswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>("Hatalı Parola");
            }
            return new SuccessDataResult<User>(userToCheck.Data, "Giriş Yapıldı.");
        }

        public async Task<IResult> UserExists(string email)
        {
            var user = await _userService.FindByEmailAsync(email);
            if (user.Data == null)
            {
                return new ErrorResult("Kullanici sistemde kayıtlı!");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu.");
        }
    }
}
