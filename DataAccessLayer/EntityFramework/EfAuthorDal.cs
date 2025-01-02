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
    public class EfAuthorDal : GenericRepository<Author>, IAuthorDal
    {
        private readonly Project5KediKitabeviContext _context;

        public EfAuthorDal(Project5KediKitabeviContext context) : base(context)
        {
            _context = context;
        }

        public int GetAuthorCount()
        {
            return _context.Authors.Count();
        }
    }
}
