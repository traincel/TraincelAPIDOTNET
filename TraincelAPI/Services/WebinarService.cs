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
    public class WebinarService : IWebinarService
    {
        private readonly IWebinarRepo _webinarRepo;
        private readonly IWebinarTypeRepo _webinarTypeRepo;
        private readonly IWebinarPurchasedOptionsDetailsRepo _webinarPurchasedOptionsDetailsRepo;
        private readonly IMapper _mapper;
        public WebinarService(IWebinarRepo webinarRepo, IWebinarTypeRepo webinarTypeRepo, IMapper mapper, IWebinarPurchasedOptionsDetailsRepo webinarPurchasedOptionsDetailsRepo)
        {
            _webinarRepo = webinarRepo;
            _webinarTypeRepo = webinarTypeRepo;
            _webinarPurchasedOptionsDetailsRepo = webinarPurchasedOptionsDetailsRepo;
            _mapper = mapper;
        }

        #region Webinar
        public bool AddWebinar(WebinarsVM webinarVM)
        {
            try
            {
               
                var webinar = _mapper.Map<Webinars>(webinarVM);
                webinar.Id = Guid.NewGuid();
                var response = _webinarRepo.AddWebinar(webinar);
                return (response.Result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteWebinar(String id)
        {
            try
            {
                var response = _webinarRepo.DeleteWebinar(new Guid (id));
                return response.Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public WebinarsVM GetWebinar(String userId, Guid webinarId)
        {
            try
            {
                var webinar = _webinarRepo.GetWebinar(userId, webinarId);
                return _mapper.Map<WebinarsVM>(webinar.Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WebinarsVM> GetWebinars(int webinarTypeId, int categoryId, String userId)
        {
            try
            {
                var webinars = new List<Webinars>();
                if (webinarTypeId != 0 && categoryId != 0)
                {
                    webinars = _webinarRepo.GetFilteredWebinars(webinarTypeId, categoryId, userId).Result;
                }
                else if (webinarTypeId == 0 && categoryId != 0)
                {
                    webinars = _webinarRepo.GetCategoryWebinars(categoryId, userId).Result;
                }
                else if (webinarTypeId != 0 && categoryId == 0)
                {
                    webinars = _webinarRepo.GetWebinarTypeWebinars(webinarTypeId, userId).Result;
                }
                else
                {
                    webinars = _webinarRepo.GetWebinars(userId).Result;
                }

                return _mapper.Map<List<WebinarsVM>>(webinars);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<WebinarsVM> GetRecentlyUpdatedWebinars()
        {
            try
            {
                var webinars = _webinarRepo.GetRecentlyUpdatedWebinars().Result;
                return _mapper.Map<List<WebinarsVM>>(webinars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WebinarsVM> GetFeaturedWebinars(String userId)
        {
            try
            {
                var featuredWebinars = _webinarRepo.GetFeaturedWebinars(userId).Result;
                return _mapper.Map<List<WebinarsVM>>(featuredWebinars);
            }
            catch (Exception)
            {
                throw new Exception("Error");
            }
        }

        public bool UpdateWebinar(WebinarsVM webinarsVM)
        {
            try
            {
                var webinar = _mapper.Map<Webinars>(webinarsVM);
                return _webinarRepo.UpdateWebinar(webinar).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WebinarsVM> SearchWebinars(string searchTerm, int webinarTypeId, int categoryId, String userId)
        {
            try
            {
                var webinars = GetWebinars(webinarTypeId, categoryId, userId);
                var searchWebinaars = webinars.Where(webinar => webinar.WebinarName.ToLower().Contains(searchTerm.ToLower())).ToList();
                return searchWebinaars;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WebinarsVM> GetHomePageWebinars(String userId)
        {
            try
            {
                var webinars = _webinarRepo.GetHomePageWebinars(userId).Result;
                return _mapper.Map<List<WebinarsVM>>(webinars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region WebinarType
        public bool AddWebinarType(WebinarTypeVM webinarTypeVM)
        {
            var webinarType = _mapper.Map<WebinarTypes>(webinarTypeVM);
            return _webinarTypeRepo.AddWebinarType(webinarType).Result;

        }

        public bool DeleteWebinarType(int id)
        {
            throw new NotImplementedException();
        }

        public List<WebinarTypeVM> GetWebinarType()
        {
            var webinarType = _webinarTypeRepo.GetWebinarTypes().Result;
            return _mapper.Map<List<WebinarTypeVM>>(webinarType);
        }

        #endregion


        #region WebinarPrice
        public bool UpdateWebinarPrice(List<WebinarPurchasedOptionsDetailsVM> webinarPurchasedOptionsDetailsVM)
        {
            try
            {
                int count = 0;
                foreach (var webinarPurchasedOptionsDetailVM in webinarPurchasedOptionsDetailsVM)
                {
                    if (webinarPurchasedOptionsDetailVM.Id.Equals(new Guid("00000000-0000-0000-0000-000000000000")))
                    {
                        webinarPurchasedOptionsDetailVM.Id = Guid.NewGuid();
                        var webinarPurchasedOptionsDetail = _mapper.Map<WebinarPurchasedOptionsDetails>(webinarPurchasedOptionsDetailVM);
                        var response = _webinarPurchasedOptionsDetailsRepo.AddWebinarPurchasedOptionsDetails(webinarPurchasedOptionsDetail).Result;
                        if (response)
                        {
                            count++;
                        }
                        else
                        {
                            throw new Exception("Error in updating one of the price");
                        }
                    }
                    else
                    {
                        var webinarPurchasedOptionsDetail = _mapper.Map<WebinarPurchasedOptionsDetails>(webinarPurchasedOptionsDetailVM);
                        var response = _webinarPurchasedOptionsDetailsRepo.UpdateWebinarPurchasedOptionsDetails(webinarPurchasedOptionsDetail).Result;
                        if (response)
                        {
                            count++;
                        }
                        else
                        {
                            throw new Exception("Error in updating one of the price");
                        }
                    }
                }

                return count == webinarPurchasedOptionsDetailsVM.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
