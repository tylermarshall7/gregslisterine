using System;
using System.Collections.Generic;
using gregslist_api.Models;
using gregslist_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace gregslist_api.Services
{
  public class HousesService
  {
    private readonly HousesRepository _repo;

    public HousesService(HousesRepository repo)
    {
      _repo = repo;
    }

    public IEnumerable<House> Get()
    {
      return _repo.Get();
    }

    public IEnumerable<House> Get(string userId)
    {
      return _repo.Get(userId);
    }

    public House GetById(int id)
    {
      House foundHouse = _repo.GetById(id);
      if (foundHouse == null)
      {
        throw new Exception("Invalid House Id");
      }
      return foundHouse;
    }
    public House Create(House newHouse)
    {
      return _repo.Create(newHouse);
    }

    public string Delete(string userId, int id)
    {
      GetById(id);
      bool delorted = _repo.Delete(userId, id);
      if (!delorted)
      {
        throw new Exception("Oops ALL BERRIES You are not the owner of this House");
      }
      return "Delorted!";
    }

    public House Update(House updatedHouse)
    {
      House foundHouse = GetById(updatedHouse.Id);
      updatedHouse.Title = updatedHouse.Title == null ? foundHouse.Title : updatedHouse.Title;
      updatedHouse.Price = updatedHouse.Price == 0 ? foundHouse.Price : updatedHouse.Price;
      updatedHouse.Year = updatedHouse.Year == 0 ? foundHouse.Year : updatedHouse.Year;
      updatedHouse.Description = updatedHouse.Description == null ? foundHouse.Description : updatedHouse.Description;
      updatedHouse.ImgUrl = updatedHouse.ImgUrl == null ? foundHouse.ImgUrl : updatedHouse.ImgUrl;
      updatedHouse.UserId = foundHouse.UserId;
      // DO ALL THE OTHER THINGS
      bool updated = _repo.Update(updatedHouse);
      if (!updated)
      {
        throw new Exception("Oops ALL BERRIES You are not the owner of this House");
      }
      return updatedHouse;
    }
  }
}