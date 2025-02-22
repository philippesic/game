using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class BlockMenu : UIToggle
{
    public Transform inputGrid;
    public Transform outputGrid;
    public Transform recipeGrid;
    private InventoryContainingFactory viewedFactory;


    protected override void Open()
    {
        WorldBlock block = PlayerRayCaster.instance.GetLookedAtWorldBlock();
        if (block != null)
        {
            InventoryContainingFactory inventoryFactory = block.GetBlockFromType<InventoryContainingFactory>();
            if (inventoryFactory != null)
            {
                viewedFactory = inventoryFactory;
                ItemProssesingFactory prossesingFactory = viewedFactory.GetBlockFromType<ItemProssesingFactory>();
                if (prossesingFactory != null)
                {
                    SetGridItems(prossesingFactory.input.inventoryItems, inputGrid);
                    foreach (UnityEngine.UI.Button button in outputGrid.GetComponentsInChildren<UnityEngine.UI.Button>())
                    {
                        button.onClick.AddListener(delegate
                        {
                            ItemContainer clickedItems = null;
                            prossesingFactory.input.Remove(clickedItems);
                            Player.instance.inv.Add(clickedItems);
                        });
                    }
                    SetGridItems(prossesingFactory.output.inventoryItems, outputGrid);
                    foreach (UnityEngine.UI.Button button in outputGrid.GetComponentsInChildren<UnityEngine.UI.Button>())
                    {
                        button.onClick.AddListener(delegate
                        {
                            prossesingFactory.ChangeCurrentRecipe(recipe);
                        });
                    }
                    SetGridRecipes(AllGameData.recipes[prossesingFactory.blockID], recipeGrid);
                    foreach (UnityEngine.UI.Button button in recipeGrid.GetComponentsInChildren<UnityEngine.UI.Button>())
                    {
                        AllGameData.Recipe recipe = AllGameData.recipeNames[button.GetComponentInChildren<TextMeshProUGUI>().text];
                        button.onClick.AddListener(delegate
                        {
                            prossesingFactory.ChangeCurrentRecipe(recipe);
                        });
                    }
                }
                else
                {
                    Drill drill = viewedFactory.GetBlockFromType<Drill>();
                    if (drill != null)
                    {
                        SetGridItems(null, inputGrid);
                        SetGridItems(drill.outputItems.inventoryItems, outputGrid);
                    }
                    SetGridRecipes(null, recipeGrid);
                }
                return;
            }
        }
        SetState();
    }


    protected override void Close()
    {
        viewedFactory = null;
    }

    void Update()
    {
        if (isOpen && viewedFactory != null)
        {
            IngameUI.instance.SetCrosshairText(9);
            ItemProssesingFactory prossesingFactory = viewedFactory.GetBlockFromType<ItemProssesingFactory>();
            if (prossesingFactory != null)
            {
                SetGridItems(prossesingFactory.input.inventoryItems, inputGrid);
                SetGridItems(prossesingFactory.output.inventoryItems, outputGrid);

            }
            else
            {
                Drill drill = viewedFactory.GetBlockFromType<Drill>();
                if (drill != null)
                {
                    SetGridItems(null, inputGrid);
                    SetGridItems(drill.outputItems.inventoryItems, outputGrid);
                }
            }
            SetProgressBar(viewedFactory.GetProssesing0To1());
        }
    }

    public override void UISleepUpdate()
    {
        WorldBlock block = PlayerRayCaster.instance.GetLookedAtWorldBlock();
        if (block != null)
        {
            InventoryContainingFactory itemProssesingFactory = block.GetBlockFromType<InventoryContainingFactory>();
            if (itemProssesingFactory != null)
            {
                IngameUI.instance.SetCrosshairText(9, "Press 'E' To Open Block Menu");
            }
            else
            {
                IngameUI.instance.SetCrosshairText(9);
            }
        }
        else
        {
            IngameUI.instance.SetCrosshairText(9);
        }
    }

    private void SetProgressBar(float progress0To1)
    {
        foreach (var comp in GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (comp.gameObject.name == "ProgressBarText")
            {
                comp.text = progress0To1 == -1 ? "" : (progress0To1 * 100 + "%");
                return;
            }
        }

    }
}
