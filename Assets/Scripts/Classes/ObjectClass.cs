using UnityEngine;
using System;

[Serializable]
public class Item
{
    /// <summary>
    /// nombre del objeto
    /// </summary>
    public string name; 
    [SerializeField] private Sprite sprite; // Imagen del objeto (para selección o HUD)
    private bool used; // indica si el objeto se ha usado o no (relevante para armas y habilidades)

    public Item(string name) //Constructora por nombre
    {
        this.name = name;
        used = false;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    /// <summary>
    /// Getter de used
    /// </summary>
    public bool WasUsed() 
    {
        return used;
    }

    /// <summary>
    /// Cambia used a true
    /// </summary>
    public void Use() 
    {
        used = true;
    }
}

/// <summary>
/// Clase hija de Item (Armas y Habiliades)
/// </summary>
[Serializable]
public class DamageItem : Item 
{
    /// <summary>
    /// Prefab del arma/activador
    /// </summary>
    [SerializeField] private GameObject prefab; 

    public DamageItem(string name) : base(name) //Constructora por nombre
    {
    }

    /// <summary>
    /// Devuelve el
    /// </summary>
    public GameObject GetPrefab() 
    {
        Use();
        return prefab;
    }
    
}

/// <summary>
/// Clase hija de Item (Potenciadores)
/// </summary>
[Serializable]
public class PowerUpItem : Item
{
    [SerializeField] private PowerUp Powerup1; //Primera estadística que sube
    [SerializeField] private PowerUp Powerup2; //Segunda estadística que sube


    public PowerUpItem(string name) : base(name)
    {
    }

    /// <summary>
    /// Getter de la primera estadística que sube
    /// </summary>
    public PowerUp GetPowerUp1()
    {
        return Powerup1;
    }

    /// <summary>
    /// Getter de la segunda estadística que sube
    /// </summary>
    public PowerUp GetPowerUp2()
    {
        return Powerup2;
    }
}

