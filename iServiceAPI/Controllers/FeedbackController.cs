﻿using iServiceRepositories.Repositories.Models.Request;
using iServiceRepositories.Repositories.Models;
using iServiceServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace iServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(IConfiguration configuration)
        {
            _feedbackService = new FeedbackService(configuration);
        }

        [HttpGet]
        public ActionResult<List<Feedback>> Get()
        {
            var result = _feedbackService.GetAllFeedbacks();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(new { message = result.ErrorMessage });
        }

        [HttpGet("{feedbackId}")]
        public ActionResult<Feedback> GetById(int feedbackId)
        {
            var result = _feedbackService.GetFeedbackById(feedbackId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(new { message = result.ErrorMessage });
        }

        [HttpPost]
        public ActionResult<Feedback> Post([FromBody] FeedbackModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _feedbackService.AddFeedback(model);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { feedbackId = result.Value.FeedbackID }, result.Value);
            }

            return BadRequest(new { message = result.ErrorMessage });
        }

        [HttpPut("{feedbackId}")]
        public ActionResult<Feedback> Put(int feedbackId, [FromBody] Feedback feedback)
        {
            if (feedbackId != feedback.FeedbackID)
            {
                return BadRequest();
            }

            var result = _feedbackService.UpdateFeedback(feedback);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(new { message = result.ErrorMessage });
        }

        [HttpDelete("{feedbackId}")]
        public IActionResult Delete(int feedbackId)
        {
            var result = _feedbackService.DeleteFeedback(feedbackId);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            return NotFound(new { message = result.ErrorMessage });
        }
    }
}