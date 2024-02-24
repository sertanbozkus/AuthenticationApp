using AuthenticationApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Data.Repositories
{
    public interface IUserRepository
    {
        void AddUser(UserEntity entity);

        IQueryable<UserEntity> GetAll(Expression<Func<UserEntity, bool>> predicate = null);

        UserEntity Get(Expression<Func<UserEntity, bool>> predicate);
    }
}
