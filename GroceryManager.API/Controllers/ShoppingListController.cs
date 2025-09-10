using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Models.Dtos.ShoppingList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListService _shoppingListService;
        private readonly CancellationToken _cancellationToken;

        public ShoppingListController(IShoppingListService shoppingListService, CancellationToken cancellationToken)
        {
            _shoppingListService = shoppingListService;
            _cancellationToken = cancellationToken;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<GetShoppingListDto>> GetShoppingLists()
        {
            var result = await _shoppingListService.GetShoppingLists(_cancellationToken);
            return result;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<GetShoppingListDto> GetShoppingList(int id)
        {
            var result = await _shoppingListService.GetShoppingList(id, _cancellationToken);
            return result;
        }

        [Authorize]
        [HttpPost]
        public async Task<List<GetShoppingListDto>> AddShoppingList(AddShoppingListDto newShoppingList)
        {
            var result = await _shoppingListService.AddShoppingList(newShoppingList, _cancellationToken);
            return result;
        }

        [Authorize]
        [HttpPut]
        public async Task<GetShoppingListDto?> UpdateShoppingList(UpdateShoppingListDto updatedShoppingList)
        {
            var result = await _shoppingListService.UpdateShoppingList(updatedShoppingList, _cancellationToken);
            return result;
        }
    }
}
