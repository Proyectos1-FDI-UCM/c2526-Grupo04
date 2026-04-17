//---------------------------------------------------------
// Gestiona la hitbox del rayo: la orienta hacia el ratón
// y la destruye tras un tiempo determinado.
// Rodrigo Ceña Álvarez
// MMDM
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

/// <summary>
/// Controla la habilidad de rayo: aparece una hitbox alargada desde el jugador
/// en la dirección del ratón, hace daño inmediato a todo lo que toca
/// y desaparece tras un tiempo determinado.
/// El daño lo gestiona el componente Damage adjunto al mismo GameObject.
/// </summary>
public class LightningBolt : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    /// <summary> Tiempo en segundos que la hitbox permanece activa </summary>
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
        AudioManager.Instance.PlayLightningSound();
        _spawnTime = Time.time;

        Vector2 aim = InputManager.Instance.AimVector;
        if (aim != Vector2.zero)
        {
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        // Desplazamos el rayo en la dirección que apunta para que salga desde el borde del player
        transform.position += transform.up;
    }

    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            if (Time.time > _spawnTime + Duration)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Duration += Time.deltaTime;
        }

            // Destruimos el rayo una vez ha pasado su duración

        }

    #endregion

} // class LightningBolt
  // namespace