using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;

namespace TraincelAPI.Repository.Interface
{
    public interface IWebinarRepo
    {
        public Task<List<Webinars>> GetWebinars(String userId);
        public Task<List<Webinars>> GetRecentlyUpdatedWebinars();
        public Task<List<Webinars>> GetCategoryWebinars(int categoryId, String userId);
        public Task<List<Webinars>> GetWebinarTypeWebinars(int webinarTypeId, String userId);
        public Task<List<Webinars>> GetFilteredWebinars(int webinarTypeId, int categoryId, String userId);
        public Task<List<Webinars>> GetFeaturedWebinars(String userId);
        public Task<bool> AddWebinar(Webinars webinar);
        public Task<bool> DeleteWebinar(Guid id);
        public Task<Webinars> GetWebinar(String userId, Guid webinarId);
        public Task<bool> UpdateWebinar(Webinars webinar);
        public Task<List<Webinars>> GetHomePageWebinars(String userId);
        public Webinars GetWebinarsUserCartAndOrderItems(Webinars webinar, string userId);
    }
}
