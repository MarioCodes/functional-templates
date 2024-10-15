using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace testFeatures.Models
{
    public class RequestUserModel
    {
        [Required(ErrorMessage = "Name is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }
        public bool Active { get; set; }
    }

    public class RequestUserListModel
    {
        [JsonPropertyName("list")]
        public List<RequestUserModel> RequestListModel { get; set; }
    }
}
