using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AllGameDate : ScriptableObject
{

    static AllGameDate()
    {
        add(0, "Trash", "t");
        add(1, "Raw Iron", "This is iron before it is smelted.");
        add(12, "Iron", "idk man");

        recipes.Add(new Recipe(
                15,
                10,
                new ItemIDAndCountList().end(),
                new ItemIDAndCountList(1, 5).end()
            ));
        recipes.Add(new Recipe(
                16,
                10,
                new ItemIDAndCountList().end(),
                new ItemIDAndCountList(1, 10).end()
            ));
        recipes.Add(new Recipe(
                17,
                10,
                new ItemIDAndCountList().end(),
                new ItemIDAndCountList(1, 15).end()
            ));
        recipes.Add(new Recipe(
                10,
                5,
                new ItemIDAndCountList(1, 15).end(),
                new ItemIDAndCountList(12, 15).end()
            ));

        FactoryPlacementCost.Add(0, new ItemIDAndCountList(12, 1).end());
    }

    // internal stuff

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

        public ItemIDAndCountList() { }

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

    public class Recipe
    {
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
    public static Dictionary<int, List<ItemIDAndCount>> FactoryPlacementCost = new Dictionary<int, List<ItemIDAndCount>>();

    static void add(int id, string name, string description)
    {
        names.Add(id, name);
        descriptions.Add(id, description);
        icons.Add(id, Resources.Load<Sprite>("Items/Icons/" + names[id]));
    }
}


