//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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

    [SerializeField] RectTransform CanvasTransform;
    
    [SerializeField] private TMPro.TextMeshProUGUI TimerGUI;
    [SerializeField] private TMPro.TextMeshProUGUI Level;

    [Header("Magia")]
    [SerializeField] private TMPro.TextMeshProUGUI MagicText;
    [SerializeField] private Image MagicJar;

    [Header("Xp")]
    [SerializeField] private TMPro.TextMeshProUGUI XpText;
    [SerializeField] private Image XpBar;

    [Header("Health")]
    [SerializeField] private TMPro.TextMeshProUGUI HealthText;
    [SerializeField] private Image HealthJar;

    [Header("Objeto vacío con Spriterenderer para mostrar armas y habilidades, tiene que tener RectTransform")]
    [SerializeField] private SpriteRenderer HUDitem;

    [Header("Posiciones en las que están los items en orden")]
    [SerializeField] private Vector2[] posList;


    [Header("Imágenes y texto para los botones de la selección")]
    [SerializeField] private Image[] buttonImages;
    [SerializeField] private TMPro.TextMeshProUGUI[] buttonTexts;
    [SerializeField] private TMPro.TextMeshProUGUI[] buttonDescriptions;
    [SerializeField] private Button ConfirmationButton;

    
    [Header("Menús")]
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject DefeatMenu;
    [SerializeField] private GameObject WinMenu;
    [SerializeField] private GameObject SelectionMenu;

    [Header("")]
    

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

    private ItemSelectionManager SelectionManager;

    private struct HUDlist
    {
        public Vector2[] listaPos;
        public int index;
    }

    HUDlist elemList;


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
            Init();
        } // if-else somos instancia nueva o no.
        PauseMenu.SetActive(false);
        DefeatMenu.SetActive(false);
        WinMenu.SetActive(false);

        ConfirmationButton.gameObject.SetActive(false);
        SelectionManager = FindAnyObjectByType<ItemSelectionManager>(); 

        HUDlistIni(out elemList);

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
        if (XpText != null) XpText.text = exp + " / " + currentLimit;
        else Console.WriteLine("Texto de XP sin asignar");

        if (XpBar != null) XpBar.fillAmount = exp / currentLimit;
        else Console.WriteLine("Barra de XP sin asignar");

        if (Level != null) Level.text = "Nivel: " + level;
        else Console.WriteLine("Ui de nivel sin asignar");
    }

    public void UpdateMagicGUI(float currentMagic, float maxMagic) //actualizar el texto en pantalla limitando el valor de la magia actual a dos decimales para que se vea mejor
    {
        if (MagicText != null) MagicText.text = currentMagic.ToString("F0") + " / " + maxMagic;
        else Console.WriteLine("Texto de magia sin asignar");

        if (MagicJar != null) MagicJar.fillAmount = (currentMagic * 0.7f) / maxMagic;
        else Console.WriteLine("Tarro de magia sin asignar");
    }

    public void UpdateHealthGUI(int maxHealth, float currentHealth)
    {
        if (HealthText != null) HealthText.text = currentHealth.ToString("0") + " / " + maxHealth.ToString("0");
        else Console.WriteLine("Texto de vida sin asignar");

        if (HealthJar != null) HealthJar.fillAmount = (currentHealth * 0.7f) / maxHealth;
        else Console.WriteLine("Tarro de vida sin asignar");
    }

    public void UpdateSelectionGUI(Item item, int button)
    {
        TMPro.TextMeshProUGUI name = buttonTexts[button - 1];
        TMPro.TextMeshProUGUI description = buttonDescriptions[button - 1];
        Image image = buttonImages[button - 1];

        if (image != null) image.sprite = item.GetSprite();

        if(name != null) name.text = item.name;

        if (description != null) description.text = item.GetDescription();
    }

    public void PauseMenuHUD(bool pausedGame)
    {
        PauseMenu.SetActive(pausedGame);
        EventSystem.current.SetSelectedGameObject(PauseMenu.GetComponentInChildren<Button>().gameObject);
    }

    public void DefeatMenuHUD()
    {
        DefeatMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(DefeatMenu.GetComponentInChildren<Button>().gameObject);
        LevelManager.Instance.PauseGame();

    }

    public void LevelUpMenuHUD(bool levelUp)
    {
        SelectionMenu.SetActive(levelUp);
        ConfirmationButton.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(SelectionMenu.GetComponentInChildren<Button>().gameObject);
        LevelManager.Instance.PauseGame();
    }

    public bool IsInLevelUp()
    {
        return SelectionMenu.activeSelf;
    }

    public void WinMenuHUD()
    {
        WinMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(WinMenu.GetComponentInChildren<Button>().gameObject);
        LevelManager.Instance.PauseGame();
    }

    public void DmgItemsHUDEnable(Item item)
    {
        Vector3 pos = new Vector3(elemList.listaPos[elemList.index].x, elemList.listaPos[elemList.index].y, 0);
        SpriteRenderer HUDelem;

        HUDelem = SpriteRenderer.Instantiate(HUDitem, CanvasTransform, false);

        HUDelem.gameObject.AddComponent<RectTransform>();
        HUDelem.gameObject.AddComponent<Image>();

        HUDelem.gameObject.GetComponent<Image>().sprite = item.GetSprite();

        RectTransform rectTrans = HUDelem.GetComponent<RectTransform>();

        rectTrans.anchoredPosition = pos;
        elemList.index++;
    }    

    public void ConfirmSelection(int button)
    {
        ConfirmationButton.gameObject.SetActive(true);
        ConfirmationButton.onClick.RemoveAllListeners();
        switch (button)
        {
            case 1:
                ConfirmationButton.onClick.AddListener(SelectionManager.Option1);
                break;
            case 2:
                ConfirmationButton.onClick.AddListener(SelectionManager.Option2);
                break;
            case 3:
                ConfirmationButton.onClick.AddListener(SelectionManager.Option3);
                break;
        }

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

    private void HUDlistIni(out HUDlist lista)
    {
        lista.listaPos = new Vector2[6];
        for (int i = 0; i < lista.listaPos.Length; i++)
        {
            lista.listaPos[i] = posList[i];
        }
        lista.index = 0;
    }

    #endregion   

} // class HUDManager 
// namespace
