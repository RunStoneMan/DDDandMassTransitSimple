using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Message.Events
{
    public class OrderCreated
    {
        public string OrderId { set; get; }

        public string UserId { set; get; }

        public List<String> ProductIds { set; get; }

        public DateTime EventCreatedTime { set; get; }
    }
}
