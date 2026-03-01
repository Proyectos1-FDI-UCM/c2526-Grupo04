//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// Clase base para todos los enemigos del juego. Gestiona el movimiento,
/// la persecución del jugador y el sistema de colisión con ataque y retroceso.
/// </summary>
public class EnemyStateMachine : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    /// <summary> Velocidad de movimiento del enemigo en unidades por segundo </summary>
    [SerializeField] protected float MovementSpeed;

    /// <summary> Layer del player, usada para detectar colisiones con él </summary>
    [SerializeField] protected int EnemyRangeLayer;

    /// <summary> Fuerza del empuje que recibe el enemigo al chocar con el player </summary>
    [SerializeField] private float KnockbackForce;

    /// <summary> Daño que inflige el enemigo al player al colisionar </summary>
    [SerializeField] protected float Damage;

    /// <summary> Duración en segundos del empuje tras colisionar con el player </summary>
    [SerializeField] private float KnockbackDuration;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    protected GameObject _player;
    protected Collider2D _collider;
    protected Rigidbody2D _rb;
    protected Vector2 _playerPosition;
    protected Vector2 _dir;

    // Temporizador para actualizar la dirección cada cierto tiempo
    protected float _dirTimer = 0f;

    // Tolerancia en grados para detectar direcciones cardinales
    protected int _tolerancy = 15;

    // Intervalo de tiempo entre actualizaciones de dirección
    protected float _dirTime = 0.25f;

    protected enum State
    {
        Chasing,
        Attacking
    }

    protected State _currentState;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    /// Inicializa las referencias necesarias y establece el estado inicial de persecución.
    /// </summary>
    void Start()
    {
        _currentState = State.Chasing;
        _player = FindFirstObjectByType<Movement>().gameObject;
        _rb = GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<Collider2D>();
    }

    /// <summary>
    /// Ejecuta la máquina de estados en cada frame.
    /// </summary>
    void Update()
    {
        SetState();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == EnemyRangeLayer)
        {
            _currentState = State.Attacking;

            // Calculamos la dirección de retroceso: desde el player hacia el enemigo
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            // Iniciamos la corrutina de knockback cinemático
            StartCoroutine(KnockbackCoroutine(knockbackDirection));
        }
    }

    /// <summary>
    /// Mueve al enemigo en la dirección de retroceso durante un breve tiempo
    /// simulando el efecto de knockback de forma cinemática.
    /// </summary>
    private IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        float timer = 0f;

        while (timer < KnockbackDuration)
        {
            transform.position += (Vector3)(direction * KnockbackForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    /// <summary>
    /// Devuelve la dirección de movimiento actual del enemigo.
    /// </summary>
    public Vector2 GetDir()
    {
        return _dir;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    /// <summary>
    /// Evalúa el estado actual y ejecuta su lógica correspondiente.
    /// Detiene el movimiento antes de cada estado para evitar inercias no deseadas.
    /// </summary>
    protected void SetState()
    {
        _rb.linearVelocity = Vector2.zero;
        switch (_currentState)
        {
            case State.Chasing:
                ChasingState();
                break;

            case State.Attacking:
                AttackingState();
                break;
        }
    }

    /// <summary>
    /// Actualiza la dirección hacia el player cada _dirTime segundos
    /// para no recalcularla en cada frame y optimizar el rendimiento.
    /// </summary>
    protected void SetDir()
    {
        if (_dirTimer >= _dirTime)
        {
            _playerPosition = _player.transform.position;
            _dir = ((Vector2)_player.transform.position - (Vector2)transform.position).normalized;
            _dirTimer = 0f;
        }
        else _dirTimer += Time.deltaTime;
    }

    /// <summary>
    /// Estado de persecución: el enemigo se mueve hacia el player
    /// actualizando su dirección periódicamente.
    /// </summary>
    protected virtual void ChasingState()
    {
        SetDir();
        transform.position += (Vector3)(_dir * MovementSpeed * Time.deltaTime);

        // Calculamos la dirección hacia el player
        Vector3 direction = (_player.transform.position - transform.position).normalized;

        // Obtenemos el ángulo que el enemigo debe girar para apuntar al player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Rotamos el enemigo dicho ángulo respecto al eje Z
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    /// <summary>
    /// Estado de ataque: inflige daño al player y vuelve a perseguirle.
    /// </summary>
    protected virtual void AttackingState()
    {
        // TODO: _player.GetComponent<Health>().TakeDamage(Damage);

        // Una vez atacado, vuelve a perseguir al player
        _currentState = State.Chasing;
    }

    #endregion

} // class EnemyStateMachine 
  // namespace