using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos
{
    public class ProductsRepository
    {
        private BasicContext db = new BasicContext();

        public IEnumerable<Products> GetByProductGroup(Guid groupId)
        {
            return db.products.Where(p => p.GroupId == groupId).ToList();
        }
    }
}
