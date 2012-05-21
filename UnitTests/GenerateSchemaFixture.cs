using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using NUnit.Framework;

using dotDash.Domain;

namespace UnitTests
{
    public class GenerateSchemaFixture
    {
        [Test]
        public void Can_generate_schema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Service).Assembly);

            new SchemaExport(cfg).Execute(true, true, false);
        }
    }
}