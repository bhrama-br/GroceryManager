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
        Task<List<GetShoppingListDto>> GetShoppingLists(CancellationToken cancellationToken);

        Task<List<GetShoppingListDto>> AddShoppingList(AddShoppingListDto newShoppingList, CancellationToken cancellationToken);

        Task<GetShoppingListDto> GetShoppingList(int id, CancellationToken cancellationToken);

        Task<GetShoppingListDto?> UpdateShoppingList(UpdateShoppingListDto updatedShoppingList, CancellationToken cancellationToken);
    }
}