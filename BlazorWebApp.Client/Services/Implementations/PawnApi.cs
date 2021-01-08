using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorWebApp.Client.Services.Contracts;
using BlazorWebApp.Shared.Auth;
using BlazorWebApp.Shared.Helpers;
using BlazorWebApp.Shared.Models;

namespace BlazorWebApp.Client.Services.Implementations
{
    public class PawnApi : IPawnApi
    {
        private readonly HttpClient _httpClient;

        public PawnApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Pawn>> GetPawns()
        {
            var res = await _httpClient.GetStringAsync("api/pawns/GetPawns");
            return JsonSerealizeHelper.DeserializeArray<Pawn>(res);
        }
        public async Task<Pawn> GetPawnById(Guid id)
        {
            var res = await _httpClient.GetStringAsync($"api/pawns/GetPawnById?id={id}");
            return JsonSerealizeHelper.Deserialize<Pawn>(res);
        }
        public async Task<List<Pawn>> GetPawnsByUserId(Guid userId)
        {
            var res = await _httpClient.GetStringAsync($"api/pawns/GetPawnsByUserId?userId={userId}");
            return JsonSerealizeHelper.DeserializeArray<Pawn>(res);
        }
        public async Task CreateRandom()
        {
            var result = await _httpClient.PostAsync($"api/pawns/CreateRandom", null);
            result.EnsureSuccessStatusCode();
        }
        public async Task Edit(Pawn pawn)
        {

        }

        public async Task GetNextRandomSurName(Guid pawnId)
        {
            var res = await _httpClient.GetStringAsync($"api/pawns/GetNextRandomSurName?pawnId={pawnId}");
        }

        public async Task GetNextRandomName(Guid pawnId)
        {
            var res = await _httpClient.GetStringAsync($"api/pawns/GetNextRandomName?pawnId={pawnId}");
        }

        public async Task RemovePawn(Guid pawnId)
        {
            var res = await _httpClient.GetStringAsync($"api/pawns/RemovePawn?pawnId={pawnId}");
        }
    }
}
