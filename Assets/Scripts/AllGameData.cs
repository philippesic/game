using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AllGameData : ScriptableObject
{

    static AllGameData()
    {
        // items
        addItem(1, "Raw Iron", "Unrefined iron, awaiting transformation.");
        addItem(2, "Raw Copper", "Unprocessed copper, in its natural state.");
        addItem(3, "Coal", "A fundamental source of energy, essential for various processes.");
        addItem(4, "Uranium", "Radioactive element with unique properties.");
        addItem(5, "Banana", "A delicious and nutritious fruit.");
        addItem(6, "Raw Silver", "Silver in its unrefined state, awaiting refinement.");
        addItem(7, "Raw Gold", "Unprocessed gold, waiting to be refined and utilized.");
        addItem(8, "Lead", "Dense and malleable lead metal.");
        addItem(9, "Lithium", "Essential lithium element for various applications.");
        addItem(10, "Sulfur", "A vital element with diverse industrial applications.");
        addItem(11, "Diamond", "A precious and exceptionally hard gemstone.");
        addItem(12, "Processed Iron", "Refined iron, ready for various applications.");
        addItem(13, "Processed Copper", "Refined copper, suitable for various uses.");
        addItem(14, "Processed Silver", "Refined silver, ready for crafting and manufacturing.");
        addItem(15, "Processed Gold", "Refined gold, prepared for various high-value applications.");
        addItem(16, "Glass", "Transparent material used for various constructions.");
        addItem(17, "Steel", "Strong and versatile steel material for industrial applications.");
        addItem(18, "Iron Plate", "Flat iron sheet, commonly used in fabrication.");
        addItem(19, "Steel Plate", "Sturdy steel sheet, essential for heavy-duty projects.");
        addItem(20, "Copper Plate", "Flat copper sheet, ideal for various crafting endeavors.");
        addItem(21, "Motor", "Core component for machinery and automation systems.");
        addItem(22, "Silicon", "Crucial element for electronic devices and technology.");
        addItem(23, "Laser", "High-intensity beam emitter for precision applications.");
        addItem(24, "Microcontroller", "Miniaturized computing unit for controlling intricate systems.");
        addItem(25, "Solar Panel", "Device that harnesses sunlight for sustainable energy.");
        addItem(26, "Fan", "Mechanical device for generating airflow and cooling.");
        addItem(27, "Magnet", "Magnetic element with various industrial and scientific uses.");
        addItem(28, "Computer", "Advanced computing system for processing complex tasks.");
        addItem(29, "High Precision Motor", "Specialized motor for precision-critical applications.");
        addItem(30, "Fuel", "Essential energy source for various machinery and vehicles.");

        // factories
        addFactory(0, "1x1x1 block", "Basic 1x1x1 block for building", new ItemIDAndCountList().end());
        addFactory(1, "1x1x1 Conveyor", "Basic 1x1x1 Conveyor for building", new ItemIDAndCountList().end());
        addFactory(2, "1x1x1 Factory", "Basic 1x1x1 Factory for building", new ItemIDAndCountList().end());

        // recipes
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

    public static Dictionary<int, string> itemNames = new Dictionary<int, string>();
    public static Dictionary<int, string> itemDescriptions = new Dictionary<int, string>();
    public static Dictionary<int, Sprite> itemIcons = new Dictionary<int, Sprite>();
    public static Dictionary<int, Sprite> itemPrefabs = new Dictionary<int, Sprite>();

    public static List<Recipe> recipes = new List<Recipe>();

    public static Dictionary<int, string> factoryNames = new Dictionary<int, string>();
    public static Dictionary<int, string> factoryDescriptions = new Dictionary<int, string>();
    public static Dictionary<int, Sprite> factoryIcons = new Dictionary<int, Sprite>();
    public static Dictionary<int, GameObject> factoryPrefabs = new Dictionary<int, GameObject>();
    public static Dictionary<int, List<ItemIDAndCount>> factoryPlacementCosts = new Dictionary<int, List<ItemIDAndCount>>();

    static void addItem(int id, string name, string description)
    {
        itemNames.Add(id, name);
        itemDescriptions.Add(id, description);
        itemIcons.Add(id, Resources.Load<Sprite>("Items/Icons/" + itemNames[id]));
        itemPrefabs.Add(id, Resources.Load<Sprite>("Items/Prefabs/" + itemNames[id]));
    }

    static void addFactory(int id, string name, string description, List<ItemIDAndCount> placementCost)
    {
        factoryNames.Add(id, name);
        factoryDescriptions.Add(id, description);
        factoryIcons.Add(id, Resources.Load<Sprite>("WorldBlocks/Icons/" + factoryNames[id]));
        factoryPrefabs.Add(id, Resources.Load<GameObject>("WorldBlocks/Prefabs/" + factoryNames[id]));
        factoryPlacementCosts.Add(id, placementCost);
    }
}


