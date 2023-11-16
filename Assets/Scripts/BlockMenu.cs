using TMPro;
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
