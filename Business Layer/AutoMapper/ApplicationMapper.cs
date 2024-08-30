using AutoMapper;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.CartVMs;
using Data_Layer.ResourceModel.ViewModel.Category;
using Data_Layer.ResourceModel.ViewModel.FeedBackVMs;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
using Data_Layer.ResourceModel.ViewModel.OrderDetailVMs;
using Data_Layer.ResourceModel.ViewModel.OrderStatusVMs;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using Data_Layer.ResourceModel.ViewModel.User;

namespace Business_Layer.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<MenuFoodItem, MenuFoodItemVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Category, CategoryCreateVM>().ReverseMap();
            CreateMap<Category, CategoryUpdateVM>().ReverseMap();

            CreateMap<Order, OrderVM>().ReverseMap();

            CreateMap<Order, OrderViewVM>().ReverseMap();
            CreateMap<Order, OrderCreateVM>().ReverseMap();
            CreateMap<Order, OrderUpdateVM>().ReverseMap();
            CreateMap<Order, OrderPaymentVM>().ReverseMap();
            CreateMap<Order, OrderUpdateForShipperVM>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailViewVM>().ReverseMap();
            CreateMap<OrderDetail, OrderDetaiCreateVM>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailUpdateVM>().ReverseMap();

            CreateMap<MenuFoodItem, MenuFoodItemViewVM>().ReverseMap();
            CreateMap<MenuFoodItem, MenuFoodItemCreateVM>().ReverseMap();
            CreateMap<MenuFoodItem, MenuFoodItemUpdateVM>().ReverseMap();

            CreateMap<Cart, CartCreateVM>().ReverseMap();
            CreateMap<Cart, CartUpdateVM>().ReverseMap();
            CreateMap<Cart, CartViewVM>().ReverseMap();

            CreateMap<FeedBack, FeedBackCreateVM>().ReverseMap();
            CreateMap<FeedBack, FeedBackUpdateVM>().ReverseMap();
            CreateMap<FeedBack, FeedBackViewVM>().ReverseMap();

            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<OrderStatus, OrderStatusVM>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusViewVM>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusUpdateVM>().ReverseMap();

        }
    }
}
