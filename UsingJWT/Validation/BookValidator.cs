using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsingJWT.Models;

namespace UsingJWT.Validation
{
    public class BookValidator:AbstractValidator<Book>
    {
        public BookValidator()
        {
        RuleFor(book => book.AuthorName).NotEmpty()
                .WithMessage("Please Set an Author For the Book");
        RuleFor(book => book.PublishYear).NotEmpty()
                .WithMessage("Please Set an Year For the Book");
        }
    }
}
