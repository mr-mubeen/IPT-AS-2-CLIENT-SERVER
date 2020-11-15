using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace WebApplication2
{
    /// <summary>
    /// Summary description for student
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class student : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string Calculate()
        {
            string data = HttpContext.Current.Request["request"];
            Subject[] Subjects = JsonConvert.DeserializeObject<Subject[]>(data);

            Subject MinSubjects = Subjects.First(x => x.obtmarks == Subjects.Min(y => y.obtmarks));
            Subject MaxSubjects = Subjects.First(x => x.obtmarks == Subjects.Max(y => y.obtmarks));

            decimal noOfSubjects = Subjects.Count();
            decimal totalMarks = noOfSubjects * 100;
            decimal totalmarks = Subjects.Sum(x => x.obtmarks);
            decimal percent = (totalmarks / totalMarks) * 100;

            Result result = new Result()
            {
                MinSubject = MinSubjects,
                MaxSubject = MaxSubjects,
                Percentage = percent
            };

            return JsonConvert.SerializeObject(result);
        }

        public class Subject
        {
            public string name { get; set; }
            public int obtmarks { get; set; }

        }

        public class Result
        {
            public Subject MinSubject { get; set; }
            public Subject MaxSubject { get; set; }
            public decimal Percentage { get; set; }
        }
    }



}

