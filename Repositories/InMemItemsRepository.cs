using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = items.Where(i => i.Id == id).SingleOrDefault();

            if(item != null)
            {
                items.Remove(item);
            }

            await Task.CompletedTask;
        }

        public async Task<Item?> GetItemAsync(Guid id)
        {
            var item = items.Where(i => i.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task UpdateItemAsync(Item item)
        {
            var itemIndex = items.FindIndex(i => i.Id == item.Id);
            items[itemIndex] = item;

            await Task.CompletedTask;
        }
    }
}