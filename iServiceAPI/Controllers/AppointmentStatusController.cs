﻿using iServiceRepositories.Repositories.Models;
using iServiceRepositories.Repositories.Models.Request;
using iServiceServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace iServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentStatusController : ControllerBase
    {
        private readonly AppointmentStatusService _appointmentStatusService;

        public AppointmentStatusController(IConfiguration configuration)
        {
            _appointmentStatusService = new AppointmentStatusService(configuration);
        }

        [HttpGet]
        public ActionResult<List<AppointmentStatus>> Get()
        {
            var result = _appointmentStatusService.GetAllAppointmentStatuses();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(new { message = result.ErrorMessage });
        }

        [HttpGet("{appointmentStatusId}")]
        public ActionResult<AppointmentStatus> GetById(int appointmentStatusId)
        {
            var result = _appointmentStatusService.GetAppointmentStatusById(appointmentStatusId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(new { message = result.ErrorMessage });
        }

        [HttpPost]
        public ActionResult<AppointmentStatus> Post([FromBody] AppointmentStatusModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _appointmentStatusService.AddAppointmentStatus(model);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { appointmentStatusId = result.Value.AppointmentStatusID }, result.Value);
            }

            return BadRequest(new { message = result.ErrorMessage });
        }

        [HttpPut("{appointmentStatusId}")]
        public ActionResult<AppointmentStatus> Put(int appointmentStatusId, [FromBody] AppointmentStatus appointmentStatus)
        {
            if (appointmentStatusId != appointmentStatus.AppointmentStatusID)
            {
                return BadRequest();
            }

            var result = _appointmentStatusService.UpdateAppointmentStatus(appointmentStatus);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(new { message = result.ErrorMessage });
        }

        [HttpDelete("{appointmentStatusId}")]
        public IActionResult Delete(int appointmentStatusId)
        {
            var result = _appointmentStatusService.DeleteAppointmentStatus(appointmentStatusId);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            return NotFound(new { message = result.ErrorMessage });
        }
    }
}