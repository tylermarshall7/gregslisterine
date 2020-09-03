using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using gregslist_api.Models;
using gregslist_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gregslist_api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class HousesController : ControllerBase
  {
    private readonly HousesService _service;

    public HousesController(HousesService service)
    {
      _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<House>> Get()
    {
      try
      {
        return Ok(_service.Get());
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<House> GetById(int id)
    {
      try
      {
        return Ok(_service.GetById(id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    // NOTE use this if you want to lock just this route
    [Authorize]
    [HttpGet("user")]
    public ActionResult<IEnumerable<House>> GetMyItems()
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to make a House, yo.");
        }
        return Ok(_service.Get(user.Value));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [Authorize]
    [HttpPost]
    public ActionResult<House> Create([FromBody] House newHouse)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to make a House, yo.");
        }
        newHouse.UserId = user.Value;
        return Ok(_service.Create(newHouse));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to make a House, yo.");
        }
        return Ok(_service.Delete(user.Value, id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public ActionResult<House> Update(int id, [FromBody] House updatedHouse)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to make a House, yo.");
        }
        updatedHouse.UserId = user.Value;
        updatedHouse.Id = id;
        return Ok(_service.Update(updatedHouse));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

  }
}