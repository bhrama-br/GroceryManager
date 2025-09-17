using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Revenue;

namespace GroceryManager.Services.Revenues
{
    public interface IRevenuesService
    {
        Task<List<GetRevenuesDto>> GetAllRevenuesApiAsync();
    }

    public class RevenuesService : IRevenuesService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public RevenuesService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<List<GetRevenuesDto>> GetAllRevenuesApiAsync()
        {
            var allRevenues = new List<GetRevenuesDto>();

            int page = 1;
            int limit = 50;
            ApiRevenueResponseDto? apiResponse;

            do
            {
                var url = $"https://api-receitas-pi.vercel.app/receitas/todas?page={page}&limit={limit}";

                using var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                apiResponse = JsonSerializer.Deserialize<ApiRevenueResponseDto>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (apiResponse?.Items != null)
                {
                    var dtos = _mapper.Map<List<GetRevenuesDto>>(apiResponse.Items);
                    allRevenues.AddRange(dtos);
                }

                page++;
            }
            while (apiResponse?.Meta != null && page <= apiResponse.Meta.TotalPages);

            return allRevenues;
        }
    }
}