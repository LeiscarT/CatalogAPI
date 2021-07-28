using System;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;


namespace Catalog.Controllers
{


    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase{

      private readonly InMemItemRepository repository;

      public ItemsController(){
        repository = new InMemItemRepository();
      }


      [HttpGet]

      public IEnumerable<Item> GetItems(){
        var items = repository.GetItems();
        return items;
      }




    


      
    }
}