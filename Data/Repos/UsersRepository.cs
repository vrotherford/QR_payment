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

        public string checkUser(string email, string pass)
        {
            var client = db.Clients.FirstOrDefault(x => x.Email == email && x.Pass == pass);
            if(client != null)
            {
                return client.Id.ToString();
            }
            return null;
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
