using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;

namespace TraincelAPI
{
    public class AutoMapping :Profile
    {
        public AutoMapping()
        {
            CreateMap<CategoriesVM, Categories>().ReverseMap();
            CreateMap<CountriesVM, Countries>().ReverseMap();
            CreateMap<FacultiesVM, Faculties>().ReverseMap();
            CreateMap<PartialFacultiesVM, Faculties>().ReverseMap();
            CreateMap<InvoiceVM, Invoice>().ReverseMap();
            CreateMap<LoginDetailsVM, LoginTable>().ReverseMap();
            CreateMap<OrdersVM, Orders>().ReverseMap();
            CreateMap<PurchaseOptionsVM, PurchaseOptions>().ReverseMap();
            CreateMap<PurchaseOptionTypeVM, PurchaseOptionType>().ReverseMap();
            CreateMap<CartItemVM, CartItems>().ReverseMap();
            CreateMap<UserDetailsVM, UserTable>().ReverseMap();
            CreateMap<WebinarPurchasedOptionsDetailsVM, WebinarPurchasedOptionsDetails>().ReverseMap();
            CreateMap<WebinarTypeVM, WebinarTypes>().ReverseMap();
            CreateMap<WebinarsVM, Webinars>().ReverseMap();
            CreateMap<WebinarTypeVM, WebinarTypes>().ReverseMap();
            CreateMap<RegisterVM, UserTable>().ReverseMap();
            CreateMap<RegisterVM, LoginTable>().ReverseMap();
            CreateMap<CompanyVM, Company>().ReverseMap();
            CreateMap<UserCartVM, UserCartMapping>().ReverseMap();
            CreateMap<OrderItemsVM, OrderItems>().ReverseMap();
        }
    }
}

