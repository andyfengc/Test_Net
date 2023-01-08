using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Console.aop.autofac;

namespace Console.aop
{
    public class OrderService : IOrderService
    {
        public virtual void GrabNewOrders(int days)
        {
            System.Console.WriteLine("Grabbed 100 new orders within " + days + " days");
            System.Console.WriteLine("Saved 100 new orders within " + days + " days");
        }

        public virtual void ShipOrder(Order order)
        {
            System.Console.WriteLine(string.Format("ship order id: {0}, amount: {1}", order.orderId, order.totalAmount));
        }
    }
}
