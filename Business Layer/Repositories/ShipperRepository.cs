using AutoMapper;
using Business_Layer.DataAccess;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.ShipperViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    //public class shipperrepository /*: ishipperrepository*/
    //{
    //      private readonly fastfooddeliverydbcontext _context;
    //      private readonly imapper _mapper;
    //      private usermanager<user> _usermanager;
    //      private readonly rolemanager<identityrole> _rolemanager;

    //      public shipperrepository(fastfooddeliverydbcontext context, imapper mapper, usermanager<user> usermanager, rolemanager<identityrole> rolemanager)
    //      {
    //          _context = context;
    //          _mapper = mapper;
    //          _usermanager = usermanager;
    //          _rolemanager = rolemanager;
    //      } 

    //    public async task<list<shippervm>> getallshipper()
    //      {
    //          var shippers = await _usermanager.getusersinroleasync("shipper");
    //          var shipperlist = new list<shippervm>();
    //          foreach (var shipper in shippers)
    //          {
    //              var shippervm = new shippervm();
    //              var orders = _context.orders.where(x => x.shipperid.equals(shipper.id)).tolist();
    //              shippervm.userid = shipper.id;
    //              if (orders != null)
    //              {
    //                  foreach (var order in orders)
    //                  {
    //                      shippervm.orderstatusid.add(order.orderid);
    //                  }
    //              }
    //              else shippervm.orderstatusid = null;
    //              shipperlist.add(shippervm);
    //          }
    //          var result = _mapper.map<list<shippervm>>(shipperlist);
    //          return result;
    //      }

    //      public async task<apiresponsemodel> getorderstatusbyshipperid(string userid)
    //      {
    //          var shippers = await _usermanager.getusersinroleasync("shipper");
    //          var user = await _usermanager.findbyidasync(userid);
    //          if (!shippers.contains(user))
    //          {
    //              return new apiresponsemodel()
    //              {
    //                  code = 200,
    //                  message = "this user is not shipper",
    //                  issuccess = false,
    //              };
    //          }
    //          var orderstatuses = _context.orderstatuses.where(o => o.shipperid.equals(userid)).tolist();
    //          if (orderstatuses.count() == 0) return new apiresponsemodel()
    //          {
    //              code = 200,
    //              message = "shipper doesn't have any order",
    //              issuccess = false,
    //          };
    //          var result = _mapper.map<list<orderstatus>>(orderstatuses);
    //          return new apiresponsemodel()
    //          {
    //              code = 200,
    //              message = "get successful",
    //              issuccess = true,
    //              data = result
    //          };
    //      }
    //    public async Task<list<shipperreport>>? gettopfiveshippersasync()
    //      {
    //          var shippers = await _usermanager.getusersinroleasync("shipper");
    //          var shipperhasmostorder = shippers.select(s => new shipperreport
    //          {
    //              shippername = s.fullname,
    //              totalreceivedorders = s.orderstatuses.count(),
    //              totalshippedorders = s.orderstatuses.count(os => os.order.statusorder == "confirmed")
    //          })
    //              .orderbydescending(x => x.totalshippedorders)
    //              .take(5)
    //              .tolist();
    //          return shipperhasmostorder;
    //      }
        
    //}
}
