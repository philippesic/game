using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AllGameData : ScriptableObject
{

    static AllGameData()
    {
        // ---- Items ----
        // Main
        AddItem(1, "Raw Iron", "Unrefined iron, awaiting transformation.");
        AddItem(2, "Raw Copper", "Unprocessed copper, in its natural state.");
        AddItem(3, "Coal", "A fundamental source of energy, essential for various processes.");
        AddItem(4, "Uranium", "Radioactive element with unique properties.");
        AddItem(5, "Banana", "A delicious and nutritious fruit.");
        AddItem(6, "Raw Gold", "Unprocessed gold, waiting to be refined and utilized.");
        AddItem(7, "Sulfur", "A vital element with diverse industrial applications.");
        AddItem(8, "Diamond", "A precious and exceptionally hard gemstone.");
        // Basic
        AddItem(10, "Iron", "Iron, ready for various applications.");
        AddItem(11, "Copper", "Copper, suitable for various uses.");
        AddItem(12, "Gold", "Gold, prepared for various high-value applications.");
        AddItem(13, "Steel", "Strong and versatile steel material for industrial applications.");
        AddItem(14, "Iron Rod", "Core component for machinery and automation systems.");
        AddItem(15, "Iron Plate", "Flat iron sheet, commonly used in fabrication.");
        AddItem(16, "Steel Plate", "Sturdy steel sheet, essential for heavy-duty projects.");
        AddItem(17, "Steel Frame", "Steel Frame, ideal for various crafting Endeavors.");
        AddItem(28, "Copper Wire", "Copeer wire, ideal for various crafting Endeavors.");
        AddItem(29, "Wire Coil", "Wire Coil, ideal for various crafting Endeavors.");
        AddItem(20, "Steel Casing", "Steel Casing, ideal for various crafting Endeavors.");
        AddItem(21, "Propeller", "Propeller, ideal for various crafting Endeavors.");
        // Advanced
        AddItem(30, "Gold Flake", "Gold Flake, ideal for various crafting Endeavors.");
        AddItem(31, "Diamond Dust", "Core component for machinery and automation systems.");
        AddItem(32, "Diamond Glass", "Transparent material used for various constructions.");
        AddItem(33, "Motor", "Core component for machinery and automation systems.");
        AddItem(34, "Microcontroller", "Miniaturized computing unit for controlling intricate systems.");
        AddItem(35, "Fan", "Mechanical device for generating airflow and cooling.");
        AddItem(36, "Fiber Optics", "Super fast wire");
        AddItem(37, "Magnet", "Magnetic element with various industrial and scientific uses.");
        // Uber Advanced
        AddItem(40, "Computer", "Advanced computing system for processing complex tasks.");
        AddItem(41, "High Precision Motor", "Specialized motor for precision-critical applications.");
        AddItem(42, "Laser", "High-intensity beam emitter for precision applications.");

        // ---- factories ----
        // Non Factory Blocks
        AddFactory(0, "1x1x1 block", "Basic 1x1x1 block for building", new ItemList("Iron Plate", 6).End());
        // Conveyors
        AddFactory(1, "1x1x1 Conveyor", "Basic 1x1x1 Conveyor for moving your stupi items", new ItemList().End());
        AddFactory(4, "Claw Factory", "it grab you", new ItemList().End());
        // Power
        AddFactory(10, "Solar Array T1", "Sun power!", new ItemList().End());
        AddFactory(11, "Solar Array T2", "Sun power!^2", new ItemList().End());
        AddFactory(12, "Wind Turbine T1", "Wind power!", new ItemList().End());
        AddFactory(13, "Wind Turbine T2", "Wind power!^2", new ItemList().End());
        // Machines
        AddFactory(20, "Smelter", "Basic Smelter for starting your factory", new ItemList().End());
        AddFactory(21, "Electric Smelter", "block", new ItemList().End());
        AddFactory(22, "Caster", "block", new ItemList().End());
        AddFactory(23, "Foundry", "Combines Stuff", new ItemList().End());
        AddFactory(24, "Assembler", "Builds stuff", new ItemList().End());
        AddFactory(25, "Crusher", "Crushing you mom", new ItemList().End());
        AddFactory(26, "Manufacture", "Builds the most complex stuff", new ItemList().End());
        // Drills
        AddFactory(35, "Drill T1", "it dirl", new ItemList().End());
        AddFactory(36, "Drill T2", "it dirl", new ItemList().End());
        AddFactory(37, "Drill T3", "it dirl", new ItemList().End());

        // ---- Recipes ----
        // Smelter
        AddRecipe(new Recipe("Coal Smelted Iron", "Smelter", 10, new ItemList("Raw Iron", 5).Add("Coal", 1).End(), new ItemList("Iron", 5).End()));
        AddRecipe(new Recipe("Coal Smelted Copper", "Smelter", 10, new ItemList("Raw Copper", 5).Add("Coal", 1).End(), new ItemList("Copper", 5).End()));
        AddRecipe(new Recipe("Coal Smelted Gold", "Smelter", 10, new ItemList("Raw Gold", 5).Add("Coal", 1).End(), new ItemList("Gold", 5).End()));
        // Electric Smelter
        AddRecipe(new Recipe("Electric Smelter", 10, new ItemList("Raw Iron", 5).End(), new ItemList("Iron", 5).End()));
        AddRecipe(new Recipe("Electric Smelter", 10, new ItemList("Raw Copper", 5).End(), new ItemList("Copper", 5).End()));
        AddRecipe(new Recipe("Electric Smelter", 10, new ItemList("Raw Gold", 5).End(), new ItemList("Gold", 5).End()));
        // Caster
        AddRecipe(new Recipe("Caster", 20, new ItemList("Iron", 10).End(), new ItemList("Iron Rod", 40).End()));
        AddRecipe(new Recipe("Caster", 20, new ItemList("Iron", 10).End(), new ItemList("Iron Plate", 20).End()));
        AddRecipe(new Recipe("Caster", 20, new ItemList("Steel", 10).End(), new ItemList("Steel Plate", 20).End()));
        AddRecipe(new Recipe("Caster", 20, new ItemList("Steel", 20).End(), new ItemList("Steel Frame", 5).End()));
        AddRecipe(new Recipe("Caster", 20, new ItemList("Copper", 20).End(), new ItemList("Copper Wire", 30).End()));
        // Foundry
        AddRecipe(new Recipe("Foundry", 30, new ItemList("Iron", 10).Add("Coal", 5).End(), new ItemList("Steel", 15).End()));
        AddRecipe(new Recipe("Foundry", 40, new ItemList("Diamond Dust", 30).Add("Gold", 8).End(), new ItemList("Diamond Glass", 4).End()));
        // Crusher
        AddRecipe(new Recipe("Crusher", 40, new ItemList("Diamond", 5).End(), new ItemList("Diamond Dust", 10).End()));
        AddRecipe(new Recipe("Crusher", 20, new ItemList("Gold", 5).End(), new ItemList("Gold Flake", 20).End()));
        // Assembler
        AddRecipe(new Recipe("Assembler", 50, new ItemList("Copper Wire", 20).Add("Iron Rod", 5).End(), new ItemList("Wire Coil", 5).End()));
        AddRecipe(new Recipe("Assembler", 20, new ItemList("Steel Frame", 8).Add("Steel Plate", 24).End(), new ItemList("Steel Casing", 4).End()));
        AddRecipe(new Recipe("Assembler", 100, new ItemList("Wire Coil", 20).Add("Steel Casing", 4).End(), new ItemList("Motor", 1).End()));
        AddRecipe(new Recipe("Assembler", 50, new ItemList("Iron Plate", 50).Add("Iron Rod", 10).End(), new ItemList("Propeller", 10).End()));
        AddRecipe(new Recipe("Assembler", 50, new ItemList("Motor", 20).Add("Propeller", 10).End(), new ItemList("Fan", 5).End()));
        AddRecipe(new Recipe("Assembler", 40, new ItemList("Diamond Glass", 30).Add("Gold Flake", 50).End(), new ItemList("Fiber Optics", 5).End()));
        AddRecipe(new Recipe("Assembler", 150, new ItemList("Steel Casing", 5).Add("Copper Wire", 50).End(), new ItemList("Microcontroller", 1).End()));
        AddRecipe(new Recipe("Assembler", 200, new ItemList("Wire Coil", 20).Add("Steel Frame", 10).End(), new ItemList("Magnet", 5).End()));
        // Manufacture
        AddRecipe(new Recipe("Manufacture", 200, new ItemList("Microcontroller", 20).Add("Fiber Optics", 40).Add("Steel Casing", 10).Add("Fan", 8).End(), new ItemList("Computer", 1).End()));
        AddRecipe(new Recipe("Manufacture", 100, new ItemList("Microcontroller", 10).Add("Magnet", 10).Add("Steel Casing", 4).Add("Motor", 20).End(), new ItemList("High Precision Motor", 1).End()));
        AddRecipe(new Recipe("Manufacture", 200, new ItemList("Magnet", 30).Add("Fiber Optics", 40).Add("Steel Casing", 12).Add("Diamond Glass", 80).End(), new ItemList("Laser", 4).End()));
    }


    // items
    public static Dictionary<int, string> itemNames = new();
    public static Dictionary<string, int> itemIDs = new();
    public static Dictionary<int, string> itemDescriptions = new();
    public static Dictionary<int, Sprite> itemIcons = new();
    public static Dictionary<int, GameObject> itemPrefabs = new();

    // recipes
    public static Dictionary<int, List<Recipe>> recipes = new();
    public static Dictionary<string, Recipe> recipeNames = new();


    // factories
    public static List<int> factoryIDsList = new();
    public static Dictionary<int, string> factoryNames = new();
    public static Dictionary<string, int> factoryIDs = new();
    public static Dictionary<int, string> factoryDescriptions = new();
    public static Dictionary<int, Sprite> factoryIcons = new();
    public static Dictionary<int, GameObject> factoryPrefabs = new();
    public static Dictionary<int, List<ItemIDAndCount>> factoryPlacementCosts = new();

    // internal stuff
    static void AddItem(int id, string name, string description)
    {
        itemNames.Add(id, name);
        itemIDs.Add(name, id);
        itemDescriptions.Add(id, description);
        itemIcons.Add(id, Resources.Load<Sprite>("Items/Icons/" + itemNames[id]));
        itemPrefabs.Add(id, Resources.Load<GameObject>("Items/Prefabs/" + itemNames[id]));
    }

    static void AddRecipe(Recipe recipe)
    {
        if (!recipes.ContainsKey(recipe.factoryID))
        {
            recipes.Add(recipe.factoryID, new List<Recipe>());
        }
        recipes[recipe.factoryID].Add(recipe);
        recipeNames.Add(recipe.name, recipe);
    }

    static void AddFactory(int id, string name, string description, List<ItemIDAndCount> placementCost)
    {
        factoryIDsList.Add(id);
        factoryNames.Add(id, name);
        factoryIDs.Add(name, id);
        factoryDescriptions.Add(id, description);
        factoryIcons.Add(id, Resources.Load<Sprite>("WorldBlocks/Icons/" + factoryNames[id]));
        factoryPrefabs.Add(id, Resources.Load<GameObject>("WorldBlocks/Prefabs/" + factoryNames[id]));
        factoryPlacementCosts.Add(id, placementCost);
    }

    // data types
    public struct ItemIDAndCount
    {
        public int id;
        public int count;

        public ItemIDAndCount(int id, int count)
        {
            this.id = id;
            this.count = count;
        }

        public ItemIDAndCount(string name, int count)
        {
            id = itemIDs[name];
            this.count = count;
        }
    }

    public class ItemList
    {
        private readonly List<ItemIDAndCount> itemIDAndCounts = new();

        public ItemList(int id, int count)
        {
            Add(id, count);
        }

        public ItemList(string name, int count)
        {
            Add(name, count);
        }

        public ItemList() { }

        public ItemList Add(int id, int count)
        {
            itemIDAndCounts.Add(new ItemIDAndCount(id, count));
            return this;
        }

        public ItemList Add(string name, int count)
        {
            return Add(itemIDs[name], count);
        }

        public List<ItemIDAndCount> End()
        {
            return itemIDAndCounts;
        }
    }

    public class Recipe
    {
        public List<ItemIDAndCount> itemsCost;
        public List<ItemIDAndCount> itemsMade;
        public string name;
        public float ticks;
        public int factoryID;

        public Recipe(string recipeName, int factoryID, float timeTicks, List<ItemIDAndCount> itemsCost, List<ItemIDAndCount> itemsMade)
        {
            this.factoryID = factoryID;
            name = recipeName;
            ticks = timeTicks;
            this.itemsCost = itemsCost;
            this.itemsMade = itemsMade;
        }

        public Recipe(string recipeName, string factoryName, float timeTicks, List<ItemIDAndCount> itemsCost, List<ItemIDAndCount> itemsMade)
        {
            factoryID = factoryIDs[factoryName];
            name = recipeName;
            ticks = timeTicks;
            this.itemsCost = itemsCost;
            this.itemsMade = itemsMade;
        }

        public Recipe(int factoryID, float timeTicks, List<ItemIDAndCount> itemsCost, List<ItemIDAndCount> itemsMade)
        {
            this.factoryID = factoryID;
            name = itemNames[itemsMade[0].id];
            ticks = timeTicks;
            this.itemsCost = itemsCost;
            this.itemsMade = itemsMade;
        }

        public Recipe(string factoryName, float timeTicks, List<ItemIDAndCount> itemsCost, List<ItemIDAndCount> itemsMade)
        {
            factoryID = factoryIDs[factoryName];
            name = itemNames[itemsMade[0].id];
            ticks = timeTicks;
            this.itemsCost = itemsCost;
            this.itemsMade = itemsMade;
        }
    }
}


