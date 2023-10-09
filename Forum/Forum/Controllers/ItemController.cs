using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;


namespace Forum.Controllers;


public class ItemController : Controllers
{
    public IAactionResult Table()
    {
        var items = new List<Item>();
        var item1 = new Item();
        item1.ItemId = 1;
        item1.Name = "Name",
        item1.Price = 1000;

        var item2 = new Item
        {
            item2.ItemId = 2;
            item2.Name = "Name",
            item2.Price = 2000;
        };


        items.Add(item1);
        items.Add(item2);
        
        ViewBag.CurrentViewName = " List of items ";
        return View(items);

    }
}