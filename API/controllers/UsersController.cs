using API.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API
{
    public class UsersController: ApiController
    {

        public static List<User> users =  new List<User>() 
        { 
           new User{ Id= 0, Name="user0", Password="1234" },
           new User{ Id= 1, Name="user1", Password="1234" },
           new User{ Id= 2, Name="user2", Password="1234" },
           new User{ Id= 3, Name="user3", Password="1234" }
        };
        // GET api/users
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET api/users/5
        public User Get(int id)
        {
            return users.Where(c => c.Id == id).FirstOrDefault(); 
            // another function
            // return (from inc in users where inc.Id == id select inc).FirstOrDefault();
        }

        // POST api/users 
        public IEnumerable<User> Post([FromBody] User value)
        {
            User val = new User { Id = users.Max(c => c.Id) + 1, Name = value.Name, Password = value.Password };
            users.Add( val ); 
            return users;
        }

        // PUT api/users/5
        public IEnumerable<User> Put(int id, [FromBody]User value)
        {
            User val = users.Where(c => c.Id == id).FirstOrDefault();
            val.Name = value.Name;
            val.Password = value.Password;
            return users;
        }

        // DELETE api/users/5
        public IEnumerable<User> Delete(int id)
        {
            User val = users.Where(c => c.Id == id).FirstOrDefault();
            users.Remove(val);   
            return users;
        }
    }
}