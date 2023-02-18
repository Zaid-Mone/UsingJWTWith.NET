using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsingJWT.Models
{

    public class Book
    {
        public Guid Id { get; set; }
        public int PublishYear { get; set; }
        public string  AuthorName { get; set; }
    }
}
