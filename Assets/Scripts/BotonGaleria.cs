//---------------------------------------------------------
// Gestiona los botones de los personajes de la galería
// Rodrigo Ceña Álvarez
// MMDM
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;
// Añadir aquí el resto de directivas using


public class BotonGaleria : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] Sprite sprite;
    [SerializeField] string nombre;
    [SerializeField] string descripcion;
    [SerializeField] float HoverScale = 1.1f;

    [SerializeField] Panel panel;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private Vector3 _originalScale;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    ///Guarda el tamaño original del botón
    /// </summary>
    void Start()
    {
        _originalScale = transform.localScale;
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    ///Muestra la info del personaje
    /// </summary>
    public void OnClickGalleryButton()
    {
        panel.MostrarDuende(sprite, nombre, descripcion);
    }

    /// <summary>
    ///Cierra el panel de info
    /// </summary>
    public void ClosePanel()
    {
        panel.Cerrar();
    }

    /// <summary>
    ///Amplía el tamaño al hacer hover
    /// </summary>
    public void OnPointerEnter()
    {
        transform.localScale = _originalScale * HoverScale;
    }

    /// <summary>
    ///Restaura el tamaño al salir del hover
    /// </summary>
    public void OnPointerExit()
    {
        transform.localScale = _originalScale;
    }

    #endregion


} // class BotonGaleria 
// namespace
