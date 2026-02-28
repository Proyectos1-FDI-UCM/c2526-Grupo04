//---------------------------------------------------------
// Contiene el script para la gestión de la magia (prototipo)
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
public class SistemaMagia : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    [SerializeField]
    private float tiempoTotalRecarga;

    [SerializeField]
    private float coste;

    [SerializeField]
    private TMPro.TextMeshProUGUI tanqueMagia;

    [SerializeField]
    private float magiaMax;

    // ---- ATRIBUTOS PRIVADOS ----
    private float magiaActual;    
    private float tiempoActualRecarga;
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    void Start()
    {
        magiaActual = 0f;
        magiaMax = 100;
        tiempoActualRecarga = tiempoTotalRecarga;
    }

    void Update()
    {
        if (magiaActual < magiaMax)
        {
            tiempoActualRecarga = (magiaMax - magiaActual) * tiempoTotalRecarga / magiaMax;
            magiaActual += (Time.deltaTime * (magiaMax - magiaActual)) / tiempoActualRecarga;
            magiaActual = Mathf.Clamp(magiaActual, 0, magiaMax);            
            
        }

        if (InputManager.Instance.FireWasPressedThisFrame() && magiaActual > coste)
        {
            magiaActual -= coste;
        }

        UpdateGUI();
    }

    void UpdateGUI()
    {
        tanqueMagia.text = magiaActual.ToString("F2") + " / " + magiaMax;
    }

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

} // class Magia 
// namespace
