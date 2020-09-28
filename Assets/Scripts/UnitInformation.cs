using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformation : MonoBehaviour
{
    #region Variables
    public float Health { get; set; }
    public Fighting fighting;
    [Header("ID's")]
    public int _shipType;
    public string _team;
    [Header("Health")]
    static float _SmallHealth = 3;
    static float _MediumHealth = 5;
    static float _BigHealth = 10;
    float maxHealth;
    public float health;
    [Header("Selection")]
    MeshRenderer meshRenderer;
    public Material baseMat;
    public Material selectedMat;
    [Header("UI")]
    public Text healthText;
    [Header("")]
    #endregion
    void Start()
    {
        fighting = GetComponent<Fighting>();
        if (_team != null && _shipType > 0)
        {
            //Sets unit health based on ship type
            switch (_shipType)
            {
                case 1:
                    maxHealth = _SmallHealth;
                    GameObject smallMeshHolder = GameObject.FindGameObjectWithTag("SmallMeshHolder");
                    break;
                case 2:
                    maxHealth = _MediumHealth;
                    break;
                case 3:
                    maxHealth = _BigHealth;
                    break;
            }
            health = maxHealth;
        }
        if (health == 0)
        {
            Debug.LogError(health);
        }
        Health = health;
        meshRenderer = GetComponent<MeshRenderer>();
        MeshMatColorChange();
    }
    void MeshMatColorChange()
    {
#if UNITY_EDITOR

        switch (_team)
        {
            case "Red":
                meshRenderer.material.color = Color.red;
                break;
            case "Blue":
                meshRenderer.material.color = Color.blue;
                break;

        }
#endif
    }
    private void Update()
    {
        UpdateHealth();
        DeathCheck();
    }
    void UpdateHealth()
    {
        health = Health;
        healthText.text = health.ToString();
    }
    public void SelectUnit()
    {
        meshRenderer.material = selectedMat;
    }
    public void DeselectUnit()
    {
        meshRenderer.material = baseMat;
        MeshMatColorChange();
    }
    void DeathCheck()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
