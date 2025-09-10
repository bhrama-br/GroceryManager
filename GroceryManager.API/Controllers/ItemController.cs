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

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<List<GetItemDto>> GetItems(CancellationToken cancellationToken)
        {
            var result = await _itemService.GetItems(cancellationToken);
            return result;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<GetItemDto> GetItem(int id, CancellationToken cancellationToken)
        {
            var result = await _itemService.GetItem(id, cancellationToken);
            return result;
        }

        [Authorize]
        [HttpPost]
        public async Task<GetItemDto> AddItem(AddItemDto newItem, CancellationToken cancellationToken)
        {
            var result = await _itemService.AddItem(newItem, cancellationToken);
            return result;
        }

        [Authorize]
        [HttpPut]
        public async Task<GetItemDto?> UpdateItem(UpdateItemDto updatedItem, CancellationToken cancellationToken)
        {
            var result = await _itemService.UpdateItem(updatedItem, cancellationToken);
            return result;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<List<GetItemDto>> DeleteItem(int id, CancellationToken cancellationToken)
        {
            var result = await _itemService.DeleteItem(id, cancellationToken);
            return result;
        }
    }
}