using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Sales.Message.Commands
{
    public class PlaceOrder
    {
        public string UserId { set; get; }

        public List<string> ProductIds { set; get; }

        public DateTime createTime { set; get; }
    }
}
