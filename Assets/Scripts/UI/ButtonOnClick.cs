//---------------------------------------------------------
// Script hecho para que los botones se asignen su función correspondiente desde el GameManager
// de la escena.
// Arturo Ramos Romero
// MMDM (Meteorito Monstruos Duendes Matar)
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class ButtonOnClick : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
 
    [SerializeField] private use SpecificUse;
    [Header("Solo si SpecificUse es ChangeScene")]
    [SerializeField] private int NextScene;

    #endregion
    
    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private enum use // Enumerado que hace referencia al uso específico del botón.
    {
        ChangeScene,
        QuitGame,
        Unpause
    }

    private Button _button; // Componente Button del GameObject.

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    
    void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        if (_button != null)
        {
            switch (SpecificUse) // En función del uso que se marque, se le asigna un método u otro.
            {
                case use.ChangeScene:
                    _button.onClick.AddListener(() => GameManager.Instance.ChangeScene(NextScene));
                    break;
                case use.QuitGame:
                    _button.onClick.AddListener(() => GameManager.Instance.QuitGame());
                    break;
                case use.Unpause:
                    _button.onClick.AddListener(() => LevelManager.Instance.PauseGameButton());
                    break;
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

    #endregion   

} // class ButtonOnClick 
// namespace
