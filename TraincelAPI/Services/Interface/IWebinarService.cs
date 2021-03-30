using System;
using System.Collections.Generic;
using TraincelAPI.Models.DB;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Services.Interface
{
   public interface IWebinarService
    {
        #region Webinars
        public List<WebinarsVM> GetWebinars(int webinarTypeId, int categoryId, String userId);
        public List<WebinarsVM> GetRecentlyUpdatedWebinars();
        public bool AddWebinar(WebinarsVM webinars);
        public bool DeleteWebinar(String id);
        public WebinarsVM GetWebinar(String userId, Guid webinarId);
        public List<WebinarsVM> GetFeaturedWebinars(String userId);
        public bool UpdateWebinar(WebinarsVM webinarsVM);
        public List<WebinarsVM> SearchWebinars(string searchTerm, int webinarTypeId, int categoryId, String userId);
        public List<WebinarsVM> GetHomePageWebinars(String userId);

        #endregion


        #region WebinarType
        public List<WebinarTypeVM> GetWebinarType();
        public bool AddWebinarType(WebinarTypeVM webinarTypeVM);
        public bool DeleteWebinarType(int id);

        #endregion

        #region WebinarPrice
        public bool UpdateWebinarPrice(List<WebinarPurchasedOptionsDetailsVM> webinarPurchasedOptionsDetails);

        #endregion


    }
}
