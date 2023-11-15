using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    public void SetText(string text)
    {
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetIcon(Sprite icon)
    {
        transform.Find("Icon").GetComponent<Image>().sprite = icon;
    }
}
