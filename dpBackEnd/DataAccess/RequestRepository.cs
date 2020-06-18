using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;


namespace DataAccess
{
    public class RequestRepository: ConnectionClass
    {
        public void addRequest(Request r) {
            Entity.Requests.Add(r);
            Entity.SaveChanges();
        }

        public List<Request> GetRequestsByEmail(string email)
        {
             return Entity.Requests.Where(r => r.Email == email).ToList();
        }


        public Request GetLatestUserRequest(List<Request> requests)
        {
            return requests.OrderByDescending(r => r.TimeStamp).FirstOrDefault();
        }


        //will get all requests by a specific user and filters it to the latest.
        public Request GetLatestRequestByEmail(string email) {
            List<Request> allRequests =  GetRequestsByEmail(email);
            return GetLatestUserRequest(allRequests);
        }
    }
}
