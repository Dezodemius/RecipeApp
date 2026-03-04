using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeManagerBot.Data;
using RecipeManagerBot.Models;

namespace RecipeManagerBot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly AppDbContext _db;

    public RecipesController(AppDbContext db)
    {
        _db = db;
    }

    // 1. Получить все рецепты пользователя
    [HttpGet("user/{telegramId}")]
    public async Task<IActionResult> GetMyRecipes(long telegramId)
    {
        var recipes = await _db.Recipes
            .Where(r => r.OwnerId == telegramId)
            .Include(r => r.Ingredients)
            .Include(r => r.Steps)
            .ThenInclude(s => s.Images)
            .ToListAsync();
            
        return Ok(recipes);
    }

    // 2. Добавить новый рецепт
    [HttpPost]
    public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
    {
        if (recipe == null) return BadRequest();

        _db.Recipes.Add(recipe);
        await _db.SaveChangesAsync();

        return Ok(new { message = "Рецепт успешно сохранен!", recipeId = recipe.Id });
    }

    // 3. Получить конкретный рецепт (для шаринга)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipe(int id)
    {
        var recipe = await _db.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Steps)
            .ThenInclude(s => s.Images)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe == null) return NotFound();
        
        return Ok(recipe);
    }

    // 4. Удалить рецепт
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        var recipe = await _db.Recipes.FindAsync(id);
        if (recipe == null) return NotFound();

        _db.Recipes.Remove(recipe);
        await _db.SaveChangesAsync();
        return Ok();
    }
}