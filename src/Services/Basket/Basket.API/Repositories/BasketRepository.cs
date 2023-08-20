using Basket.API.Entities;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCash;

        public BasketRepository(IDistributedCache redisCash)
        {
            _redisCash = redisCash ?? throw new ArgumentNullException(nameof(redisCash));
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCash.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var Basket =await _redisCash.GetStringAsync(userName);

            if (string.IsNullOrEmpty(Basket)) return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(Basket);

        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCash.SetStringAsync(basket.UserName,JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
