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
        public async Task<GetItemDto> AddItem(AddItemDto newItem)
        {
            var shoppingList = await _context.ShoppingLists
                .FirstOrDefaultAsync(sl => sl.Id == newItem.ShoppingListId);
            if (shoppingList is null)
                throw new Exception("Shopping list not found.");

            var item = _mapper.Map<Item>(newItem);
            item.ShoppingList = shoppingList;

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetItemDto>(item);
        }

        public async Task<List<GetItemDto>> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                throw new Exception("Item not found.");

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            var items = await _context.Items.ToListAsync();

            return _mapper.Map<List<GetItemDto>>(items);
        }

        public async Task<GetItemDto> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            return _mapper.Map<GetItemDto>(item);
        }

        public async Task<List<GetItemDto>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return _mapper.Map<List<GetItemDto>>(items);
        }

        public async Task<GetItemDto?> UpdateItem(UpdateItemDto updatedItem)
        {
            var item = await _context.Items.FindAsync(updatedItem.Id);
            if (item == null)
                return null;

            var updated_item = _mapper.Map<Item>(updatedItem);

            await _context.SaveChangesAsync();
            return _mapper.Map<GetItemDto>(updated_item);
        }
    }
}