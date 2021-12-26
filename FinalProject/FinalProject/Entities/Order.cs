using FinalProject.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class UserAddress
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; } = true;

        public UserInfo User { get; set; }
    }

    public class Payment
    {
        public int Id { get; set; }
        public string PaymentName { get; set; }
        public string Description { get; set; }

        public ICollection<PaymentDetail> PaymentDetails { get; set; }
        public Payment()
        {
            PaymentDetails = new HashSet<PaymentDetail>();
        }
    }

    public class PaymentDetail
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Total { get; set; }

        public Order Order { get; set; }
        public Payment Payment { get; set; }
    }

    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? OrderDelivery { get; set; }
        public Guid UserId { get; set; }
        public Guid UserAddressId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double Total { get; set; }

        public ICollection<PaymentDetail> PaymentDetails { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Order()
        {
            PaymentDetails = new HashSet<PaymentDetail>();
            OrderDetails = new HashSet<OrderDetail>();
        }
    }

    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
