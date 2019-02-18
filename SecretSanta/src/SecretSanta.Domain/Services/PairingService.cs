using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services
{
    public class PairingService : IPairingService
    {
        private ApplicationDbContext DbContext { get; }
        private Random Random { get; } = new Random();
        private readonly object _LockKey = new object();

        public PairingService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> GeneratePairings(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(x => x.GroupUsers)
                .FirstOrDefaultAsync(x => x.Id == groupId);

            List<int> userIds = group?.GroupUsers?.Select(x => x.UserId).ToList();
            if (userIds == null || userIds.Count < 2)
            {
                return false;
            }

            Task<List<Pairing>> task = Task.Run(() => GetPairings(userIds));
            List<Pairing> myPairings = await task;

            await DbContext.Pairings.AddRangeAsync(myPairings);
            await DbContext.SaveChangesAsync();

            return true;
        }

        private List<Pairing> GetPairings(List<int> userIds)
        {
            int index;

            //Random random = new Random();
            //index = random.Next(userIds.Count);

            lock(_LockKey)
            {
                index = Random.Next(userIds.Count);
            }

            List<Pairing> pairings = new List<Pairing>();

            for (int i = 0; i < userIds.Count - 1; i++)
            {
                Pairing pairing = new Pairing
                {
                    SantaId = userIds[i],
                    RecipientId = userIds[i + 1]
                };
                pairings.Add(pairing);
            }
            Pairing lastPairing = new Pairing
            {
                SantaId = userIds.Last(),
                RecipientId = userIds.First()
            };
            pairings.Add(lastPairing);

            return pairings;
        }
    }
}
