using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{

    [HideInInspector] public int maxHealth;
    public float currentHealth;
    public float healthLossCoeff;
    public bool allowRequestTools;
    public bool decreaseHealthOverTime = false;

    //public Image healthBar;
    private PlantStageController stageController;
    public Animator requestAnim;


    private void Awake()
    {
        stageController = GetComponent<PlantStageController>();
        requestAnim = GetComponent<Animator>();
        currentHealth = maxHealth;
        Debug.Log(decreaseHealthOverTime);
    }

    private void Start()
    {
        Debug.Log(decreaseHealthOverTime);
    }

    private void LateUpdate()
    {
        Debug.Log(decreaseHealthOverTime);
        if (decreaseHealthOverTime) {
            currentHealth -= Time.deltaTime * healthLossCoeff;
            float healthPercentage = (1f - (currentHealth / maxHealth));
            stageController.currentStage.sprite.color = Color.HSVToRGB(0, healthPercentage, 1);
        }

        if (currentHealth <= 0)
        {
            //TODO: DESTROY?
            GameManager.totalPlants--;
            Destroy(this.gameObject);
        }

    }


    public void ResetHealth()
    {
        currentHealth = maxHealth;
        float healthPercentage = (1f - (currentHealth / maxHealth));
        stageController.currentStage.sprite.color = Color.HSVToRGB(0, healthPercentage, 1);
    }

    public void SetMaxHealth(float max)
    {
        maxHealth = (int)max;
        ResetHealth();
    }

    public void InteractWith()
    {
        return;
    }


}
