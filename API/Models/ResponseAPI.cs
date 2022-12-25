using System.Net;
namespace API.Models
{
    public class ResponseAPI
    {
        public ResponseAPI()
        {
            var ErrorMessage = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool isSucces { get; set; } = true;
        public List<string> ErrorMessage { get; set; }
        public object Resut { get; set; }
    }
}
