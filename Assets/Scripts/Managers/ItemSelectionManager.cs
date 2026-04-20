//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class ItemSelectionManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    //interfaz visual del juego y del menú de elección
    [SerializeField] private GameObject GameCanvas;

    //Array de todos los tipos de items del juego
    [SerializeReference] private Item[] ItemList = new Item[] {
        new DamageItem("Lanza"),
        new DamageItem("Maza"),
        new DamageItem("Espada"),
        new AbilityItem("Rayo"),
        new AbilityItem("Fireball"),
        new AbilityItem("Poison"),
        new PowerUpItem("Casco"),
        new PowerUpItem("Pocima"),
        new PowerUpItem("Sello"),
        new PowerUpItem("Pesa"),
        new PowerUpItem("Tunica"),
        new PowerUpItem("Orbe"),
    };


    

    //textos de los botones con las distintas elecciones

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints


    //array con todas las opciones posibles que pueden salir

    

    private enum Options{Lanza, Maza, Espada, Rayo, Fireball, Poison, Casco, Pocima, Sello, Pesa, Tunica, Orbe};
    private int queue = 0;
    private Potenciadores _potenciadores;
    private Item item1, item2, item3;
    private bool FirstTime;
    private int[] _random;
    private int _elec;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        FirstTime = true;
        _potenciadores = GetComponent<Potenciadores>(); //obtenemos componente potenciadores
        _random = new int[100];
        for (int i = 0; i < 100; i++)
        {
            _random[i] = UnityEngine.Random.Range(0, ItemList.Length);
        }
        _elec = 0;
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            if (queue > 0)
            {
                PowerUpScreen();
                queue--;
            }
        }
    }


    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public void PowerUpScreen() //método para pausar el juego, escondemos la UI del juego, mostramos el menú y paramos el resto
    {
        GameCanvas.SetActive(false);
        HUDManager.Instance.LevelUpMenuHUD(true);
        LevelManager.Instance.PauseGame();
        RandomOptions(); //elegimos aleatoriamente las opciones que se mostrarán en el menú
    }

    public void Option1() //se ejecuta cuando el jugador elige la primera opcion
    {
        AudioManager.Instance.ClickSound();
        DamageItem DItem1 = item1 as DamageItem;
        PowerUpItem PUItem1 = item1 as PowerUpItem;
        if (DItem1 != null) 
        { 
            Instantiate(DItem1.GetPrefab());
            ItemHUDEnable(item1);
        }
        else if (PUItem1 != null)
        {
            _potenciadores.Potencia(PUItem1.GetPowerUp1());
            _potenciadores.Potencia(PUItem1.GetPowerUp2());
        }
        ResumeGame();
    }

    public void Option2() //se ejecuta cuando el jugador elige la segunda opcion
    {
        AudioManager.Instance.ClickSound();
        DamageItem DItem2 = item2 as DamageItem;
        PowerUpItem PUItem2 = item2 as PowerUpItem;
        if (DItem2 != null)
        {
            Instantiate(DItem2.GetPrefab());
            ItemHUDEnable(item2);
        }
        else if (PUItem2 != null)
        {
            _potenciadores.Potencia(PUItem2.GetPowerUp1());
            _potenciadores.Potencia(PUItem2.GetPowerUp2());
        }
        ResumeGame();
    }

    public void Option3() //se ejecuta cuando el jugador elige la tercera opcion
    {
        AudioManager.Instance.ClickSound();
        DamageItem DItem3 = item3 as DamageItem;
        PowerUpItem PUItem3 = item3 as PowerUpItem;
        if (DItem3 != null) 
        {
            Instantiate(DItem3.GetPrefab());
            ItemHUDEnable(item3);
        }
        else if (PUItem3 != null)
        {
            _potenciadores.Potencia(PUItem3.GetPowerUp1());
            _potenciadores.Potencia(PUItem3.GetPowerUp2());
        }
        ResumeGame();
    }

    public void QueueUp()
    {
        queue++;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)


    
    
    private void RandomOptions() //elegimos al azar las opciones entre todas las disponibles
    {
        // Si se trata de la primera selección de la partida, solo se podrá elegir entre las 3 armas del juego
        do
        {   
            if (FirstTime) item1 = ItemList[0];
            else item1 = ItemList[_random[_elec]]; _elec++;
        } while (item1.WasUsed() == true); //nos aseguramos de que no se haya elegido antes
        HUDManager.Instance.UpdateSelectionGUI(item1, 1);

        do 
        {
            if (FirstTime) item2 = ItemList[1];
            else item2 = ItemList[_random[_elec]]; _elec++;
        } while (item1 == item2 || item2.WasUsed() == true); //ademas en las siguientes nos aseguramos de que no sean igual que las anteriores
        HUDManager.Instance.UpdateSelectionGUI(item2, 2);

        do
        {
            if (FirstTime) item3 = ItemList[2]; 
            else item3 = ItemList[_random[_elec]]; _elec++;
        } while (item3 == item1 || item3 == item2 || item3.WasUsed() == true); //aqui igual
        HUDManager.Instance.UpdateSelectionGUI(item3, 3);
        // Cambiamos el valor del booleano FirstTime para que de ahora en adelante salgan todas las opciones
        if (FirstTime) FirstTime = false;
    }
    
    private void ResumeGame() //metodo para reanudar el juego cuando se haya elgido alguna de las opciones, hace lo opuesto al metodo de pausa
    {
        HUDManager.Instance.LevelUpMenuHUD(false);
        GameCanvas.SetActive(true);
        LevelManager.Instance.UnPauseGame();
    }
    
    private void ItemHUDEnable(Item item)
    {
        HUDManager.Instance.DmgItemsHUDEnable(item);
    }
    
    #endregion   

} // class ItemSelectionManager 
// namespace
