using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using asp_MVC_letsTry.Models.registrationForms;

namespace asp_MVC_letsTry.Models
{
    public class user
    {
        public int id { get; set; }

        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}
