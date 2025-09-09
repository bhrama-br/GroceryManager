using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Models.Dtos.ShoppingList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListService _shoppingListService;

        public ShoppingListController(IShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetShoppingListDto>>>> GetShoppingLists()
        {
            var result = await _shoppingListService.GetShoppingLists();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetShoppingListDto>>> GetShoppingList(int id)
        {
            var result = await _shoppingListService.GetShoppingList(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetShoppingListDto>>>> AddShoppingList(AddShoppingListDto newShoppingList)
        {
            var result = await _shoppingListService.AddShoppingList(newShoppingList);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetShoppingListDto>>> UpdateShoppingList(UpdateShoppingListDto updatedShoppingList)
        {
            var result = await _shoppingListService.UpdateShoppingList(updatedShoppingList);
            if (result.Data == null)
                return NotFound(result);
            return Ok(result);
        }
    }
}
