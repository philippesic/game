using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AllGameData : ScriptableObject
{

    static AllGameData()
    {
        // items
        AddItem(0, "Trash", "Its trash");
        AddItem(1, "Raw Iron", "Unrefined iron, awaiting transformation.");
        AddItem(2, "Raw Copper", "Unprocessed copper, in its natural state.");
        AddItem(3, "Coal", "A fundamental source of energy, essential for various processes.");
        AddItem(4, "Uranium", "Radioactive element with unique properties.");
        AddItem(5, "Banana", "A delicious and nutritious fruit.");
        AddItem(6, "Raw Silver", "Silver in its unrefined state, awaiting refinement.");
        AddItem(7, "Raw Gold", "Unprocessed gold, waiting to be refined and utilized.");
        AddItem(8, "Lead", "Dense and malleable lead metal.");
        AddItem(9, "Lithium", "Essential lithium element for various applications.");
        AddItem(10, "Sulfur", "A vital element with diverse industrial applications.");
        AddItem(11, "Diamond", "A precious and exceptionally hard gemstone.");
        AddItem(12, "Processed Iron", "Refined iron, ready for various applications.");
        AddItem(13, "Processed Copper", "Refined copper, suitable for various uses.");
        AddItem(14, "Processed Silver", "Refined silver, ready for crafting and manufacturing.");
        AddItem(15, "Processed Gold", "Refined gold, prepared for various high-value applications.");
        AddItem(16, "Glass", "Transparent material used for various constructions.");
        AddItem(17, "Steel", "Strong and versatile steel material for industrial applications.");
        AddItem(18, "Iron Plate", "Flat iron sheet, commonly used in fabrication.");
        AddItem(19, "Steel Plate", "Sturdy steel sheet, essential for heavy-duty projects.");
        AddItem(20, "Copper Plate", "Flat copper sheet, ideal for various crafting Endeavors.");
        AddItem(21, "Motor", "Core component for machinery and automation systems.");
        AddItem(22, "Silicon", "Crucial element for electronic devices and technology.");
        AddItem(23, "Laser", "High-intensity beam emitter for precision applications.");
        AddItem(24, "Microcontroller", "Miniaturized computing unit for controlling intricate systems.");
        AddItem(25, "Solar Panel", "Device that harnesses sunlight for sustainable energy.");
        AddItem(26, "Fan", "Mechanical device for generating airflow and cooling.");
        AddItem(27, "Magnet", "Magnetic element with various industrial and scientific uses.");
        AddItem(28, "Computer", "Advanced computing system for processing complex tasks.");
        AddItem(29, "High Precision Motor", "Specialized motor for precision-critical applications.");
        AddItem(30, "Fuel", "Essential energy source for various machinery and vehicles.");

        // factories
        AddFactory(0, "1x1x1 block", "Basic 1x1x1 block for building", new ItemIDAndCountList().End());
        AddFactory(1, "1x1x1 Conveyor", "Basic 1x1x1 Conveyor for building", new ItemIDAndCountList().End());
        AddFactory(2, "1x1x1 Factory", "Basic 1x1x1 Factory for building", new ItemIDAndCountList().End());
        AddFactory(3, "Drill", "it dirl", new ItemIDAndCountList().End());
        AddFactory(4, "Claw Factory", "it grab you", new ItemIDAndCountList().End());

        //craft recipe test
        AddRecipe(new Recipe(
                9,
                10,
                new ItemIDAndCountList(0, 5).End(),
                new ItemIDAndCountList(1, 1).End()
        ));
        // recipes
        AddRecipe(new Recipe(
                15,
                40,
                new ItemIDAndCountList().End(),
                new ItemIDAndCountList(1, 5).End()
            ));
        AddRecipe(new Recipe(
                16,
                40,
                new ItemIDAndCountList().End(),
                new ItemIDAndCountList(1, 10).End()
            ));
        AddRecipe(new Recipe(
                17,
                40,
                new ItemIDAndCountList().End(),
                new ItemIDAndCountList(1, 15).End()
            ));
        AddRecipe(new Recipe(
                10,
                20,
                new ItemIDAndCountList(1, 15).End(),
                new ItemIDAndCountList(12, 15).End()
            ));
    }


    // items
    public static Dictionary<int, string> itemNames = new();
    public static Dictionary<int, string> itemDescriptions = new();
    public static Dictionary<int, Sprite> itemIcons = new();
    public static Dictionary<int, GameObject> itemPrefabs = new();

    // recipes
    public static Dictionary<int, List<Recipe>> recipes = new();

    // factories
    public static Dictionary<int, string> factoryNames = new();
    public static Dictionary<int, string> factoryDescriptions = new();
    public static Dictionary<int, Sprite> factoryIcons = new();
    public static Dictionary<int, GameObject> factoryPrefabs = new();
    public static Dictionary<int, List<ItemIDAndCount>> factoryPlacementCosts = new();

    // internal stuff
    static void AddItem(int id, string name, string description)
    {
        itemNames.Add(id, name);
        itemDescriptions.Add(id, description);
        itemIcons.Add(id, Resources.Load<Sprite>("Items/Icons/" + itemNames[id]));
        itemPrefabs.Add(id, Resources.Load<GameObject>("Items/Prefabs/" + itemNames[id]));
    }

    static void AddRecipe(Recipe recipe)
    {
        if (!recipes.ContainsKey(recipe.machineId))
        {
            recipes.Add(recipe.machineId, new List<Recipe>());
        }
        recipes[recipe.machineId].Add(recipe);
    }

    static void AddFactory(int id, string name, string description, List<ItemIDAndCount> placementCost)
    {
        factoryNames.Add(id, name);
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
    }

    public class ItemIDAndCountList
    {
        private List<ItemIDAndCount> itemIDAndCounts = new List<ItemIDAndCount>();

        public ItemIDAndCountList(int id, int count)
        {
            Add(id, count);
        }

        public ItemIDAndCountList() { }

        public ItemIDAndCountList Add(int id, int count)
        {
            itemIDAndCounts.Add(new ItemIDAndCount(id, count));
            return this;
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
        public float timeTicks;
        public int machineId;

        public Recipe(int machineId, float timeTicks, List<ItemIDAndCount> itemsCost, List<ItemIDAndCount> itemsMade)
        {
            this.machineId = machineId;
            this.timeTicks = timeTicks;
            this.itemsCost = itemsCost;
            this.itemsMade = itemsMade;
        }
    }
}


