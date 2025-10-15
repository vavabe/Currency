using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Application.Models;
using CurrencyServices.CurrencyApp.RestApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyServices.CurrencyApp.RestApi.Controllers;

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
        var userId = HttpContext.ExtractUserId();
        if (userId is null)
            return BadRequest();

        var favorites = await _currencyService.GetFavoritesByUser(userId.GetValueOrDefault());
        return Ok(favorites);
    }

    [HttpPost("favorites")]
    public async Task<IActionResult> AddToFavorite(FavoriteDto favoriteDto, CancellationToken token)
    {
        var userId = HttpContext.ExtractUserId();
        if (userId is not null)
        {
            var favorites = await _currencyService.AddToFavorite(favoriteDto.CurrencyId, userId.GetValueOrDefault());
            return Ok(favorites);
        }
        return BadRequest();
    }
}
