﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Utility.Attributes;

namespace Vaccines_Scheduling.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentSignUpController : ControllerBase
    {
        private readonly IAppointmentSignUpBusiness _appointmentSignUpBusiness;

        public AppointmentSignUpController(IAppointmentSignUpBusiness appointmentSignUpBusiness)
        {
            _appointmentSignUpBusiness = appointmentSignUpBusiness;
        }
        [Authorize]
        [HttpGet("FindAppointments")]
        public async Task<List<AppointmentDTO>> FindAppointments()
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            return await _appointmentSignUpBusiness.GetPatientAppointments(userId);
        }
        [Authorize]
        [MandatoryTransactions]
        [HttpDelete("DeleteAppointment")]
        public async Task<List<AppointmentDTO>> DeleteAppointment(AppointmentChangeModel appointment)
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            return await _appointmentSignUpBusiness.DeleteAppointment(appointment, userId);
        }
        [Authorize]
        [MandatoryTransactions]
        [HttpPut("ChangeAppointmentStatus")]
        public async Task<List<AppointmentDTO>> ChangeAppointmentStatus(AppointmentChangeModel appointment)
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            return await _appointmentSignUpBusiness.ChangeAppointmentStatus(appointment, userId);
        }
        [Authorize]
        [MandatoryTransactions]
        [HttpPost("MakeAppointment")]
        public async Task<List<AppointmentDTO>> InsertAppointment(AppointmentSignUpModel newAppointment)
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            return await _appointmentSignUpBusiness.InsertAppointment(newAppointment, userId);
        }
    }
}
