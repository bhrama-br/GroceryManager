using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Models.Dtos.ShoppingList;
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

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetShoppingListDto>>>> AddShoppingList(AddShoppingListDto newShoppingList)
        {
            var result = await _shoppingListService.AddShoppingList(newShoppingList);
            return Ok(result);
        }
    }
}
