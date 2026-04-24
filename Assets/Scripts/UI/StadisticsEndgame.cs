//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using TMPro;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class StadisticsEndgame : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombr es de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private TextMeshProUGUI textMeshProStats;





    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    //nivel del jugador
    private int plevel; //nivel del jugador
    private float damage; //daño total
    private float time; //tiempo transcurrido
    private float mins;
    private float secs;

    private int kills; //enemigos muertos

    private float h;
    private float d ;
    private float m;

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
        if (playerLevel != null)
        {
            plevel = playerLevel.LevelPlayer();
        }
        
        if (levelManager != null)
        {
            time = (600 - levelManager.GetTimer());

            mins = Mathf.FloorToInt(time / 60); ;
            secs = secs = Mathf.FloorToInt(time % 60);

            kills = levelManager.GetKills();
            damage = levelManager.GetTotalDamage();
        }
        
        if (stats  != null)
        {
            h = stats.GetMaxHealth();
            m = stats.GetMaxMagic();
            d = stats.GetDmg();
        }

        //textMeshProStats.text = ("Nivel del jugador " + plevel + "\n\n" + "Has hecho " + damage + " de daño" + "\n\n" + "Has sobrevivido " + mins + " minutos y "+ secs + " segundos" + "\n\n" + "Has matado a " + kills + " enemigos" + "\n\n" + "Vida " + h + "\n" + "Daño " + d + "\n" + "Magia " + m);

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {// Sumamos el tiempo que pasó desde el frame anterior

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

    #endregion   

} // class StadisticsEndgame 
// namespace
