using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.Repositories
{
    public interface IBookStoreRepository<Tentity>
    {
        IList<Tentity> list();
        Tentity find(int ID);
        void Add(Tentity entity);
        void update(int ID, Tentity entity);
        void delete(int ID);   
        List<Tentity> Search(string term);
 
    }
}
