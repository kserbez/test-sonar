using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Api.DTO
{
    public class PaginationDTO
    {
        [FromQuery(Name = "page_number")]
        [Range(1, Int32.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "page_size")]
        [Range(1, Int32.MaxValue)]
        public int PageSize { get; set; } = 10;
    }
}
