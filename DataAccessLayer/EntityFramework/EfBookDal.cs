using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfBookDal : GenericRepository<Book>, IBookDal
    {
        public EfBookDal(Project5KediKitabeviContext context) : base(context)
        {
        }
    }
}
