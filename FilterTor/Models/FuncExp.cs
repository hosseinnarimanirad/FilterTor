namespace FilterTor.Models;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


public class FuncExp<T1, T2>
{
    public FuncExp(Expression<Func<T1, T2>> expression)
    {
        _expression = expression;

        _func = expression.Compile();
    }

    private Expression<Func<T1, T2>> _expression;

    public Expression<Func<T1, T2>> Expression
    {
        get { return _expression; }

        private set { _expression = value; }
    }


    private Func<T1, T2> _func;

    public Func<T1, T2> Func
    {
        get { return _func; }
        private set { _func = value; }
    }
}
