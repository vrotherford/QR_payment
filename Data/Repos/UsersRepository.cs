using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos
{
    public class UsersRepository
    {
        private BasicContext db;

        public UsersRepository()
        {
            this.db = new BasicContext();
        }

        public Guid checkUser(string email, string pass)
        {
            return db.Clients.FirstOrDefault(x => x.Email == email && x.Pass == pass).Id;
        }

        public Guid addUser(Clients user)
        {
            db.Clients.Add(user);
            db.SaveChanges();
            return new Guid();
        }

        public Clients GetUser(Guid id)
        {
            return db.Clients.FirstOrDefault(x => x.Id == id);
        }
    }
}
