using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using gregslist_api.Models;

namespace gregslist_api.Repositories
{
  public class HousesRepository
  {
    private readonly IDbConnection _db;

    public HousesRepository(IDbConnection db)
    {
      _db = db;
    }

    public IEnumerable<House> Get()
    {
      string sql = "SELECT * FROM houses";
      return _db.Query<House>(sql);
    }

    public IEnumerable<House> Get(string userId)
    {
      string sql = "SELECT * FROM houses WHERE userId = @UserId;";
      return _db.Query<House>(sql, new { userId });
    }

    public House GetById(int id)
    {
      string sql = "SELECT * FROM houses WHERE id = @id;";
      return _db.QueryFirstOrDefault<House>(sql, new { id });
    }
    public House Create(House newHouse)
    {
      string sql = @"
      INSERT INTO houses
      (Title, Price, Year, Description, ImgUrl, UserId)
      VALUES
      (@title, @price, @year, @description, @imgurl, @userId);
      SELECT LAST_INSERT_ID();
      ";
      newHouse.Id = _db.ExecuteScalar<int>(sql, newHouse);
      return newHouse;
    }
    public bool Delete(string userId, int id)
    {
      string sql = "DELETE FROM houses WHERE id = @Id AND userId = @UserId LIMIT 1;";
      int rowsAffected = _db.Execute(sql, new { userId, id });
      return rowsAffected == 1;
    }

    public bool Update(House updatedHouse)
    {
      string sql = @"
        UPDATE houses 
        SET
        Title = @title,
        Price = @price,
        Year = @year,
        Description = @description,
        ImgUrl = @imgUrl
        WHERE Id = @id AND UserId = @userId LIMIT 1;
        ";
      int rowsAffected = _db.Execute(sql, updatedHouse);
      return rowsAffected == 1;
    }
  }
}