using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryManager.Database;
using GroceryManager.Database.Entities;
using GroceryManager.Models;
using GroceryManager.Models.Dtos.ShoppingList;
using Microsoft.EntityFrameworkCore;

namespace GroceryManager.Services.ShoppingListService
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ShoppingListService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetShoppingListDto>> AddShoppingList(AddShoppingListDto newShoppingList)
        {
            var shoppingList = _mapper.Map<ShoppingList>(newShoppingList);
            _context.ShoppingLists.Add(shoppingList);
            await _context.SaveChangesAsync();

            var shoppingLists = await _context.ShoppingLists.Include(sl => sl.Items).ToListAsync();
            return _mapper.Map<List<GetShoppingListDto>>(shoppingLists);
        }

        public async Task<GetShoppingListDto> GetShoppingList(int id)
        {
            var shoppingList = await _context.ShoppingLists.Include(sl => sl.Items).FirstOrDefaultAsync(sl => sl.Id == id);
            return _mapper.Map<GetShoppingListDto>(shoppingList);
        }

        public async Task<List<GetShoppingListDto>> GetShoppingLists()
        {
            var shoppingLists = await _context.ShoppingLists.Include(sl => sl.Items).ToListAsync();
            return _mapper.Map<List<GetShoppingListDto>>(shoppingLists);
        }

        public async Task<GetShoppingListDto?> UpdateShoppingList(UpdateShoppingListDto updatedShoppingList)
        {
            var shoppingList = await _context.ShoppingLists.Include(sl => sl.Items).FirstOrDefaultAsync(sl => sl.Id == updatedShoppingList.Id);
            if (shoppingList == null)
                return null;

            _mapper.Map(updatedShoppingList, shoppingList);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetShoppingListDto>(shoppingList);
        }
    }
}
