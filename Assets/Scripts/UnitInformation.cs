using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInformation : MonoBehaviour
{
    #region Variables
    
    public float Health { get; set; }
    [Header("ID's")]
    public int _shipType;
    public string _team;
    [Header("Health")]
    static float _SmallHealth = 2;
    static float _MediumHealth = 5;
    static float _BigHealth = 10;
    float maxHealth;
    float health;
    #endregion
    void Start()
    {
        if (_team != null && _shipType > 0)
        {
            //Sets unit health based on ship type
            switch (_shipType)
            {
                case 1:
                    maxHealth = _SmallHealth;
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
    }
    private void Update()
    {
        UpdateHealth();
    }
    void UpdateHealth()
    {
        health = Health;
    }
}
