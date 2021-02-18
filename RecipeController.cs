using CookBookFinal.Data;
using CookBookFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookFinal.Controllers
{
    [Route("/Recipe")]
    public class RecipeController : Controller {
        private readonly IRecipeManager recipeManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipeController(IRecipeManager _recipeManager, UserManager<ApplicationUser> _userManager) {
            recipeManager = _recipeManager;
            userManager = _userManager;
        }

        // GET: RecipeController
        public ActionResult Index() {
            return View();
        }


        // GET: RecipeController/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int id) {
             //Mock Data...
             recipe.Tags.Add(new Tag { Name = "Dinner" });
             recipe.Tags.Add(new Tag { Name = "Yummy" });
             recipe.Tags.Add(new Tag { Name = "Chicken" });
             recipe.Tags.Add(new Tag { Name = "Alfredo" });
             recipe.Tags.Add(new Tag { Name = "Quick-n-Easy" });

            return View("Details", recipeManager.GetRecipeById(id));
        }

        [Authorize]
        // GET: RecipeController/Create
        [Route("Create")]
        public ActionResult Create() {
            return View("Create");
        }

        [Authorize]
        // POST: RecipeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Created")]
        public async Task<ActionResult> Created(/*ApplicationUser user*/) {
            var user = await userManager.GetUserAsync(User);
            var newRecipe = recipeManager.Create(user);

            //Mock Data
            recipeManager.AddIngredient(newRecipe.Id, new Ingredient { Name = "Alfredo Noodles", Quantity = "One Bag", Description = "Make sure they are the long and thick noodles" });
            recipeManager.AddIngredient(newRecipe.Id, new Ingredient { Name = "chicken", Quantity = "One Full Chicken Breast", Description = "However you like them :)" });
            recipeManager.AddIngredient(newRecipe.Id, new Ingredient { Name = "Alfredo Sauce", Quantity = "One can", Description = "They are <this> brand of sauce" });
            recipeManager.AddIngredient(newRecipe.Id, new Ingredient { Name = "Garlic", Quantity = "Small Pinch", Description = "For extra Taste" });
            


            return RedirectToAction("Edit", new { id = newRecipe.Id });
        }

        [Authorize]
        // GET: RecipeController/Edit/5
        [Route("Edit/{id:int?}", Name = "EditSpecific")]
        public ActionResult Edit(int id) {
            var model = new EditRecipeViewModel { Recipe = recipeManager.GetRecipeById(id) };
            return View(model);
        }

        [Authorize]
        // GET: RecipeController/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int id) {
            return View("Delete", recipeManager.GetRecipeById(id));
        }

        [HttpPost]
        [Route("Delete/{id:int}")]
        public ActionResult Deleted(int id) {
            recipeManager.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("SaveChanges")]
        [HttpPost]
        public ActionResult SaveChanges(Recipe recipe) {
            var edited = recipeManager.Edit(recipe);
            return Details(edited.Id);
            //return View("Details", edited);
        }

        //Edit Form Actions
        [Route("Add")]
        public ActionResult AddIngredient([FromQuery] int recipeId, [FromQuery] string name, [FromQuery] string quantity, [FromQuery] string description) {
            if(name == null || quantity == null) {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            recipeManager.AddIngredient(recipeId, new Ingredient { Name = name, Quantity = quantity, Description =  description });
            return StatusCode(StatusCodes.Status200OK);
        }


        [Route("Remove")]
        public ActionResult RemoveIngredient([FromQuery]int ingredientId) {
            recipeManager.RemoveIngredient(ingredientId);
            return StatusCode(StatusCodes.Status202Accepted);
        }
    }
}