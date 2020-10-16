using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformation : WorldWorker
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
    MeshFilter meshFilter;
    public Material baseMat;
    public Material selectedMat;
    [Header("Meshes")]
    public GameObject smallMeshHolder;
    public Mesh _smallShip;
    public GameObject[] _smallShipsMeshes;
    public Mesh _mediumShip;
    public Mesh _bigShip;

    #endregion
    void Start()
    {
        #region SetSpecial
        turnController = FindObjectOfType<TurnController>();
        unitCanvasHolder = GameObject.FindGameObjectWithTag("UnitCanvas").GetComponent<UnitCanvasHolder>();
        selectingMaster = GameObject.FindGameObjectWithTag("SelectingMaster");
        selectingUnits = selectingMaster.GetComponent<NewSelectingUnit>();
        #endregion
        fighting = GetComponent<Fighting>();
        meshFilter = GetComponent<MeshFilter>();
        if (_team != null && _shipType > 0)
        {
            SetBasedOnShipType(_shipType);
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
    void SetBasedOnShipType(int type)
    {
        //Sets unit health based on ship type
        switch (_shipType)
        {
            case 1:
                maxHealth = _SmallHealth;
                meshFilter.mesh = null;
                smallMeshHolder.SetActive(true);
                for (int i = 0; i < _smallShipsMeshes.Length; i++)
                {
                    switch (_team)
                    {
                        case "Red":
                            _smallShipsMeshes[i].GetComponent<MeshRenderer>().material.color = Color.red;
                            break;
                        case "Blue":
                            _smallShipsMeshes[i].GetComponent<MeshRenderer>().material.color = Color.blue;
                            break;
                    }
                }
                //meshFilter.mesh = _smallShip;
                break;
            case 2:
                maxHealth = _MediumHealth;
                meshFilter.mesh = _mediumShip;
                break;
            case 3:
                maxHealth = _BigHealth;
                meshFilter.mesh = _bigShip;
                break;
        }
    }
    void MeshMatColorChange()
    {
#if UNITY_EDITOR
        switch (_team)
        {
            case "Red":
                meshRenderer.material.color = Color.red;
                if (_shipType == 1)
                {
                    for (int i = 0; i < _smallShipsMeshes.Length; i++)
                    {
                        _smallShipsMeshes[i].GetComponent<MeshRenderer>().material.color = Color.red;
                    }
                }
                break;
            case "Blue":
                meshRenderer.material.color = Color.blue;
                if (_shipType == 1)
                {
                    for (int i = 0; i < _smallShipsMeshes.Length; i++)
                    {
                        _smallShipsMeshes[i].GetComponent<MeshRenderer>().material.color = Color.blue;
                    }
                }
                break;
        }
#endif
    }
    void Update()
    {
        UpdateHealth();
        DeathCheck();
    }
    void UpdateHealth()
    {
        health = Health;
        if (_shipType == 1)
        {
            int healthInt = Mathf.RoundToInt(health);
            for (int i = 0; i < _smallShipsMeshes.Length; i++)
            {
                _smallShipsMeshes[i].SetActive(false);
            }
            for (int i = 0; i < health; i++)
            {
                _smallShipsMeshes[i].SetActive(true);
            }
        }
    }
    public void SelectUnit()
    {
        if (_shipType == 1)
        {
            for (int i = 0; i < _smallShipsMeshes.Length; i++)
            {
                _smallShipsMeshes[i].GetComponent<MeshRenderer>().material = selectedMat;
            }
        }
        meshRenderer.material = selectedMat;
    }
    public void DeselectUnit()
    {
        if (_shipType == 1)
        {
            for (int i = 0; i < _smallShipsMeshes.Length; i++)
            {
                _smallShipsMeshes[i].GetComponent<MeshRenderer>().material = selectedMat;
            }
        }
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