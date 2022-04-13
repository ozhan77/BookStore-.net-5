using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;
using FluentValidation;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookValidator : AbstractValidator<>
    {
        public GetBookValidator()
        {
            Rulefor(command => command.BookId).
        }
    }
}