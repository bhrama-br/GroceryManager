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
        Task<List<GetItemDto>> GetItems();
        Task<GetItemDto> GetItem(int id);
        Task<GetItemDto> AddItem(AddItemDto newItem);
        Task<GetItemDto?> UpdateItem(UpdateItemDto updatedItem);
        Task<List<GetItemDto>> DeleteItem(int id);
    }
}