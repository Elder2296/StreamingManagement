using Org.BouncyCastle.Bcpg.OpenPgp;

namespace StreamingManagement.Models.dto.Response
{
    public class Response
    {
        public int code { get; set; }
        public string message { get; set; }
        public Object data { get; set; }

        public Response(int code, string message, Object data) {
            this.code = code;
            this.message = message;
            this.data = data;
        }
    }
}
