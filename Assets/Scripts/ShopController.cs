using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public KeyCode shopKey;
    public GameObject shop;

    void Start()
    {
        shop.SetActive(false);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(shopKey)) {
            ToggleShop();
        }
    }

        private void ToggleShop()
    {
        if (shop.activeSelf)
        {
            Time.timeScale = 1;
            shop.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 1;
            shop.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }
}
