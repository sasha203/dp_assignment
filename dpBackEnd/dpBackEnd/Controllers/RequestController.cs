using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using DataAccess;

namespace dpBackEnd.Controllers
{
    public class RequestController : ApiController
    {
        RequestRepository rr = new RequestRepository();

        public void addRequest(Request r) {
            rr.addRequest(r);
        }

        public Request GetLatestRequestByEmail(string email)
        {
            return rr.GetLatestRequestByEmail(email);
        }

    }
}
