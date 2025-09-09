using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Models;
using GroceryManager.Models.Dtos.ShoppingList;

namespace GroceryManager.Services.ShoppingListService
{
    public interface IShoppingListService
    {
        Task<ServiceResponse<List<GetShoppingListDto>>> GetShoppingLists();

        Task<ServiceResponse<List<GetShoppingListDto>>> AddShoppingList(AddShoppingListDto newShoppingList);

        Task<ServiceResponse<GetShoppingListDto>> GetShoppingList(int id);

        Task<ServiceResponse<GetShoppingListDto>> UpdateShoppingList(UpdateShoppingListDto updatedShoppingList);
    }
}