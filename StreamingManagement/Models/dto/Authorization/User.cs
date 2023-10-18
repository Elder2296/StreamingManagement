using System.ComponentModel.DataAnnotations;

namespace StreamingManagement.Models.dto.Authorization
{
    public class User
    {
        [Required(ErrorMessage ="Campo obligagtorio")]
        public String username { get; set; }

        [Required(ErrorMessage = "Campo obligagtorio")]
        public String password { get; set; }    
    }
}
