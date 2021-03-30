using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;
using TraincelAPI.Repository.Interface;
using TraincelAPI.Services.Interface;

namespace TraincelAPI.Services
{
    public class PurchaseOptionsService: IPurchasedOptionsService
    {
        private readonly IPurchaseOptionsRepo _purchaseOptionsRepo;
        private readonly IPurchasedOptionTypeRepo _purchasedOptionTypeRepo;
        private readonly IMapper _mapper;
        public PurchaseOptionsService(IPurchaseOptionsRepo purchaseOptionsRepo, IPurchasedOptionTypeRepo purchasedOptionTypeRepo, IMapper mapper)
        {
            _purchaseOptionsRepo = purchaseOptionsRepo;
            _purchasedOptionTypeRepo = purchasedOptionTypeRepo;
            _mapper = mapper;
        }

        public bool AddPurchaseOptions(PurchaseOptionsVM purchaseOptionVM)
        {
            var purchaseOption = _mapper.Map<PurchaseOptions>(purchaseOptionVM);
            var response = _purchaseOptionsRepo.AddPurchaseOption(purchaseOption);
            return (response.Result);
        }

        public bool DeletePurchaseOptions(int id)
        {
            var response = _purchaseOptionsRepo.DeletePurchaseOption(id);
            return response.Result;
        }

        public List<PurchaseOptionsVM> GetPurchaseOptions()
        {
            var categories = _purchaseOptionsRepo.GetPurchaseOptions();
            return _mapper.Map<List<PurchaseOptionsVM>>(categories.Result);
        }

        #region PurchasedOptionType
        public bool AddPurchaseOptionType(PurchaseOptionTypeVM purchaseOptionTypeVM)
        {
            var purchaseOptionType = _mapper.Map<PurchaseOptionType>(purchaseOptionTypeVM);
            var response = _purchasedOptionTypeRepo.AddPurchasedOptionType(purchaseOptionType);
            return (response.Result);
        }

        public bool DeletePurchaseOptionType(int id)
        {
            var response = _purchasedOptionTypeRepo.DeletePurchasedOptionType(id);
            return response.Result;
        }
        #endregion
    }
}
