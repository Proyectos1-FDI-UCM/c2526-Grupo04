//---------------------------------------------------------
// Gestiona la generación periódica del rastro de fuego
// que deja el Caminante Ardiente a su paso.
// Rodrigo Ceña Álvarez
// MMDM
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

/// <summary>
/// Componente que instancia rastros de fuego periódicamente
/// en la posición del enemigo mientras este se mueve.
/// </summary>
public class FireTrailSpawner : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    /// <summary> Prefab del rastro de fuego a instanciar </summary>
    [SerializeField] private GameObject FireTrailPrefab;

    /// <summary> Cada cuántos segundos se genera un nuevo rastro </summary>
    [SerializeField] private float SpawnInterval;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private float _lastSpawnTime;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void Start()
    {
        _lastSpawnTime = Time.time;
    }

    void Update()
    {
        // Instanciamos un rastro cada SpawnInterval segundos en la posición actual del enemigo
        if (!LevelManager.Instance.GetPause())
        {
            if (Time.time > _lastSpawnTime + SpawnInterval)
            {
                Instantiate(FireTrailPrefab, transform.position, Quaternion.identity);
                _lastSpawnTime = Time.time;
            }
        }
    }

    #endregion

} // class FireTrailSpawner
  // namespace