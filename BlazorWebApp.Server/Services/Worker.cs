using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorWebApp.Server.Data;
using BlazorWebApp.Shared.Auth;
using BlazorWebApp.Shared.Helpers;
using BlazorWebApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWebApp.Server.Services
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ApplicationDbContext _context;
        private readonly Random random;
        private readonly PawnGeneratorService _pawnGenerator;
        private readonly ApplicationUser world;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            random = new Random();
            var scope = serviceScopeFactory.CreateScope();
            _context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            _pawnGenerator = scope.ServiceProvider.GetService<PawnGeneratorService>();
            world = _context.Users.Where(x => x.NormalizedUserName == "WORLD").FirstOrDefault();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var jobs = await _context.WorkerJobs.Include(x => x.Pawn).ToListAsync();
                    _logger.LogTrace($"Worker running at: {DateTimeOffset.Now.ToString("G")} jobs found {jobs.Count}");
                    foreach (var job in jobs)
                    {
                        switch (job.WorkerJobType)
                        {
                            case WorkerJobType.CreateParents:
                                await CreateParents(job);
                                break;
                            case WorkerJobType.CreateOthenRelations:
                                await CreateOthenRelations(job);
                                break;
                                break;
                        }
                        _context.Remove(job);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                   _logger.LogError($"Worker Error {e.Message}");
                   if (e.InnerException != null)
                   {
                       _logger.LogError($"Worker Error InnerException {e.InnerException.Message}");
                   }
                }
                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task CreateParents(WorkerJob job)
        {
            var pawn = job.Pawn;
            var fathertype = RandomHelper.GetFatherType(random);
            var mothertype = RandomHelper.GetMotherType(random);
            var father = await _pawnGenerator.CreateFather(pawn, fathertype);
            var mother = await _pawnGenerator.CreateMother(pawn, mothertype);

            father.User = world;
            father.UserId = world.Id;
            mother.User = world;
            mother.UserId = world.Id;
            Pawn biologicalfather = null;
            Pawn biologicalmother = null;
            if (fathertype == RelationType.FosterFather)
            {
                biologicalfather = await _pawnGenerator.CreateFather(pawn, RelationType.BiologicalFather);
                father = await _pawnGenerator.CreatePawnName(father);
                await _context.Pawns.AddAsync(biologicalfather);
                await _context.SaveChangesAsync();
            }
            if (mothertype == RelationType.FosterMother)
            {
                biologicalmother = await _pawnGenerator.CreateMother(pawn, RelationType.BiologicalMother);
                await _context.Pawns.AddAsync(biologicalmother);
                await _context.SaveChangesAsync();
            }
            await _context.Pawns.AddAsync(father);
            await _context.Pawns.AddAsync(mother);
            await _context.SaveChangesAsync();

            await _context.Relations.AddRangeAsync(new List<Relation>()
            {
                new Relation(){PawnId = pawn.Id, RelationPawnId = father.Id, RelationType = fathertype},
                new Relation(){PawnId = pawn.Id, RelationPawnId = mother.Id, RelationType = mothertype},
                new Relation(){PawnId = father.Id, RelationPawnId = pawn.Id, RelationType = RelationType.Child},
                new Relation(){PawnId = mother.Id, RelationPawnId = pawn.Id, RelationType = RelationType.Child},
                new Relation(){PawnId = father.Id, RelationPawnId = mother.Id, RelationType = RelationType.Spouse},
                new Relation(){PawnId = mother.Id, RelationPawnId = father.Id, RelationType = RelationType.Spouse}
            });
            if (fathertype == RelationType.FosterFather)
            {
                await _context.Relations.AddRangeAsync(new List<Relation>()
                {
                    new Relation(){PawnId = pawn.Id, RelationPawnId = biologicalfather.Id, RelationType = RelationType.BiologicalFather},
                    new Relation(){PawnId = biologicalfather.Id, RelationPawnId = pawn.Id, RelationType = RelationType.Child}
                });
            }

            if (mothertype == RelationType.FosterMother)
            {
                await _context.Relations.AddRangeAsync(new List<Relation>()
                {
                    new Relation(){PawnId = pawn.Id, RelationPawnId = biologicalmother.Id, RelationType = RelationType.BiologicalMother},
                    new Relation(){PawnId = biologicalmother.Id, RelationPawnId = pawn.Id, RelationType = RelationType.Child}
                });
            }

            if (fathertype == RelationType.FosterFather && mothertype == RelationType.FosterMother)
            {
                var sp = RandomHelper.GetSpouseOrEx(random);
                await _context.Relations.AddRangeAsync(new List<Relation>()
                {
                    new Relation(){PawnId = pawn.Id, RelationPawnId = biologicalmother.Id, RelationType = sp},
                    new Relation(){PawnId = biologicalmother.Id, RelationPawnId = pawn.Id, RelationType = sp}
                });
            }
            await _context.SaveChangesAsync();
            
        }
        private async Task CreateOthenRelations(WorkerJob job)
        {
            
        }
    }
}
