using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebApp.Shared.Models;

namespace BlazorWebApp.Client.Services.Contracts
{
    public interface IPawnApi
    {
        Task<List<Pawn>> GetPawns();
        Task<Pawn> GetPawnById(Guid id);
        Task<List<Pawn>> GetPawnsByUserId(Guid userId);
        Task CreateRandom();
        Task Edit(Pawn pawn);
        Task GetNextRandomSurName(Guid pawnId);
        Task GetNextRandomName(Guid pawnId);
        Task RemovePawn(Guid pawnId);
        Task RemovePawns(List<Pawn> pawns);
    }
}
