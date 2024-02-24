using AuthenticationApp.Business.Dtos;
using AuthenticationApp.Business.Services;
using AuthenticationApp.Business.Types;
using AuthenticationApp.Data.Entities;
using AuthenticationApp.Data.Repositories;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IDataProtector _dataProtector;
        public UserManager(IUserRepository userRepo, IDataProtectionProvider dataProtectionProvider)
        {
            _userRepo = userRepo;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }
        // Şifreleme işlemi yapacağım için IDataProtector nesnesine ihtiyacım var. DataProtection.Abstractions kütüphanesini nugetten yükledikten sonra DI ile manager'a ekliyorum.
        public ServiceMessage AddUser(SignUpDto signUpDto)
        {
            var hasMail = _userRepo.GetAll(x => x.Email.ToLower() == signUpDto.Email.ToLower()).ToList();

            // hasMail.count != 0
            if (hasMail.Any())
            {
                return new ServiceMessage()
                {
                    IsSucceed = false,
                    Message = "The Email Adress Has Already Exists."
                };


            }

            var entity = new UserEntity()
            {
                Email = signUpDto.Email,
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,
                Password = _dataProtector.Protect(signUpDto.Password),
            };

            _userRepo.AddUser(entity);

            return new ServiceMessage()
            {
                IsSucceed = true,
                Message = "The Sign Up Process Has Been Completed."
            };



        }

        public UserInfoDto SignInUser(SignInDto signInDto)
        {
            var userEntity = _userRepo.Get(x => x.Email.ToLower() == signInDto.Email.ToLower());

            if (userEntity is null) // == null ile aynı
            {
                return null;
                // Eğer form üzerinden gönderilen email adresi ile eşleşen bir veri tabloda yoksa oturum açılmayacağın için geriye bilgi dönemiyorum. Null dönüyorum.
            }

            var rawPassword = _dataProtector.Unprotect(userEntity.Password); // Şifreyi açtım.

            if(rawPassword == signInDto.Password)
            { 
                return new UserInfoDto()
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName

                };
            }
            else
            {
                return null; // Şifreler eşlemediyse geriye bilgi dönme.
            }
        }
    }
}

// Sertanbozkus@gmail.com

// sertanbozkus@gmail.com
