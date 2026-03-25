//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
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
    [SerializeField] private Image[] buttonImages;
    [SerializeField] private TMPro.TextMeshProUGUI[] buttonTexts;
    

    // Menús
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject DefeatMenu;
    [SerializeField] private GameObject WinMenu;
    [SerializeField] private GameObject SelectionMenu;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private static HUDManager _instance;
    

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
        if (_instance != null)
        {
            // No somos la primera instancia. Se supone que somos un
            // GameManager de una escena que acaba de cargarse, pero
            // ya había otro en DontDestroyOnLoad que se ha registrado
            // como la única instancia.
            // Si es necesario, transferimos la configuración que es
            // dependiente de este manager al que ya existe.
            // Esto permitirá al GameManager real mantener su estado interno
            // pero acceder a los elementos de la nueva escena
            // o bien olvidar los de la escena previa de la que venimos
            TransferManagerSetup();

            // Y ahora nos destruímos del todo. DestroyImmediate y no Destroy para evitar
            // que se inicialicen el resto de componentes del GameObject para luego ser
            // destruídos. Esto es importante dependiendo de si hay o no más managers
            // en el GameObject.
            DestroyImmediate(this.gameObject);
        }
        else
        {
            // Somos el primer GameManager.
            // Queremos sobrevivir a cambios de escena.
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            Init();
        } // if-else somos instancia nueva o no.
        PauseMenu.SetActive(false);
        DefeatMenu.SetActive(false);
        WinMenu.SetActive(false);


        


    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public static HUDManager Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    } // Instance

    public void UpdateTimerGUI(float timer)
    {
        float mins = (int)timer / 60;
        float secs = (int)timer % 60;

        if (mins >= 1)
        {
            TimerGUI.text = string.Format("{0:00}:{1:00}", mins, secs);
        }
        else TimerGUI.text = secs.ToString("F0");
    }

    public void UpdateLevelGUI(int level, float exp, float currentLimit)
    {
        if (XpTank != null) XpTank.text = exp + " / " + currentLimit;
        else Console.WriteLine("Tanque de XP sin asignar");

        if (Level != null) Level.text = "Nivel: " + level;
        else Console.WriteLine("Ui de nivel sin asignar");
    }

    public void UpdateMagicGUI(float currentMagic, float maxMagic) //actualizar el texto en pantalla limitando el valor de la magia actual a dos decimales para que se vea mejor
    {
        if (MagicTank != null) MagicTank.text = "Magia: " + currentMagic.ToString("F0") + " / " + maxMagic;
        else Console.WriteLine("Tanque de magia sin asignar");
    }

    public void UpdateHealthGUI(int maxHealth, float currentHealth)
    {
        if (HealthTank != null) HealthTank.text = "Vida: " + currentHealth.ToString("0") + " / " + maxHealth.ToString("0");
        else Console.WriteLine("Tanque de vida sin asignar");
    }

    public void UpdateSelectionGUI(Item item, int button)
    {

        TMPro.TextMeshProUGUI name = buttonTexts[button - 1];
        Image image = buttonImages[button - 1];

        if (image!=null) image.sprite = item.GetSprite();

        if(name!=null) name.text = item.name;
    }

    public void PauseMenuHUD(bool pausedGame)
    {
        PauseMenu.SetActive(pausedGame);
    }

    public void DefeatMenuHUD(bool defeat)
    {
        DefeatMenu.SetActive(defeat);
    }

    public void LevelUpMenuHUD(bool levelUp)
    {
        SelectionMenu.SetActive(levelUp);
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private void Init()
    {
        // De momento no hay nada que inicializar
    }

    private void TransferManagerSetup()
    {
        // De momento no hay que transferir ningún setup
        // a otro manager
    }

    #endregion   

} // class HUDManager 
// namespace
