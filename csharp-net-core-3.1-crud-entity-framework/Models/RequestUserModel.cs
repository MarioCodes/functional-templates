using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace crud.Models
{
    public class RequestUserModel
    {
        [Required(ErrorMessage = "Id is mandatory")]
        public string Id { get; set; }
    }

    public class RequestUserListModel
    {
        [JsonPropertyName("list")]
        public List<RequestUserModel> RequestListModel { get; set; }
    }
}
