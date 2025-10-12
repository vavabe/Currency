using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Application.Models;
using CurrencyServices.CurrencyApp.RestApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CurrencyServices.CurrencyApp.RestApi.Controllers
{
    [ApiController]
    [Route("api/currency")]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavoriteCurrencies(CancellationToken token)
        {
            var userId = JwtUserIdExtractor.ExtractUserId(HttpContext);
            if (userId is not null)
            {
                var favorites = await _currencyService.GetFavoritesByUser(userId.GetValueOrDefault());
                return Ok(favorites);
            }
            return BadRequest();
        }

        [HttpPost("favorites")]
        public async Task<IActionResult> AddToFavorite(FavoriteDto favoriteDto, CancellationToken token)
        {
            var userId = JwtUserIdExtractor.ExtractUserId(HttpContext);
            if (userId is not null)
            {
                var favorites = await _currencyService.AddToFavorite(favoriteDto.CurrencyId, userId.GetValueOrDefault());
                return Ok(favorites);
            }
            return BadRequest();
        }
    }
}
