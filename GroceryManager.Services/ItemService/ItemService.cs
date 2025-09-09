using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryManager.Database;
using GroceryManager.Database.Entities;
using GroceryManager.Models;
using GroceryManager.Models.Dtos.Item;
using Microsoft.EntityFrameworkCore;

namespace GroceryManager.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ItemService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetItemDto>> AddItem(AddItemDto newItem)
        {
            var serviceResponse = new ServiceResponse<GetItemDto>();
            try
            {
                var shoppingList = await _context.ShoppingLists
                    .FirstOrDefaultAsync(sl => sl.Id == newItem.ShoppingListId);
                if (shoppingList is null)
                    throw new Exception("Shopping list not found.");

                var item = _mapper.Map<Item>(newItem);
                item.ShoppingList = shoppingList;

                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetItemDto>(item);
                serviceResponse.Success = true;
                serviceResponse.Message = "Item added successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> DeleteItem(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>();
            try
            {
                var item = await _context.Items.FindAsync(id);
                if (item == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Item not found.";
                    return serviceResponse;
                }

                _context.Items.Remove(item);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Items.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
                serviceResponse.Success = true;
                serviceResponse.Message = "Item deleted successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetItemDto>> GetItem(int id)
        {
            var serviceResponse = new ServiceResponse<GetItemDto>();
            try
            {
                var item = await _context.Items.FindAsync(id);
                if (item == null)
                    throw new Exception("Item not found.");
                serviceResponse.Data = _mapper.Map<GetItemDto>(item);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> GetItems()
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>();
            try
            {
                var items = await _context.Items.ToListAsync();
                serviceResponse.Data = items.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
                serviceResponse.Success = true;
                serviceResponse.Message = "Items retrieved successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetItemDto>> UpdateItem(UpdateItemDto updatedItem)
        {
            var serviceResponse = new ServiceResponse<GetItemDto>();
            try
            {
                var item = await _context.Items.FindAsync(updatedItem.Id);
                if (item == null)
                    throw new Exception("Item not found.");

                item.Name = updatedItem.Name;
                item.Supermarket = updatedItem.Supermarket;
                item.Quantity = updatedItem.Quantity;
                item.Names = updatedItem.Names;
                item.Notes = updatedItem.Notes;
                item.IsPurchased = updatedItem.IsPurchased;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetItemDto>(item);
                serviceResponse.Success = true;
                serviceResponse.Message = "Item updated successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}