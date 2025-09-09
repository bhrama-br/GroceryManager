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
                var shoppingList = await _context.ShoppingLists.Include(sl => sl.Items).FirstOrDefaultAsync(sl => sl.Id == id);
                serviceResponse.Data = _mapper.Map<GetShoppingListDto>(shoppingList);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetShoppingListDto>>> GetShoppingLists()
        {
            var serviceResponse = new ServiceResponse<List<GetShoppingListDto>>();
            try
            {
                var shoppingLists = await _context.ShoppingLists.Include(sl => sl.Items).ToListAsync();
                serviceResponse.Data = shoppingLists.Select(sl => _mapper.Map<GetShoppingListDto>(sl)).ToList();
                serviceResponse.Success = true;
                serviceResponse.Message = "Shopping lists retrieved successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetShoppingListDto>> UpdateShoppingList(UpdateShoppingListDto updatedShoppingList)
        {
            var serviceResponse = new ServiceResponse<GetShoppingListDto>();
            try
            {
                var shoppingList = await _context.ShoppingLists.FindAsync(updatedShoppingList.Id);
                if (shoppingList == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Shopping list not found.";
                    return serviceResponse;
                }

                shoppingList.IsPurchased = updatedShoppingList.IsPurchased;
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetShoppingListDto>(shoppingList);
                serviceResponse.Success = true;
                serviceResponse.Message = "Shopping list updated successfully.";
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
