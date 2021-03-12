using Catstagram.Server.Data;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Cats.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catstagram.Server.Features.Cats
{
    public class CatService : ICatsService
    {
        private readonly CatstagramDbContext _data;

        public CatService(CatstagramDbContext data)
        {
            _data = data;
        }

        public async Task<int> Create(string imageUrl, string description, string userId)
        {
            var cat = new Cat
            {
                ImageUrl = imageUrl,
                Description = description,
                UserId = userId
            };

            _data.Add(cat);
            await this._data.SaveChangesAsync();

            return cat.Id;
        }

        public async Task<IEnumerable<CatListingServiceModel>> ByUser(string userId) 
            => await _data
                .Cats
                .Where(c => c.UserId == userId)
                .Select(c => new CatListingServiceModel
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl
                })
                .ToListAsync();

        public async Task<CatDetailsServiceModel> Details(int id)
        {
            var result = await _data
                .Cats
                .Where(c => c.Id == id)
                .Select(c => new CatDetailsServiceModel
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    ImageUrl = c.ImageUrl,
                    Description = c.Description,
                    UserName = c.User.UserName

                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> Update(int id, string description, string userId)
        {
            var cat = await ByIdAndbyUserId(id, userId);

            if (cat == null)
            {
                return false;
            }

            cat.Description = description;
            await _data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id, string userId)
        {
            var cat = await ByIdAndbyUserId(id, userId);

            if(cat == null)
            {
                return false;
            }

            _data.Cats.Remove(cat);
            await _data.SaveChangesAsync();

            return true;


        }

        private async Task<Cat> ByIdAndbyUserId(int id, string userId)
        {
            var cat = await _data.Cats.Where(c => c.Id == id && c.UserId == userId).FirstOrDefaultAsync();

            return cat;
        }
    }
}
