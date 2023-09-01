using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Api.DTO
{
    public class ErrorDetailsDTO
    {
        [FromQuery(Name = "status_code")]
        public int StatusCode { get; set; }

        [FromQuery(Name = "message")]
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        
    }
}
