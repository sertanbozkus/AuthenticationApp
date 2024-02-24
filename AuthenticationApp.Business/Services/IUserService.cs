using AuthenticationApp.Business.Dtos;
using AuthenticationApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Business.Services
{
    public interface IUserService
    {

        ServiceMessage AddUser(SignUpDto signUpDto);

        UserInfoDto SignInUser(SignInDto signInDto);


    }
}
