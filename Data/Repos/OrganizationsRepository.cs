using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos
{
    public class OrganizationsRepository
    {
        private BasicContext db = new BasicContext();

        public Organizations GetById(Guid id)
        {
            return db.Organizations.FirstOrDefault(o => o.Id == id);
        }
        public IEnumerable<Organizations> GetByClient(Guid client_id)
        {
            return db.Organizations.Where(o => o.UserId == client_id).ToList();
        }

        public void AddOrganization(Organizations organization)
        {
            db.Organizations.Add(organization);
            db.SaveChanges();
        }

        public void UpdateOrganization()
        {
            db.SaveChanges();
        }

        public void UpdateOrganizationById(Guid id)
        {
            var organization = db.Organizations.First(o => o.Id == id);
            db.SaveChanges();
        }

        public void DeleteById(Guid id)
        {
            var organization = db.Organizations.First(o => o.Id == id); 
            db.Organizations.Remove(organization);                          
            db.SaveChanges();
        }

    }
}
