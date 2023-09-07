using System;
using System.Collections.Generic;
using UnityEngine;


public class AllItemData : MonoBehaviour
{
    public class Recipe
    {
        public struct ItemIDAndCount
        {
            public int id;
            public int count;

            public ItemIDAndCount(int id, int count)
            {
                this.id = id;
                this.count = count;
            }
        }

        public List<ItemIDAndCount> itemsCost;
        public List<ItemIDAndCount> itemsMade;
        public int machineId;

        public Recipe(int machineId, List<ItemIDAndCount> itemsCost, List<ItemIDAndCount> itemsMade)
        {
            this.machineId = machineId;
            this.itemsCost = itemsCost;
            this.itemsMade = itemsMade;
        }
    }

    public static Dictionary<int, string> names = new Dictionary<int, string>();
    public static Dictionary<int, string> descriptions = new Dictionary<int, string>();
    public static Dictionary<int, Sprite> icons = new Dictionary<int, Sprite>();
    public static Dictionary<int, Recipe> recipes = new Dictionary<int, Recipe>();

    static AllItemData()
    {
        add(0, "Trash", "t");
        add(1, "Raw Iron", "This is iron before it is smelted.");

        add(1, "Iron", "idk man", new Recipe(
            10,
            new List<Recipe.ItemIDAndCount>(){ new Recipe.ItemIDAndCount(1, 15) },
            new List<Recipe.ItemIDAndCount>(){ new Recipe.ItemIDAndCount(12, 15) }
            ));
    }

    static void add(int id, string name, string description)
    {
        names.Add(id, name);
        descriptions.Add(id, description);
        icons.Add(id, Resources.Load("Assets/Item/Icons" + names[id]) as Sprite);
    }

    static void add(int id, string name, string description, Recipe recipe)
    {
        names.Add(id, name);
        descriptions.Add(id, description);
        icons.Add(id, Resources.Load("Assets/Item/Icons" + names[id]) as Sprite);
        recipes.Add(id, recipe);
    }
}


