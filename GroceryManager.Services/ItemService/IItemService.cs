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
        Task<ServiceResponse<List<GetItemDto>>> GetItems();
        Task<ServiceResponse<GetItemDto>> GetItem(int id);
        Task<ServiceResponse<GetItemDto>> AddItem(AddItemDto newItem);
        Task<ServiceResponse<GetItemDto>> UpdateItem(UpdateItemDto updatedItem);
        Task<ServiceResponse<List<GetItemDto>>> DeleteItem(int id);
    }
}