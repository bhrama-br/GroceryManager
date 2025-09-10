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
        public async Task<List<GetItemDto>> GetItems()
        {
            var result = await _itemService.GetItems();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<GetItemDto> GetItem(int id)
        {
            var result = await _itemService.GetItem(id);
            return result;
        }

        [HttpPost]
        public async Task<GetItemDto> AddItem(AddItemDto newItem)
        {
            var result = await _itemService.AddItem(newItem);
            return result;
        }

        [HttpPut]
        public async Task<GetItemDto?> UpdateItem(UpdateItemDto updatedItem)
        {
            var result = await _itemService.UpdateItem(updatedItem);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<List<GetItemDto>> DeleteItem(int id)
        {
            var result = await _itemService.DeleteItem(id);
            return result;
        }
    }
}