using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebApp.Server.Data;
using BlazorWebApp.Server.Services;
using BlazorWebApp.Shared.Auth;
using BlazorWebApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWebApp.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PawnsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PawnGeneratorService _pawnGenerator;
        public PawnsController(ApplicationDbContext context, PawnGeneratorService pawnGenerator, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _pawnGenerator = pawnGenerator;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<Pawn>> GetPawns()
        {
            return await _context.Pawns.ToListAsync();
        }
        [Authorize]
        [HttpGet]
        public async Task<Pawn> GetPawnById(Guid id)
        {
            return await _context.Pawns.FindAsync(id);
        }
        [Authorize]
        [HttpGet]
        public async Task<List<Pawn>> GetPawnsByUserId(Guid userId)
        {
            return await _context.Pawns.Where(x=>x.UserId==userId).ToListAsync();
        }
        [Authorize]
        [HttpPost]
        public async Task CreateRandom()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var world = await _userManager.FindByEmailAsync("world@world.world");
            Pawn pawn = await _pawnGenerator.GenerateRandomPawn();
            pawn.UserId = user.Id;
            pawn.User = user;
            var parents = await _pawnGenerator.GenerateParents(pawn);
            var father = parents.Item1;
            var mother = parents.Item2;
            father.User = world;
            father.UserId = world.Id;
            mother.User = world;
            mother.UserId = world.Id;
            await _context.Pawns.AddAsync(father);
            await _context.Pawns.AddAsync(mother);
            await _context.Pawns.AddAsync(pawn);
            await _context.SaveChangesAsync();
            await _context.Relations.AddRangeAsync(new List<Relation>()
            {
                new Relation(){PawnId = pawn.Id, RelationPawnId = father.Id, RelationType = RelationType.Father},
                new Relation(){PawnId = pawn.Id, RelationPawnId = mother.Id, RelationType = RelationType.Mother},
                new Relation(){PawnId = father.Id, RelationPawnId = pawn.Id, RelationType = RelationType.Child},
                new Relation(){PawnId = mother.Id, RelationPawnId = pawn.Id, RelationType = RelationType.Child},
                new Relation(){PawnId = father.Id, RelationPawnId = mother.Id, RelationType = RelationType.Spouse},
                new Relation(){PawnId = mother.Id, RelationPawnId = father.Id, RelationType = RelationType.Spouse}
            });

            await _context.SaveChangesAsync();
        }
        [Authorize]
        [HttpPut]
        public async Task Edit(Pawn pawn)
        {
            _context.Pawns.Update(pawn);
            await _context.SaveChangesAsync();
        }

        [Authorize]
        [HttpGet]
        public async Task RemovePawn(Guid pawnId)
        {
            var pawn = await _context.Pawns.FindAsync(pawnId);
            _context.Pawns.Remove(pawn);
            await _context.SaveChangesAsync();
        }

        [Authorize]
        [HttpGet]
        public async Task GetNextRandomName(Guid pawnId)
        {
            var pawn = await _context.Pawns.FindAsync(pawnId);
            await _pawnGenerator.LowOldName(pawn.Name);
            pawn = await _pawnGenerator.CreatePawnName(pawn);
            _context.Pawns.Update(pawn);
            await _context.SaveChangesAsync();
       
        }
        [Authorize]
        [HttpGet]
        public async Task GetNextRandomSurName(Guid pawnId)
        {
            var pawn = await _context.Pawns.FindAsync(pawnId);
            await _pawnGenerator.LowOldSurName(pawn.Surname);
            pawn = await _pawnGenerator.CreatePawnSurName(pawn);
            _context.Pawns.Update(pawn);
            await _context.SaveChangesAsync();
        }

    }
}
