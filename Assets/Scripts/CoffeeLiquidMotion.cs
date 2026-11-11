using UnityEngine;

/// <summary>
/// Subtle sloshing motion for the coffee liquid surface. Only moves the
/// liquid's local transform around the pose it had when enabled, and
/// restores that pose when disabled, so it never drifts away from the cup.
/// </summary>
public sealed class CoffeeLiquidMotion : MonoBehaviour
{
    [SerializeField]
    private float _tiltDegrees = 1.5f;

    [SerializeField]
    private float _speed = 2f;

    [SerializeField]
    private float _bobMeters = 0.001f;

    private Vector3 _basePosition;
    private Quaternion _baseRotation;
    private float _time;

    private void OnEnable()
    {
        _basePosition = transform.localPosition;
        _baseRotation = transform.localRotation;
    }

    private void OnDisable()
    {
        transform.localPosition = _basePosition;
        transform.localRotation = _baseRotation;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        float sway = Mathf.Sin(_time * _speed);
        float surge = Mathf.Sin(_time * _speed * 0.7f + 1.3f);
        transform.localPosition = _basePosition + new Vector3(sway * _bobMeters, 0f, surge * _bobMeters);
        transform.localRotation = _baseRotation * Quaternion.Euler(surge * _tiltDegrees, 0f, sway * _tiltDegrees);
    }
}
