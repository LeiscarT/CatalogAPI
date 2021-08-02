using System;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;
using System.Linq;
using Catalog.Dtos;
using System.Threading.Tasks;

namespace Catalog.Controllers
{


    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase{

      private readonly IItemsRepository repository;

      public ItemsController(IItemsRepository repository)
      {
        this.repository = repository;
      }


      [HttpGet]
      public async Task<IEnumerable<ItemDto>> GetItemsAsync(){
        var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
        return items;
      }


      [HttpGet("{id}")]
      public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id){
        var item = await repository.GetItemAsync(id);

        if (item is null){
          return NotFound();
        }

        return item.AsDto();

      }


      //post/items
      [HttpPost]
      public async Task<ActionResult<ItemDto>>CreateItemAsync(CreateItemDto itemDto){
        Item item= new(){
          Id = Guid.NewGuid(),
          Name = itemDto.Name,
          Price = itemDto.Price,
          CreatedDate = DateTimeOffset.UtcNow
        };

        await repository.CreateItemAsync(item);
        return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.AsDto());
      
      } 


      //PUT /items/{id}
      [HttpPut("{id}")]
      public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto){
        var existinItem = await repository.GetItemAsync(id);
        
        if (existinItem is null){
          return NotFound();
        }

        Item updateItem = existinItem with {
          Name = itemDto.Name,
          Price = itemDto.Price
        };

        await repository.UpdateItemAsync(updateItem);
       
        return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<ActionResult> DeleteItemAsync(Guid id){
        var existingItem = await repository.GetItemAsync(id);

        if(existingItem is null){
          return NotFound();  
        }

        await repository.DeleteItemAsync(id);
        return NoContent();


      }


    


      
    }
}