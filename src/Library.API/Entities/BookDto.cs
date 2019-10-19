using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Entities
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
        public Guid AuthorId { get; set; }
    }
}
