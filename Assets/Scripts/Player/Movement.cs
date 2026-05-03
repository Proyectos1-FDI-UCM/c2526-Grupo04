//---------------------------------------------------------
// Gestiona el movimiento del jugador
// Rodrigo Ceña Álvarez
// MMDM
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
///Componente que permite que el jugador se mueva por el mapa.
/// </summary>
public class Movement : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField] private float Velocity;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float minX, maxX, minY, maxY;

    private Vector3 _scale;

    private Animator _animator;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    void Start()
    {
        LevelManager.Instance.GetMapLimits(out maxX, out minX, out maxY, out minY);

        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
    }

    /// <summary>
    ///Obtiene el vector de movimiento del input manager y actualiza la posición
    ///del jugador en función de este.
    /// </summary>
    void Update()
    {
        if (!LevelManager.Instance.GetPause())
        {
            Vector2 movement = InputManager.Instance.MovementVector;

           

            transform.position += (Vector3)(movement * Velocity * Time.deltaTime);

            _animator.SetFloat("Speed", movement.magnitude);

            Vector3 pos = transform.position;

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            transform.position = pos;

            if (movement.x < 0 && transform.localScale.x != 1f)
            {
                _scale.x = 1f;
                transform.localScale = _scale;
            }
            else if (movement.x > 0 && transform.localScale.x != -1f)
            {
                _scale.x = -1f;
                transform.localScale = _scale;
            }

            if (movement != Vector2.zero)
            {
                // Calculamos el ángulo de rotación. Obtenemos el ángulo en radianes y lo convertimos a grados, y después le restamos 90
                //  (pues el triángulo apunta hacia arriba la punta)
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;


                // Una vez tenemos el ángulo, rotamos el triangulo a esa dirección
                transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }
    #endregion

} // class Movement 
// namespace
