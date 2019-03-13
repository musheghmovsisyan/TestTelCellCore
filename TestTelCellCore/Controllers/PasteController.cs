using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TestTelCellCore.Models;
using TestTelCellCore.Properties;

namespace TestTelCellCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasteController : ControllerBase
    {
        string url = Settings.Default.ExternalServiceUrl;

        private ModelContext db;
        public PasteController(ModelContext context)
        {
            db = context;
        }



        // GET api/values
        [HttpGet]
        public string Get()
        {
            string data = "";
            CommonFunction.WriteLog("Get", data);
            return "Paste ID not selected";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            //string PastId = "5ZWgEpDE";

            string PastId = id;

            Paste paste = new Paste();
            AccessDatesLog al = new AccessDatesLog();

            string PastLastAccess = DateTime.Now.ToString();
            string data = string.Empty;


            if (db.Paste.Any(p => p.PasteStrId == PastId))
            {
                paste = db.Paste.Where(p => p.PasteStrId == PastId).FirstOrDefault();
                if (paste != null)
                {
                    data = paste.PasteText;
                    al.PastId = paste.Id;
                    al.PastStrId = paste.PasteStrId;
                    al.ActionName = "View";

                    db.AccessDatesLog.Add(al);


                    AccessDatesLog alLast = db.AccessDatesLog.Where(p => p.PastId == paste.Id).OrderByDescending(p => p.AccessDate).FirstOrDefault();
                    if (alLast != null)
                    {
                        PastLastAccess = alLast.AccessDate.ToString();
                    }
                }

            }
            else
            {
                paste = new Paste();


                var client = new RestClient(url + PastId);
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                data = response.Content;
                paste.PasteText = data;
                paste.PasteStrId = PastId;

                db.Paste.Add(paste);
                db.SaveChanges();

                al.PastId = paste.Id;
                al.PastStrId = paste.PasteStrId;
                al.ActionName = "Insert";



                db.AccessDatesLog.Add(al);

            }


            db.SaveChanges();








            Response.Headers.Add("Past-Last-Access", PastLastAccess);

            CommonFunction.WriteLog("GetById", data);


            return data;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public string Delete(string id)
        {


            string PastId = id;

            Paste paste = new Paste();
            AccessDatesLog al = new AccessDatesLog();


            string data = string.Empty;


            if (db.Paste.Any(p => p.PasteStrId == PastId))
            {
                paste = db.Paste.Where(p => p.PasteStrId == PastId).FirstOrDefault();
                if (paste != null)
                {
                    data = paste.PasteText;
                    al.PastId = paste.Id;
                    al.PastStrId = paste.PasteStrId;
                    al.ActionName = "Delete";

                    db.AccessDatesLog.Add(al);

                    db.Remove(paste);
                    db.SaveChanges();
                }



                CommonFunction.WriteLog("Delete", data);


            }
            return data;
        }
    }
}
