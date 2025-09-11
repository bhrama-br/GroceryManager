using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryManager.Database;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Supermarket;
using Microsoft.EntityFrameworkCore;

namespace GroceryManager.Services.Supermarkets
{
    public interface ISupermarketService
    {
        Task<List<GetSupermarketDto>> GetSupermarkets(CancellationToken cancellationToken);

        Task<List<GetSupermarketDto>> AddSupermarket(AddSupermarketDto newSupermarket, CancellationToken cancellationToken);

        Task<GetSupermarketDto> GetSupermarket(int id, CancellationToken cancellationToken);

        Task<GetSupermarketDto?> UpdateSupermarket(UpdateSupermarketDto updatedSupermarket, CancellationToken cancellationToken);
    }

    public class SupermarketService : ISupermarketService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SupermarketService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetSupermarketDto>> AddSupermarket(AddSupermarketDto newSupermarket, CancellationToken cancellationToken)
        {
            var supermarket = _mapper.Map<Supermarket>(newSupermarket);
            _context.Supermarkets.Add(supermarket);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<List<GetSupermarketDto>>(await _context.Supermarkets.ToListAsync(cancellationToken));
        }

        public async Task<GetSupermarketDto> GetSupermarket(int id, CancellationToken cancellationToken)
        {
            var supermarket = await _context.Supermarkets
                .Include(s => s.Items)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            return _mapper.Map<GetSupermarketDto>(supermarket);
        }

        public async Task<List<GetSupermarketDto>> GetSupermarkets(CancellationToken cancellationToken)
        {
            var supermarkets = await _context.Supermarkets
                .Include(s => s.Items)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<GetSupermarketDto>>(supermarkets);
        }

        public async Task<GetSupermarketDto?> UpdateSupermarket(UpdateSupermarketDto updatedSupermarket, CancellationToken cancellationToken)
        {
            var supermarket = await _context.Supermarkets
                .Include(s => s.Items)
                .FirstOrDefaultAsync(c => c.Id == updatedSupermarket.Id, cancellationToken);
            if (supermarket == null) return null;

            _mapper.Map(updatedSupermarket, supermarket);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GetSupermarketDto>(supermarket);
        }
    }
}