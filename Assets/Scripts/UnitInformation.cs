using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInformation : MonoBehaviour
{
    #region Variables
    [Header("ID's")]
    public int _shipType;
    public string team;
    [Header("Health")]
    static float _smallHealth = 2;
    static float _mediumHealth = 5;
    static float _bigHealth = 10;
    float _maxHealth;
    float _health;
    #endregion
    void Start()
    {
        if (team != null && _shipType < 0)
        {
            //Sets unit health based on ship type
            switch (_shipType)
            {
                case 1:
                    _maxHealth = _smallHealth;
                    break;
                case 2:
                    _maxHealth = _mediumHealth;
                    break;
                case 3:
                    _maxHealth = _bigHealth;
                    break;
            }
            _health = _maxHealth;
        }
        if (_health == 0)
        {
            Debug.LogError(_health);
        }
    }
}
