using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class crafting : MonoBehaviour
{
    public GameObject uiPanel; 
    public KeyCode interactionKey = KeyCode.E; 
    private bool inTriggerArea = false; 


    private void start() {
        uiPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerArea = false;
            uiPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (inTriggerArea && Input.GetKeyDown(interactionKey))
        {
            ToggleUIPanel(); 
        }
    }

    private void ToggleUIPanel()
    {
        if (uiPanel.activeSelf)
        {
            Time.timeScale = 1;
            uiPanel.SetActive(false); 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0;
            uiPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void raft() {
        SceneManager.LoadScene("raft", LoadSceneMode.Additive);
    }

    public void boat() {
        SceneManager.LoadScene("boat", LoadSceneMode.Additive);
    }

    public void plane() {
        SceneManager.LoadScene("plane", LoadSceneMode.Additive);
    }

    public void teleporter() {
        SceneManager.LoadScene("teleporter", LoadSceneMode.Additive);
    }

    public void spaceship() {
        SceneManager.LoadScene("spaceship", LoadSceneMode.Additive);
    }
}
