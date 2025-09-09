using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryManager.Database;
using GroceryManager.Database.Entities;
using GroceryManager.Models;
using GroceryManager.Models.Dtos.ShoppingList;

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
        public async Task<ServiceResponse<List<GetShoppingListDto>>> AddShoppingList(AddShoppingListDto newShoppingList)
        {
            var serviceResponse = new ServiceResponse<List<GetShoppingListDto>>();
            try
            {
                var shoppingList = _mapper.Map<ShoppingList>(newShoppingList);
                _context.ShoppingLists.Add(shoppingList);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.ShoppingLists.Select(sl => _mapper.Map<GetShoppingListDto>(sl)).ToList();
                serviceResponse.Success = true;
                serviceResponse.Message = "Shopping list added successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetShoppingListDto>> GetShoppingList(int id)
        {
            var serviceResponse = new ServiceResponse<GetShoppingListDto>();
            try
            {
                var shoppingList = await _context.ShoppingLists.FindAsync(id);
                serviceResponse.Data = _mapper.Map<GetShoppingListDto>(shoppingList);
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
