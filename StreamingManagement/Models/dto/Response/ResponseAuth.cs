namespace StreamingManagement.Models.dto.Response
{
    public class ResponseAuth
    {
        public string usuario { get; set; }
        public string token { get; set; }
        public DateTime fechaExpiracion { get; set; }

    }
}
