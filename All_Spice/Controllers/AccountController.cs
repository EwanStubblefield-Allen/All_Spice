namespace All_Spice.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly RecipesService _recipesService;
  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth0Provider;

  public AccountController(AccountService accountService, RecipesService recipesService, FavoritesService favoritesService, Auth0Provider auth0Provider)
  {
    _accountService = accountService;
    _recipesService = recipesService;
    _favoritesService = favoritesService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpGet("recipes")]
  public async Task<ActionResult<List<Recipe>>> GetRecipesByAccountId()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Recipe> recipes = _recipesService.GetRecipesByAccountId(userInfo.Id);
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpGet("favorites")]
  public async Task<ActionResult<List<RecipeFav>>> GetFavoritesByAccountId()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<RecipeFav> favorites = _favoritesService.GetFavoritesByAccountId(userInfo.Id);
      return Ok(favorites);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
