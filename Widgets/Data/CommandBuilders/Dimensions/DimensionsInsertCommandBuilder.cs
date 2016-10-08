using Gnosis.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.Data.CommandBuilders.Dimensions
{
    public class DimensionsInsertCommandBuilder
    {
        private IDimensionsInsertCommandBuilder innerBuilder;

        public DimensionsInsertCommandBuilder(): this(new TextDimensionsInsertCommandBuilder())
        {
        }

        public DimensionsInsertCommandBuilder(IDimensionsInsertCommandBuilder innerBuilder)
        {
            this.innerBuilder = innerBuilder;
        }

        public DbCommand Build(DbConnection conn, DbTransaction trans, Guid id, Guid revision, IDimensions dimensions)
        {
            DbCommand cmd = innerBuilder.Build(conn, trans);

            ParameterHelper helper = new ParameterHelper();

            helper.AddParameters(cmd, new Dictionary<string, object>()
            {
                { "@id", id },
                { "@revision", revision },
                { "@length", dimensions.Length },
                { "@width", dimensions.Width },
                { "@height", dimensions.Height }
            });
            
            return cmd;
        }
    }
}
