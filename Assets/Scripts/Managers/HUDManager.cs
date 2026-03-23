//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class HUDManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    // Elementos del HUD
    [SerializeField] private TMPro.TextMeshProUGUI TimerGUI;
    [SerializeField] private TMPro.TextMeshProUGUI Level;

    [SerializeField] private TMPro.TextMeshProUGUI MagicTank; 
    [SerializeField] private TMPro.TextMeshProUGUI XpTank;
    [SerializeField] private TMPro.TextMeshProUGUI HealthTank; 


    // Menús
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject DefeatMenu;
    [SerializeField] private GameObject WinMenu;


    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private static LevelManager _levelManager;
    
    private float _timer;

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
    protected void Awake()
    {
        // aqui que busque al componente levelmanager
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
        if (!_levelManager.IsPaused())
        {
            _timer = _levelManager.GetTimer();
            if (!_levelManager.TimeUp())
            {
                UpdateGUI(_timer);
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

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private void UpdateGUI(float timer)
    {
        if (!_levelManager.TimeUp())
        {
            UpdateTimerGUI(timer);
        }

    }

    private void UpdateTimerGUI(float timer)
    {
        float mins = (int)timer / 60;
        float secs = (int)timer % 60;

        if (mins >= 1)
        {
            TimerGUI.text = string.Format("{0:00}:{1:00}", mins, secs);
        }
        else TimerGUI.text = secs.ToString("F0");
    }

    #endregion   

} // class HUDManager 
// namespace
