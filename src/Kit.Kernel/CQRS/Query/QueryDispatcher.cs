﻿using System;

namespace Kit.Kernel.CQRS.Query
{
    public class QueryDispatcher : IQueryDispatcher
    {
        public TResult Dispatch<TParameter, TResult>(TParameter query) where TParameter : IQuery where TResult : IQueryResult
        {
            throw new NotImplementedException();
        }
    }
}