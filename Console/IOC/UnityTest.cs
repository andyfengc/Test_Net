using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Console.IOC
{
    public class UnityTest
    {
        public void Test()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<Service>(); 
            container.RegisterType<Dao, SqlServerDao>();
            // default resolve to get sqlserver dao
            var service = container.Resolve<Service>();
            service.Add();
            // explicit set resolve to oracle dao
            service = container.Resolve<Service>(new ParameterOverride("dao", new OracleDao()));
            service.Add();
        }
    }

    public class Service 
    {
        private Dao dao;

        public Service(Dao dao)
        {
            this.dao = dao;
        }
        public void Add()
        {
            dao.Add();
        }
    }

    public interface Dao
    {
        void Add();
    }
    public class SqlServerDao:Dao
    {
        public void Add()
        {
            System.Console.WriteLine("insert data into sql server");
        }
    }

    public class OracleDao:Dao
    {
        public void Add()
        {
            System.Console.WriteLine("insert data into oracle");
        }
    }
}
