using UnityEngine;
using UnityEngine.UI;


public class PlantCollector : MonoBehaviour
{
    public GameObject tomatoPanel;
    public GameObject cactusPanel;
    public GameObject coleusPanel;
    public GameObject cucumberPanel;

    public GameObject plantCanvas;
    public PlayerMovement playerMovement;

    public Text plantCountText;

    private int plantsCollected = 0;
    private int totalPlants = 4;

    void Start()
    {
        plantCanvas.SetActive(false);
        DisableAllPanels();
        UpdatePlantText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tomato"))
        {
            ShowPanel(tomatoPanel);
            Destroy(other.gameObject);
            IncrementPlantCount();
        }
        else if (other.CompareTag("Cactus"))
        {
            ShowPanel(cactusPanel);
            Destroy(other.gameObject);
            IncrementPlantCount();
        }
        else if (other.CompareTag("Coleus"))
        {
            ShowPanel(coleusPanel);
            Destroy(other.gameObject);
            IncrementPlantCount();
        }
        else if (other.CompareTag("Cucumber"))
        {
            ShowPanel(cucumberPanel);
            Destroy(other.gameObject);
            IncrementPlantCount();
        }
    }

    void ShowPanel(GameObject panel)
    {
        plantCanvas.SetActive(true);
        DisableAllPanels();
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClosePlantPanel()
    {
        plantCanvas.SetActive(false);
        DisableAllPanels();
        Time.timeScale = 1f;
    }

    void DisableAllPanels()
    {
        tomatoPanel.SetActive(false);
        cactusPanel.SetActive(false);
        coleusPanel.SetActive(false);
        cucumberPanel.SetActive(false);
    }

    void IncrementPlantCount()
    {
        plantsCollected++;
        UpdatePlantText();
    }

    void UpdatePlantText()
    {
        if (plantCountText != null)
            plantCountText.text = $"Plants Collected: {plantsCollected}/{totalPlants}";
    }
}
