using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraincelAPI.Models.VM;
using TraincelAPI.Services.Interface;
using TraincelAPI.Utilities;

namespace TraincelAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public bool SendEmail(CommonEmailVM commonEmailVM)
        {
            try
            {
                commonEmailVM.ApiKey = _config.GetSection("SEND_GRID_KEY").Value;
                var response =  EmailUtility.SendEmail(commonEmailVM).Result;
                return response.StatusCode == System.Net.HttpStatusCode.Accepted ? true : false;

            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
