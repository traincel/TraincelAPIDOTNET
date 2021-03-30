using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.DB;
using TraincelAPI.Repository.Interface;
using Z.EntityFramework.Plus;

namespace TraincelAPI.Repository
{



    public class WebinarRepo : IWebinarRepo
    {
        private readonly TraincelContext _context;
        //private readonly IMapper _mapper;

        public WebinarRepo(TraincelContext context)
        {
            _context = context;

        }

        public async Task<bool> AddWebinar(Webinars webinar)
        {
            try
            {
                webinar.FacultyLocalId = _context.Faculties.FirstOrDefaultAsync(faculty => faculty.Id == webinar.FacultyId).Result.LocalId;
                _context.Webinars.Add(webinar);
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<bool> DeleteWebinar(Guid id)
        {
            var webinar = await _context.Webinars.FirstOrDefaultAsync(webinar => webinar.Id == id);
            if (webinar == null)
            {
                throw new Exception("No such Webinar exist");
            }

            try
            {
                _context.Webinars.Remove(webinar);
                var response = await _context.SaveChangesAsync();

                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<List<Webinars>> GetWebinars(String userId)
        {
            try
            {
                var webinars = await _context.Webinars
                    .Include(webinar => webinar.WebinarType)
               .OrderBy(webinar => webinar.DateAndTime)
               .Include(webinar => webinar.Faculty)
               .Include(webinar => webinar.Category)
               .Include(webinar => webinar.WebinarPurchasedOptionsDetails)
               .ThenInclude(webinarPurchasedOptionsDetails => webinarPurchasedOptionsDetails.PurchaseOption)
               .ThenInclude(purchaseOptions => purchaseOptions.Type)
               .ToListAsync();
                webinars.ForEach(webinar =>
                {
                    webinar = GetWebinarsUserCartAndOrderItems(webinar, userId);
                });
                return webinars;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Webinars>> GetRecentlyUpdatedWebinars()
        {
            try
            {
                var webinars = await _context.Webinars.Include(webinar => webinar.WebinarType).OrderByDescending(webinar => webinar.CreatedOn).Take(5)
               .ToListAsync();
                return webinars;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Webinars> GetWebinar(String userId, Guid webinarId)
        {
            try
            {
                var webinar = await _context.Webinars.Include(webinar => webinar.WebinarType)
               .Include(webinar => webinar.Faculty)
               .Include(webinar => webinar.Category)
               .Include(webinar => webinar.WebinarPurchasedOptionsDetails)
               .ThenInclude(webinarPurchasedOptionsDetails => webinarPurchasedOptionsDetails.PurchaseOption)
               .ThenInclude(purchaseOptions => purchaseOptions.Type)
               .FirstOrDefaultAsync(webinar => webinar.Id == webinarId && !webinar.IsDeleted);
                webinar = GetWebinarsUserCartAndOrderItems(webinar, userId);
                return webinar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Webinars>> GetFeaturedWebinars(String userId)
        {
            try
            {
                var webinars = await _context.Webinars
                    .Include(webinar => webinar.WebinarType)
                    .Include(webinar => webinar.Faculty)
                    .Include(webinar => webinar.Category)
                    .Include(webinar => webinar.WebinarPurchasedOptionsDetails)
                    .ThenInclude(webinarPurchasedOptionsDetails => webinarPurchasedOptionsDetails.PurchaseOption)
                    .ThenInclude(purchaseOptions => purchaseOptions.Type)
                    .Where(webinar => webinar.IsFeatured && !webinar.IsDeleted)
                    .ToListAsync();

                webinars.ForEach(webinar =>
                {
                    webinar = GetWebinarsUserCartAndOrderItems(webinar, userId);
                });
                return webinars;
            }
            catch (DbException)
            {
                throw new Exception("Error in fetching Featured Webinars");
            }
        }

        public async Task<List<Webinars>> GetCategoryWebinars(int categoryId, String userId)
        {
            try
            {
                var webinars = await _context.Webinars
                    .Include(webinar => webinar.WebinarType)
                    .Include(webinar => webinar.Faculty)
                    .Include(webinar => webinar.Category)
                    .Include(webinar => webinar.WebinarPurchasedOptionsDetails)
                    .ThenInclude(webinarPurchasedOptionsDetails => webinarPurchasedOptionsDetails.PurchaseOption)
                    .ThenInclude(purchaseOptions => purchaseOptions.Type)
                    .Where(webinar => webinar.CategoryId == categoryId && !webinar.IsDeleted).ToListAsync();

                webinars.ForEach(webinar =>
                {
                    webinar = GetWebinarsUserCartAndOrderItems(webinar, userId);
                });

                return webinars;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Webinars>> GetWebinarTypeWebinars(int webinarTypeId, String userId)
        {
            try
            {
                var webinars = await _context.Webinars
                    .Include(webinar => webinar.WebinarType)
                    .Include(webinar => webinar.Faculty)
                    .Include(webinar => webinar.Category)
                    .Include(webinar => webinar.WebinarPurchasedOptionsDetails)
                    .ThenInclude(webinarPurchasedOptionsDetails => webinarPurchasedOptionsDetails.PurchaseOption)
                    .ThenInclude(purchaseOptions => purchaseOptions.Type)
                    .Where(webinar => webinar.WebinarTypeId == webinarTypeId && !webinar.IsDeleted).ToListAsync();

                webinars.ForEach(webinar =>
                {
                    webinar = GetWebinarsUserCartAndOrderItems(webinar, userId);
                });

                return webinars;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Webinars>> GetFilteredWebinars(int webinarTypeId, int categoryId, String userId)
        {
            try
            {
                var webinars = await _context.Webinars.Include(webinar => webinar.WebinarType)
                    .Include(webinar => webinar.Faculty)
                    .Include(webinar => webinar.Category)
                    .Include(webinar => webinar.WebinarPurchasedOptionsDetails)
                    .ThenInclude(webinarPurchasedOptionsDetails => webinarPurchasedOptionsDetails.PurchaseOption)
                    .ThenInclude(purchaseOptions => purchaseOptions.Type)
                    .Where(webinar => webinar.WebinarTypeId == webinarTypeId && webinar.CategoryId == categoryId && !webinar.IsDeleted).ToListAsync();

                webinars.ForEach(webinar =>
                {
                    webinar = GetWebinarsUserCartAndOrderItems(webinar, userId);
                });

                return webinars;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateWebinar(Webinars webinar)
        {
            try
            {
                var modelToBeUpdated = _context.Webinars.FirstOrDefaultAsync(webinarDetail => webinarDetail.Id == webinar.Id).Result;
                modelToBeUpdated.WebinarName = webinar.WebinarName;
                modelToBeUpdated.CategoryId = webinar.CategoryId;
                modelToBeUpdated.IsFeatured = webinar.IsFeatured;
                modelToBeUpdated.WebinarTypeId = webinar.WebinarTypeId;
                modelToBeUpdated.Duration = webinar.Duration;
                modelToBeUpdated.Overview = webinar.Overview;
                modelToBeUpdated.LearningObjectives = webinar.LearningObjectives;
                modelToBeUpdated.ReasonToAttend = webinar.ReasonToAttend;
                modelToBeUpdated.AreasCovered = webinar.AreasCovered;
                modelToBeUpdated.WhoWillBenefit = webinar.WhoWillBenefit;
                modelToBeUpdated.FacultyId = webinar.FacultyId;
                modelToBeUpdated.DateAndTime = webinar.DateAndTime;
                modelToBeUpdated.ThumbImageUrl = webinar.ThumbImageUrl;
                modelToBeUpdated.FacultyLocalId = _context.Faculties.FirstOrDefaultAsync(faculty => faculty.Id == webinar.FacultyId).Result.LocalId;

                _context.Entry(modelToBeUpdated).State = EntityState.Modified;
                var response = await _context.SaveChangesAsync();
                return response >= 1;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<List<Webinars>> GetHomePageWebinars(String userId)
        {
            try
            {
                var webinars = await _context.Webinars
                    .Include(webinar => webinar.WebinarType)
                    .Include(webinar => webinar.Faculty)
                    .Include(webinar => webinar.Category)
                    .Include(webinar => webinar.WebinarPurchasedOptionsDetails)
                    .ThenInclude(webinarPurchasedOptionsDetails => webinarPurchasedOptionsDetails.PurchaseOption)
                    .ThenInclude(purchaseOptions => purchaseOptions.Type)
                    .OrderBy(webinar => webinar.CreatedOn)
                    .Where(webinar => webinar.WebinarTypeId == 1 && !webinar.IsDeleted)
                    .Take(8)
               .ToListAsync();
                webinars.ForEach(webinar =>
                {
                    webinar = GetWebinarsUserCartAndOrderItems(webinar, userId);
                });
                return webinars;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Webinars GetWebinarsUserCartAndOrderItems(Webinars webinar, string userId)
        {
            if (userId != null)
            {
                webinar.CartItems = webinar.CartItems.Where(cartItem => cartItem.Cart.UserId == new Guid(userId)).ToList();
                webinar.OrderItems = webinar.OrderItems.Where(orderItem => orderItem.Order.UserId == new Guid(userId)).ToList();
            }
            return webinar;
        }

    }
}
