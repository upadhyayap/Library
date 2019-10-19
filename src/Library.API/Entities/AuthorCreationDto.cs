using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Entities
{
    public class AuthorCreationDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTimeOffset dateOfBirth { get; set; }
        public string genre { get; set; }

        public ICollection<BookDto> books { get; set; }
         = new List<BookDto>();
    }
}
