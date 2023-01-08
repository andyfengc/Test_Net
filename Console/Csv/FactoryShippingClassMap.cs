using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace Console.Csv
{
    public class FactoryShippingClassMap : ClassMap<FactoryShipping>
    {
        public FactoryShippingClassMap()
        {
            Map(m => m.CustName).Name("CUST NAME");
            Map(m => m.Sku).Name("MFG SKU");
            Map(m => m.Upc).Name("UPC CODE");
        }
    }
}
