//---------------------------------------------------------
// Gestor de escena. Podemos crear uno diferente con un
// nombre significativo para cada escena, si es necesario
// Guillermo Jiménez Díaz, Pedro Pablo Gómez Martín
// Template-P1
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// Componente que se encarga de la gestión de un nivel concreto.
/// Este componente es un singleton, para que sea accesible para todos
/// los objetos de la escena, pero no tiene el comportamiento de
/// DontDestroyOnLoad, ya que solo vive en una escena.
///
/// Contiene toda la información propia de la escena y puede comunicarse
/// con el GameManager para transferir información importante para
/// la gestión global del juego (información que ha de pasar entre
/// escenas)
/// </summary>
public class LevelManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----

    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject DefeatMenu;
    [SerializeField] private GameObject WinMenu;
    [SerializeField] private GameObject Meteorite;
    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject Pillar;
    [SerializeField] private Vector2[] PillarPos = new Vector2[3];
    [SerializeField] private Transform Player;
    [SerializeField] private float LimitX = 1.0f;
    [SerializeField] private float LimitY = 1.0f;
    [SerializeField] private float InitialTime;
    [SerializeField] private GameObject Spawner1;
    [SerializeField] private GameObject Spawner2;
    [SerializeField] private GameObject Spawner3;
    [SerializeField] private GameObject Spawner4;
    [SerializeField] private int Seed;

    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----

    #region Atributos Privados (private fields)

    /// <summary>
    /// Instancia única de la clase (singleton).
    /// </summary>
    private static LevelManager _instance;
    private float _timer;
    private int _pillarNum;
    private bool _fase1Done = false;
    private bool _pausedGame;
    private bool spawner1 = false;
    private bool spawner2 = false;
    private bool spawner3 = false;
    private bool spawner4 = false;
    private int kills;
    private float totalDamage;
    #endregion


    // ---- MÉTODOS DE MONOBEHAVIOUR ----

    #region Métodos de MonoBehaviour

    protected void Awake()
    {
        if (_instance == null)
        {
            // Somos la primera y única instancia
            _instance = this;
            Init();
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        _timer = InitialTime * 60;
        _fase1Done = false;
        _pausedGame = false;
        UnityEngine.Random.InitState(Seed);

    }

    void Update()
    {
        if (InputManager.Instance.PauseWasPressedThisFrame() && !HUDManager.Instance.IsInLevelUp())
        {
            if (!_pausedGame) PauseGame();
            else UnPauseGame();
            HUDManager.Instance.PauseMenuHUD(_pausedGame);
        }

        if (!_pausedGame)
        {
            if (!TimeUp())
            {
                _timer -= Time.deltaTime;
                HUDManager.Instance.UpdateTimerGUI(_timer);
            }
            else if (!_fase1Done)
            {
                OnTimeUp();
            }
        }

        
        if (_timer <= InitialTime * 60 && !spawner1)
        {
            CrearSpawners(Spawner1);
            spawner1 = true;
        }
            
        if (_timer <= ((InitialTime * 60)*2)/3 && !spawner2)
        {
            CrearSpawners(Spawner2);
            spawner2 = true;
        }

        if (_timer <= (InitialTime * 60)/2 && !spawner3)
        {
            CrearSpawners(Spawner3);
            spawner3 = true;
        }

        if (_timer <= (InitialTime * 60)/3 && !spawner4)
        {
            CrearSpawners(Spawner4);
            spawner4 = true;
        }
        
    }

    
    #endregion

    // ---- MÉTODOS PÚBLICOS ----

    #region Métodos públicos

    /// <summary>
    /// Propiedad para acceder a la única instancia de la clase.
    /// </summary>
    public static LevelManager Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    }

    public Transform GetPlayer()
    {
        return Player;
    }

    public bool GetPause()
    {
        return _pausedGame;
    } 

    
    

    /// <summary>
    /// Devuelve cierto si la instancia del singleton está creada y
    /// falso en otro caso.
    /// Lo normal es que esté creada, pero puede ser útil durante el
    /// cierre para evitar usar el LevelManager que podría haber sido
    /// destruído antes de tiempo.
    /// </summary>
    /// <returns>Cierto si hay instancia creada.</returns>
    public static bool HasInstance()
    {
        return _instance != null;
    }

    public void GetMapLimits(out float maxX, out float minX, out float maxY, out float minY)
    {
        maxX = LimitX / 2f;
        minX = -maxX;
        maxY = LimitY / 2f;
        minY = -maxY;
    }

    public float GetTimer()
    {
        return _timer;
    }

    public bool TimeUp()
    {
        return _timer <= 0;
    }


    public int PillarDestroyed()
    {
        _pillarNum--;
        return _pillarNum;
    }

    public void PauseGame()   
    {
        _pausedGame = true;
    }

    public void UnPauseGame()
    {
        _pausedGame = false;
    }

    public void PauseGameButton()
    {
        UnPauseGame();
        HUDManager.Instance.PauseMenuHUD(false);
    }

    public void PlayerDead()
    {
        PauseGame();
        HUDManager.Instance.DefeatMenuHUD();
    }

    public void Addkill()
    {
        kills++;
    }
    public int GetKills()
    {
        return kills;
    }

    public void AddDamage(float amount)
    {
        totalDamage += amount;
    }

    public float GetTotalDamage()
    {
        return totalDamage;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----

    #region Métodos Privados

    /// <summary>
    /// Dispara la inicialización.
    /// </summary>
    private void Init()
    {
        // De momento no hay nada que inicializar
    }

    private void OnTimeUp()
    {
        Instantiate(Meteorite);
        Instantiate(Boss);
        Instantiate(Pillar, PillarPos[0], Quaternion.identity);
        Instantiate(Pillar, PillarPos[1], Quaternion.identity);
        Instantiate(Pillar, PillarPos[2], Quaternion.identity);

        if (_pillarNum == 0)
        {
            _pillarNum = FindObjectsByType<Healing>(FindObjectsSortMode.None).Length;
        }
        _fase1Done = true;
    }

    private void CrearSpawners (GameObject spawner)
    {
        if (spawner != null)
        {
            Instantiate(spawner, transform.position, Quaternion.identity);
        }

    }
    #endregion
} // class LevelManager 
// namespace