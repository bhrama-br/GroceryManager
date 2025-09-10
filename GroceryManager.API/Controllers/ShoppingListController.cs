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

        public ShoppingListController(IShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<GetShoppingListDto>> GetShoppingLists(CancellationToken cancellationToken)
        {
            var result = await _shoppingListService.GetShoppingLists(cancellationToken);
            return result;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<GetShoppingListDto> GetShoppingList(int id, CancellationToken cancellationToken)
        {
            var result = await _shoppingListService.GetShoppingList(id, cancellationToken);
            return result;
        }

        [Authorize]
        [HttpPost]
        public async Task<List<GetShoppingListDto>> AddShoppingList(AddShoppingListDto newShoppingList, CancellationToken cancellationToken)
        {
            var result = await _shoppingListService.AddShoppingList(newShoppingList, cancellationToken);
            return result;
        }

        [Authorize]
        [HttpPut]
        public async Task<GetShoppingListDto?> UpdateShoppingList(UpdateShoppingListDto updatedShoppingList, CancellationToken cancellationToken)
        {
            var result = await _shoppingListService.UpdateShoppingList(updatedShoppingList, cancellationToken);
            return result;
        }
    }
}
