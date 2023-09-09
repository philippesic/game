using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    public void setText(string text)
    {
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = text;
    }

    public void setIcon(Sprite icon)
    {
        transform.Find("Icon").GetComponent<Image>().sprite = icon;
    }
}
