//namespace SampleApp.Application.Features;

//using FluentValidation;
//using SampleApp.Core.Common;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Resources;
//using System.Text;
//using System.Threading.Tasks;

//public class ListFundValidator : AbstractValidator<ListFundQuery>
//{
//    public ListFundValidator()
//    {
//        RuleFor(x => x.GridKey).IsInEnum();

//        //RuleFor(command => command.Title).NotNull()
//        //.NotEmpty()
//        //    .WithMessage(Resources.FundBasketNameNotEntered);

//        //RuleFor(command => command.TradeCode).NotNull()
//        //.NotEmpty()
//        //    .WithMessage(Resources.FundBasketTradeCodeNotEntered);
//    }
//}