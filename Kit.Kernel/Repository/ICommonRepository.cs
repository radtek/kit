﻿using System.Collections.Generic;

namespace Kit.Kernel.Repository
{
    public interface ICommonRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
    }
}