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
        Task<List<GetShoppingListDto>> GetShoppingLists();

        Task<List<GetShoppingListDto>> AddShoppingList(AddShoppingListDto newShoppingList);

        Task<GetShoppingListDto> GetShoppingList(int id);

        Task<GetShoppingListDto?> UpdateShoppingList(UpdateShoppingListDto updatedShoppingList);
    }
}