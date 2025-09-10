using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Models.Dtos.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly CancellationToken _cancellationToken;

        public ItemController(IItemService itemService, CancellationToken cancellationToken)
        {
            _itemService = itemService;
            _cancellationToken = cancellationToken;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<List<GetItemDto>> GetItems()
        {
            var result = await _itemService.GetItems(_cancellationToken);
            return result;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<GetItemDto> GetItem(int id)
        {
            var result = await _itemService.GetItem(id, _cancellationToken);
            return result;
        }

        [Authorize]
        [HttpPost]
        public async Task<GetItemDto> AddItem(AddItemDto newItem)
        {
            var result = await _itemService.AddItem(newItem, _cancellationToken);
            return result;
        }

        [Authorize]
        [HttpPut]
        public async Task<GetItemDto?> UpdateItem(UpdateItemDto updatedItem)
        {
            var result = await _itemService.UpdateItem(updatedItem, _cancellationToken);
            return result;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<List<GetItemDto>> DeleteItem(int id)
        {
            var result = await _itemService.DeleteItem(id, _cancellationToken);
            return result;
        }
    }
}