using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    public class UsersRepository : ConnectionClass
    {

        public IQueryable<User> getUsers() {
            return Entity.Users;
        }


        public void addUser(User u) {
            Entity.Users.Add(u);
            Entity.SaveChanges();
        }

        public User getUser(string username) {
            return Entity.Users.SingleOrDefault(u => u.Username == username);
        }

        public User GetUserByEmail(string email)
        {
            return Entity.Users.SingleOrDefault(u => u.Email == email);
        }

        public List<User> getUsersWithEmailRequest()
        {
            return Entity.Users.Where(u => u.EmailRequest == true).ToList();
        }


        public bool? GetEmailRequestStatus(string email) {
            User u = GetUserByEmail(email);
            if (u.EmailRequest != null)
            {
                return u.EmailRequest;
            }
            else {
                return false;
            }
        }

        public void setEmailRequestToTrue(string email) {
            GetUserByEmail(email).EmailRequest = true;
            Entity.SaveChanges();
        }
    }
}
