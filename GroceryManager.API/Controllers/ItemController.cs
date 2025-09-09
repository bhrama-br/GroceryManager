using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Models.Dtos.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> GetItems()
        {
            var result = await _itemService.GetItems();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> GetItem(int id)
        {
            var result = await _itemService.GetItem(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> AddItem(AddItemDto newItem)
        {
            var result = await _itemService.AddItem(newItem);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> UpdateItem(UpdateItemDto updatedItem)
        {
            var result = await _itemService.UpdateItem(updatedItem);
            if (result.Data == null)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> DeleteItem(int id)
        {
            var result = await _itemService.DeleteItem(id);
            if (result.Data == null)
                return NotFound(result);
            return Ok(result);
        }
    }
}