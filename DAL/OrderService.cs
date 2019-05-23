using DAL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using EFEntity;

namespace DAL
{
    public class OrderService : IOrderService
    {

        OrderDTO[] IOrderService.GetById(long Id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                List<OrderDTO> orders = new List<OrderDTO>();

                BaseService<Order> bs = new BaseService<Order>(ctx);
                var order = bs.GetAll().Where(p => p.Id == Id).ToList().Select(p => ToDTO(p));
                orders.AddRange(order);
                return orders.ToArray();


            }
        }

        OrderDTO[] IOrderService.GetByIds(long[] Ids)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                List<OrderDTO> orders = new List<OrderDTO>();
                BaseService<Order> bs = new BaseService<Order>(ctx);
                foreach (var Id in Ids)
                {

                    var order = bs.GetAll().Where(p => p.Id == Id).ToList().Select(p => ToDTO(p));
                    orders.AddRange(order);
                }
                return orders.ToArray();


            }
        }

        public long CreateOrder(long tid)
        {
            Random r = new Random();
            var ordernum = r.Next(10000000, 999999999);
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Line> lbs = new BaseService<Line>(ctx);
                var line = lbs.GetById(tid);
                Order o = new Order();
                o.OrderNum = ordernum.ToString();
                o.Money = line.NowPrice;
                o.tid = tid;
                o.StartDate = DateTime.Now;
                o.Name = line.title;
                o.state = 0;
                ctx.Order.Add(o);
                ctx.SaveChanges();
                return o.Id;
            }
        }


        private OrderDTO ToDTO(Order r)
        {
            OrderDTO dto = new OrderDTO();
            dto.Name = r.Name;
            dto.CreateDateTime = r.CreateDateTime;
            dto.OrderNum = r.OrderNum;
            dto.StartDate = r.StartDate;
            dto.Money = r.Money;
            dto.state = r.state;
            dto.Id = r.Id;
            dto.tid = r.tid;
            return dto;
        }

        public long[] GetByTids(long[] oid)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Order> bs = new BaseService<Order>(ctx);
                List<long> list = new List<long>();
                foreach (var item in oid)
                {
                    long t = bs.GetAll().Where(e => e.Id == item).ToList().Select(e => e.tid).SingleOrDefault();
                    list.Add(t);
                }

                return list.ToArray();
            }
        }

        public bool CompleteOrder(long oid)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<Order> bs = new BaseService<Order>(ctx);
                var order=bs.GetById(oid);
                if (order==null)
                {
                    return false;
                }
                order.state = 1;
                ctx.SaveChanges();
                return true;
            }
        }

        public OrderDTO[] GetAll()
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<Order> bs = new BaseService<Order>(ctx);
                return  bs.GetAll().ToList().Select(e=>ToDTO(e)).ToArray();
            }
        }

        public void MarkDeleted(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Order> bs = new BaseService<Order>(ctx);
                bs.MarkDeleted(id);
            }
        }

    }
}
