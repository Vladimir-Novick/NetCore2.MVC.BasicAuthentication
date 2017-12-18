using System.Runtime.Serialization;

namespace NetCore2.MVC.BasicAuthentication.Controllers
{
    public class Query
    {

        [DataMember]
        public string value { get; set; }
    }
}