using System.Collections.Generic;

namespace testFeatures.Models
{
    public class ResponseUserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }

    public class ResponseListUserModel
    {
        public List<ResponseUserModel> Result { get; set; }
    }
}
