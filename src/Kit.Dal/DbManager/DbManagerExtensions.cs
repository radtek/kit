﻿using System;
using System.Data;
using Castle.Core.Internal;

// ReSharper disable ConvertClosureToMethodGroup
namespace Kit.Dal.DbManager
{
    public static class DbManagerExtensions
    {
        public static PocoReader<T> ExecutePocoReader<T>(this IDbManager dbManager, string commandText, Func<IDataRecord, T> convertFunc, params IDbDataParameter[] parameters)
        {
            parameters.ForEach(p => dbManager.AddParameter(p));
            IDataReader reader = dbManager.ExecuteReader(CommandType.Text, commandText);
            return new PocoReader<T>(reader, convertFunc, false);
        }
        
        public static object RunFunc(this IDbManager dbManager, string name, params IDbDataParameter[] parameters)
        {
            const string resultName = "result";

            IDbDataParameter pResult = dbManager.AddParameter(resultName, null, ParameterDirection.ReturnValue, short.MaxValue);
            RunProc(dbManager, name, parameters);
            
            return pResult.Value;
        }

        public static T RunFunc<T>(this IDbManager dbManager, string name, params IDbDataParameter[] parameters) where T : struct
        {
            T result = default(T);

            object value = RunFunc(dbManager, name, parameters);
            if (value != null)
                result = (T)Convert.ChangeType(value, typeof(T));

            return result;
        }

        public static void RunProc(this IDbManager dbManager, string name, params IDbDataParameter[] parameters)
        {
            parameters.ForEach(p => dbManager.AddParameter(p));
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, name);
        }
    }
}