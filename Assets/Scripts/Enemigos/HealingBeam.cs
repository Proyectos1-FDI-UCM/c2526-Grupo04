//---------------------------------------------------------
// Genera un rayo visual entre el pilar y el boss al aparecer,
// representando la curación que los pilares otorgan al boss.
// Rodrigo Ceña Álvarez
// MMDM
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

/// <summary>
///Al activarse, instancia un rectángulo visual que va desde
///la cima del pilar hasta la posición del boss, representando
///el rayo curativo. Se destruye cuando el pilar muere.
/// </summary>
public class HealingBeam : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    /// <summary> 
    ///Prefab del rectángulo visual del rayo
    /// </summary>
    [SerializeField] private GameObject BeamPrefab;

    /// <summary>
    ///Offset desde la base del pilar hasta su cima
    /// </summary>
    [SerializeField] private float PillarTopOffset;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private GameObject _beam;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void Start()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss == null || BeamPrefab == null) return;

        //Calculamos la posición de la cima del pilar
        Vector2 pillarTop = (Vector2)transform.position + Vector2.up * PillarTopOffset;
        Vector2 bossPos = boss.transform.position;

        //Calculamos la distancia y la dirección entre el pilar y el boss
        float distance = Vector2.Distance(pillarTop, bossPos);
        Vector2 direction = (bossPos - pillarTop).normalized;

        //Instanciamos el rayo
        _beam = Instantiate(BeamPrefab);

        //Lo posicionamos en el punto medio entre el pilar y el boss
        _beam.transform.position = (Vector3)(pillarTop + direction * distance * 0.5f);

        //Lo rotamos para que apunte hacia el boss
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        _beam.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        //Escalamos en Y para que cubra toda la distancia
        _beam.transform.localScale = new Vector3(_beam.transform.localScale.x, distance, _beam.transform.localScale.z);

        SpriteRenderer beamRenderer = _beam.GetComponent<SpriteRenderer>();
        if (beamRenderer != null)
        {
            //Si el pilar está por encima del boss, el rayo va detrás del pilar
            //Si está por debajo, va delante
            beamRenderer.sortingOrder = pillarTop.y > bossPos.y ? 3 : 2;
        }
    }

    private void OnDestroy()
    {
        //Destruimos el rayo cuando el pilar muere
        if (_beam != null) Destroy(_beam);
    }

    #endregion

} // class HealingBeam
  // namespace