//---------------------------------------------------------
// Controla el comportamiento del rastro de fuego que deja
// el Caminante Ardiente. Daña al player si lo pisa y
// desaparece tras un tiempo determinado.
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

/// <summary>
/// Rastro de fuego instanciado por el Caminante Ardiente.
/// Inflige daño al player mientras permanece activo y se destruye
/// tras un tiempo configurable desde el editor.
/// El daño lo gestiona el componente Damage adjunto al mismo GameObject.
/// </summary>
public class FireTrail : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    /// <summary> Tiempo en segundos que el rastro permanece activo antes de desaparecer </summary>
    [SerializeField] private float Duration;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private float _spawnTime;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void Awake()
    {
        _spawnTime = Time.time;
    }

    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            // Destruimos el rastro una vez ha pasado su duración
            if (Time.time > _spawnTime + Duration)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Duration += Time.deltaTime;
        }
    }

    #endregion

} // class FireTrail
  // namespace