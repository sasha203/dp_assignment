using Common;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace dpBackEnd.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        // GET: Users
        [HttpGet, Route("api/Users")]
        public List<User> GetUsers()
        {
            try
            {
                UsersRepository ur = new UsersRepository();
                List<User> users = ur.getUsers().ToList();
                return users;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpPost, Route("api/Users/Register")]
        public IHttpActionResult AddUser([FromBody] User u)
        {

            try
            {
                UsersRepository ur = new UsersRepository();

                if (u == null)
                {
                    return InternalServerError(new Exception("Invalid Input."));
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        ur.addUser(u);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

            }
            catch
            {
                return InternalServerError(new Exception("An error occurred in the registration process."));
            }
        }

        
        [HttpGet, Authorize, Route("api/Users/GetUserClaims")] 
        public User GetUserClaims() {

            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            User user = new User()
            {
                Username = identityClaims.FindFirst("Username").Value,
                Email = identityClaims.FindFirst("Email").Value,
                Name = identityClaims.FindFirst("Name").Value,
                Surname = identityClaims.FindFirst("Surname").Value
                //LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            };

            return user;
        }


        [HttpGet, Route("api/Users/requestedEmail")]
        public List<User> GetUsersWithEmailRequest()
        {
            try
            {
                UsersRepository ur = new UsersRepository();
                List<User> users = ur.getUsersWithEmailRequest();
                return users;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }

}
