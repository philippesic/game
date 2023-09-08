using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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

        public class ItemIDAndCountList
        {
            private List<ItemIDAndCount> itemIDAndCounts = new List<ItemIDAndCount>();

            public ItemIDAndCountList(int id, int count)
            {
                add(id, count);
            }

            public ItemIDAndCountList(){}

            public ItemIDAndCountList add(int id, int count)
            {
                itemIDAndCounts.Add(new ItemIDAndCount(id, count));
                return this;
            }

            public List<ItemIDAndCount> end()
            {
                return itemIDAndCounts;
            }
        }

        public List<ItemIDAndCount> itemsCost;
        public List<ItemIDAndCount> itemsMade;
        public float timeSec;
        public int machineId;

        public Recipe(int machineId, float timeSec, List<ItemIDAndCount> itemsCost, List<ItemIDAndCount> itemsMade)
        {
            this.machineId = machineId;
            this.timeSec = timeSec;
            this.itemsCost = itemsCost;
            this.itemsMade = itemsMade;
        }
    }

    public static Dictionary<int, string> names = new Dictionary<int, string>();
    public static Dictionary<int, string> descriptions = new Dictionary<int, string>();
    public static Dictionary<int, Sprite> icons = new Dictionary<int, Sprite>();
    public static List<Recipe> recipes = new List<Recipe>();

    static AllItemData()
    {
        add(0, "Trash", "t");
        add(1, "Raw Iron", "This is iron before it is smelted.");
        add(1, "Iron", "idk man");

        recipes.Add(new Recipe(
                15,
                10,
                new Recipe.ItemIDAndCountList().end(),
                new Recipe.ItemIDAndCountList(1, 5).end()
            ));
        recipes.Add(new Recipe(
                16,
                10,
                new Recipe.ItemIDAndCountList().end(),
                new Recipe.ItemIDAndCountList(1, 10).end()
            ));
        recipes.Add(new Recipe(
                16,
                10,
                new Recipe.ItemIDAndCountList().end(),
                new Recipe.ItemIDAndCountList(1, 15).end()
            ));
        recipes.Add(new Recipe(
            10,
            5,
            new Recipe.ItemIDAndCountList(1, 15).end(),
            new Recipe.ItemIDAndCountList(12, 15).end()
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
        recipes.Add(recipe);
    }
}


