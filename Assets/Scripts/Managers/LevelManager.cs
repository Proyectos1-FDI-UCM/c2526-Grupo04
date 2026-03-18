//---------------------------------------------------------
// Gestor de escena. Podemos crear uno diferente con un
// nombre significativo para cada escena, si es necesario
// Guillermo Jiménez Díaz, Pedro Pablo Gómez Martín
// Template-P1
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

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
    [SerializeField] private TMPro.TextMeshProUGUI TimerGUI;

    [SerializeField] private Transform Player;
    [SerializeField] private float LimitX = 1.0f;
    [SerializeField] private float LimitY = 1.0f;
    [SerializeField] private float InitialTime;
    [SerializeField] private GameObject Meteorite;
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
    private bool _meteorite = false;
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
        _timer = InitialTime * 60;
        _meteorite = false;
        Meteorite.SetActive(false);
    }

    void Update()
    {
        if (!TimeUp())
        {
            _timer -= Time.deltaTime;
        }
        else OnTimeUp();

        UpdateGUI();
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

    private void UpdateGUI()
    {
        if (!TimeUp())
        {
            UpdateTimerGUI();
        }
        
    }

    private void UpdateTimerGUI()
    {
        float mins = (int)_timer / 60;
        float secs = (int)_timer % 60;

        if (mins >= 1)
        {
            TimerGUI.text = string.Format("{0:00}:{1:00}", mins, secs);
        }
        else TimerGUI.text = secs.ToString("F0");
    }

    private void OnTimeUp()
    {
        if (!_meteorite) 
        {
            Meteorite.SetActive(true);
            _meteorite = true;
        }
    }

    #endregion
} // class LevelManager 
// namespace