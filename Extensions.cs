using Catalog.Dtos;
using Catalog.Entities;
using System.Collections.Generic;

namespace Catalog
{
    public static class Extensions
    {
        public static ItemDto asDto(this Item item) {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }

        public static IEnumerable<ItemDto> asDtoList(this IEnumerable<Item> items) {
            return items.Select(item => item.asDto());
        }
    }
}