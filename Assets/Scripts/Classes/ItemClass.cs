using UnityEngine;
using System;
using System.Runtime.CompilerServices;

[Serializable]
public class Item
{
    /// <summary>
    /// nombre del objeto
    /// </summary>
    public string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite; // Imagen del objeto (para selecci�n o HUD)
    private bool used; // indica si el objeto se ha usado o no (relevante para armas y habilidades)

    public Item(string name) //Constructora por nombre
    {
        this.name = name;
        used = false;
    }

    /// <summary>
    /// Devuelve el sprite del item
    /// </summary>
    public Sprite GetSprite()
    {
        return sprite;
    }

    /// <summary>
    /// Devuelve la descripci�n del item
    /// </summary>
    /// <returns></returns>
    public string GetDescription()
    {
        return description;
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

public class AbilityItem : DamageItem
{
    [SerializeField] private Sprite KeyboardSprite;
    [SerializeField] private Sprite ControllerSprite;

    public AbilityItem(string name) : base(name) //Constructora por nombre
    {
    }

    public Sprite GetKeyboardSprite()
    {
        return KeyboardSprite;
    }

    public Sprite GetControllerSprite()
    {
        return ControllerSprite;
    }

}
public class PowerUpItem : Item
{
    [SerializeField] private PowerUp Powerup1; //Primera estad�stica que sube
    [SerializeField] private PowerUp Powerup2; //Segunda estad�stica que sube


    public PowerUpItem(string name) : base(name)
    {
    }

    /// <summary>
    /// Getter de la primera estad�stica que sube
    /// </summary>
    public PowerUp GetPowerUp1()
    {
        return Powerup1;
    }

    /// <summary>
    /// Getter de la segunda estad�stica que sube
    /// </summary>
    public PowerUp GetPowerUp2()
    {
        return Powerup2;
    }
}
