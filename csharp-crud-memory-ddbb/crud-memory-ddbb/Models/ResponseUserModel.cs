using System.Collections.Generic;

namespace crud.Models
{
    public class ResponseUserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }

    public class ResponseListUserModel
    {
        public List<ResponseUserModel> Result { get; set; }
    }
}
