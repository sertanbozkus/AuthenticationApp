using AuthenticationApp.Data.Context;
using AuthenticationApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthenticationAppContext _context;
        public UserRepository(AuthenticationAppContext context)
        {
            _context = context;
        }

        public void AddUser(UserEntity entity)
        {
            entity.CreatedDate = DateTime.Now; // ctor yerine burada yaptım.
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public UserEntity Get(Expression<Func<UserEntity, bool>> predicate)
        {
            return _context.Users.FirstOrDefault(predicate);
        }

        public IQueryable<UserEntity> GetAll(Expression<Func<UserEntity, bool>> predicate = null)
        {
            return predicate is not null ? _context.Users.Where(predicate) : _context.Users;
        }
    }
}


/*
 

VERİ BULMA METOTLARI

1 -> Find : Id ile eşleşen veriyi bulur.

2 -> First : İlk eşleşen veriyi döner, veri bulamazsa hata verir.

3 -> Single : İlk eşleşen veriyi döner. Başka eşleşen varsa veya hiç yoksa hata verir.

4 -> FirstOrDefault : İlk eşleşen veriyi döner, eşleşen yoksa null döner.

5 -> SingleOrDefault : İlk eşleşen veriyi döner, eşleşen yoksa null döner. Birden fazla varsa hata verir.


*/
