using System;
using System.Data.Common;

namespace Widgets.Data.CommandBuilders.Specifications
{
    public interface ISpecificationsInsertCommandBuilder
    {
        DbCommand Build(DbConnection conn, DbTransaction trans, Guid id, Guid revision, ISpecifications data);
    }
}
