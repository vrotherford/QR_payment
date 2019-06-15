using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos
{
    public class ProductGroupsRepository
    {
        private BasicContext db = new BasicContext();

        public ProductGroups GetById(Guid Id)
        {
            return db.productGroups.Include("Products").FirstOrDefault(p => p.Id == Id);
        }
    }
}
