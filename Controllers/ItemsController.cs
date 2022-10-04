using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;
using Catalog.Dtos;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet] // Declare that this should react to GET calls
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            return (await repository.GetItemsAsync()).asDtoList();
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id) {
            var item = await repository.GetItemAsync(id);
            if (item is null) 
            {
                return NotFound();
            }
            return Ok(item.asDto());
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.asDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if(existingItem == null)
            {
                return NotFound();
            } else
            {
                var item = existingItem with
                {
                    Name = itemDto.Name,
                    Price = itemDto.Price
                };
                await repository.UpdateItemAsync(item);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);

            if(existingItem == null) {
                return NotFound();
            } else
            {
                await repository.DeleteItemAsync(id);
                return NoContent();
            }
        }
    }
}