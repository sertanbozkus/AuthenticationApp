using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Data.Entities
{
    public abstract class BaseEntity
    {
        // Farklı bi yerde (repo) atama yapmak istediğim için ctor ile createddate atamicam şu an.
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
