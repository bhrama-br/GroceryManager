using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Models;
using GroceryManager.Models.Dtos.Item;

namespace GroceryManager.Services.ItemService
{
    public interface IItemService
    {
        Task<List<GetItemDto>> GetItems(CancellationToken cancellationToken);
        Task<GetItemDto> GetItem(int id, CancellationToken cancellationToken);
        Task<GetItemDto> AddItem(AddItemDto newItem, CancellationToken cancellationToken);
        Task<GetItemDto?> UpdateItem(UpdateItemDto updatedItem, CancellationToken cancellationToken);
        Task<List<GetItemDto>> DeleteItem(int id, CancellationToken cancellationToken);
    }
}