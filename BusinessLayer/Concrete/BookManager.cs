using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookDal _bookDal;

        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
        }

        public void TDelete(int id)
        {
            _bookDal.Delete(id);
        }

        public List<Book> TGetAll()
        {
            return _bookDal.GetAll();
        }

        public Book TGetByID(int id)
        {
            return _bookDal.GetByID(id);
        }

        public void TInsert(Book entity)
        {
            _bookDal.Insert(entity);
        }

        public void TUpdate(Book entity)
        {
            _bookDal.Update(entity);
        }
    }
}
