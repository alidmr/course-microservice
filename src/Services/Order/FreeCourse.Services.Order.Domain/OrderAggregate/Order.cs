using System;
using System.Collections.Generic;
using System.Linq;
using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    // EF CORE features 
    //  - Owned Types
    //  - Shadow Property
    //  - Backing Field
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems; // dışarıdan set edilmesin

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;  // sadece okuma yapılsın

        public Order()
        {

        }

        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (existProduct)
                return;

            var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
            _orderItems.Add(newOrderItem);
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}