using UnityEngine;

public class WrongHouseTrigger : MonoBehaviour
{
    public enum BuildingType { WrongHouse, GardenShop }
    public BuildingType buildingType;

    public GameObject wrongHousePanel;
    public GameObject gardenShopPanel;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (buildingType == BuildingType.WrongHouse && wrongHousePanel != null)
            {
                wrongHousePanel.SetActive(true);
                FreezeGame();
            }
            else if (buildingType == BuildingType.GardenShop && gardenShopPanel != null)
            {
                gardenShopPanel.SetActive(true);
                FreezeGame();
            }
        }
    }

    void FreezeGame()
    {
        Time.timeScale = 0f;
        if (playerMovement != null)
            playerMovement.canMove = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (playerMovement != null)
            playerMovement.canMove = true;

        if (wrongHousePanel != null) wrongHousePanel.SetActive(false);
        if (gardenShopPanel != null) gardenShopPanel.SetActive(false);
    }
}
