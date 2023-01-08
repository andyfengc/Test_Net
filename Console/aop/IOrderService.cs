using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Console.aop.autofac;

namespace Console.aop
{
    [Intercept(typeof(LogInterceptor))]
    public interface IOrderService
    {
        void GrabNewOrders(int days);
        void ShipOrder(Order order);
    }

    public class Order
    {
        public string orderId { get; set; }
        public DateTime createdTime { get; set; }
        public double totalAmount { get; set; }
    }
}
