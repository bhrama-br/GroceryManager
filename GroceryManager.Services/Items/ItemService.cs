using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryManager.Database;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Item;
using Microsoft.EntityFrameworkCore;

namespace GroceryManager.Services.Items
{
    public interface IItemService
    {
        Task<List<GetItemDto>> GetItems(CancellationToken cancellationToken);
        Task<GetItemDto> GetItem(int id, CancellationToken cancellationToken);
        Task<GetItemDto> AddItem(AddItemDto newItem, CancellationToken cancellationToken);
        Task<GetItemDto?> UpdateItem(UpdateItemDto updatedItem, CancellationToken cancellationToken);
        Task<List<GetItemDto>> DeleteItem(int id, CancellationToken cancellationToken);
    }

    public class ItemService : IItemService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ItemService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetItemDto> AddItem(AddItemDto newItem, CancellationToken cancellationToken)
        {
            var shoppingList = await _context.ShoppingLists
                .FirstOrDefaultAsync(sl => sl.Id == newItem.ShoppingListId, cancellationToken);
            if (shoppingList is null)
                throw new Exception("Shopping list not found.");

            var supermarket = await _context.Supermarkets
                .FirstOrDefaultAsync(sm => sm.Id == newItem.SupermarketId, cancellationToken);
            if (supermarket is null)
                throw new Exception("Supermarket not found.");

            var item = _mapper.Map<Item>(newItem);
            item.ShoppingList = shoppingList;
            item.Supermarket = supermarket;

            _context.Items.Add(item);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetItemDto>(item);
        }

        public async Task<List<GetItemDto>> DeleteItem(int id, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(id, cancellationToken);
            if (item == null)
                throw new Exception("Item not found.");

            _context.Items.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            var items = await _context.Items.ToListAsync(cancellationToken);

            return _mapper.Map<List<GetItemDto>>(items);
        }

        public async Task<GetItemDto> GetItem(int id, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(id, cancellationToken);
            return _mapper.Map<GetItemDto>(item);
        }

        public async Task<List<GetItemDto>> GetItems(CancellationToken cancellationToken)
        {
            var items = await _context.Items.ToListAsync(cancellationToken);
            return _mapper.Map<List<GetItemDto>>(items);
        }

        public async Task<GetItemDto?> UpdateItem(UpdateItemDto updatedItem, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(updatedItem.Id, cancellationToken);
            if (item == null)
                return null;

            var updated_item = _mapper.Map<Item>(updatedItem);

            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GetItemDto>(updated_item);
        }
    }
}