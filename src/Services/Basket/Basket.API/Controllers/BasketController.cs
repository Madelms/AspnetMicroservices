using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class BasketController : Controller
    {
      public readonly   IBasketRepository _BasketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _BasketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        [HttpGet("{UserName}", Name = "GetBasket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string UserName)
        {
            var Basket= await _BasketRepository.GetBasket(UserName);
            return Ok(Basket?? new ShoppingCart(UserName));
        }


        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody]ShoppingCart Basket)
        {
            return Ok(await _BasketRepository.UpdateBasket(Basket));
        }



        [HttpDelete("{UserName}", Name = "DeleteBasket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string UserName)
        {
            await _BasketRepository.DeleteBasket(UserName);
            return Ok();
        }
    }
}
