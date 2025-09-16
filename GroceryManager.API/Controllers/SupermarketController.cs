using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Models.Dtos.Supermarket;
using GroceryManager.Services.Supermarkets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupermarketController : ControllerBase
    {
        private readonly ISupermarketService _supermarketService;

        public SupermarketController(ISupermarketService supermarketService)
        {
            _supermarketService = supermarketService;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<GetSupermarketDto>> GetItems(CancellationToken cancellationToken)
        {
            var result = await _supermarketService.GetSupermarkets(cancellationToken);
            return result;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<GetSupermarketDto> GetItem(int id, CancellationToken cancellationToken)
        {
            var result = await _supermarketService.GetSupermarket(id, cancellationToken);
            return result;
        }

        [Authorize]
        [HttpPost]
        public async Task<List<GetSupermarketDto>> AddItem(AddSupermarketDto newItem, CancellationToken cancellationToken)
        {
            var result = await _supermarketService.AddSupermarket(newItem, cancellationToken);
            return result;
        }

        [Authorize]
        [HttpPut]
        public async Task<GetSupermarketDto?> UpdateItem(UpdateSupermarketDto updatedItem, CancellationToken cancellationToken)
        {
            var result = await _supermarketService.UpdateSupermarket(updatedItem, cancellationToken);
            return result;
        }
    }
}