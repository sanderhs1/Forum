using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;

namespace Forum.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Table()
        {
            var items = new List<Item>();

            var item1 = new Item
            {
                ItemId = 1,
                Name = "Name",
                Price = 1000
            };

            var item2 = new Item
            {
                ItemId = 2,
                Name = "Name",
                Price = 2000
            };

            items.Add(item1);
            items.Add(item2);

            ViewBag.CurrentViewName = "List of items";
            return View(items);
        }
    }
}