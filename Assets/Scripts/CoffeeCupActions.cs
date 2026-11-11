using UnityEngine;
using TMPro;

public sealed class CoffeeCupActions : MonoBehaviour
{
    [SerializeField]
    private GameObject _options = null;

    [SerializeField]
    private GameObject _liquid = null;

    [SerializeField]
    private TextMeshProUGUI _label = null;

    [SerializeField]
    private ParticleSystem _steam = null;

    [SerializeField]
    private GameObject _cupTemplate = null;

    [SerializeField]
    private Vector3 _newCupOffset = new Vector3(0.22f, 0f, 0f);

    [SerializeField]
    private TextMeshProUGUI _movementLabel = null;

    [SerializeField]
    private CoffeeLiquidMotion _liquidMotion = null;

    private int _spawnedCupCount;

    private void Awake()
    {
        _options.SetActive(false);
        _steam.Stop(true);
        _liquid.SetActive(false);
        if (_liquidMotion != null)
        {
            _liquidMotion.enabled = false;
        }
        UpdateMovementLabel();
    }

    public bool IsLiquidMovementEnabled => _liquidMotion != null && _liquidMotion.enabled;

    public void GenerateNewCup()
    {
        if (_cupTemplate == null)
        {
            return;
        }

        _spawnedCupCount++;
        GameObject clone = Instantiate(
            _cupTemplate,
            _cupTemplate.transform.position + _newCupOffset * _spawnedCupCount,
            _cupTemplate.transform.rotation,
            _cupTemplate.transform.parent);
        clone.name = _cupTemplate.name + " (New " + _spawnedCupCount + ")";

        // Clone the cup body's material before tinting so the original
        // cup and other clones keep their own colors.
        MeshRenderer body = clone.GetComponent<MeshRenderer>();
        if (body != null)
        {
            body.material = new Material(body.sharedMaterial);
            body.material.color = Random.ColorHSV(0f, 1f, 0.4f, 1f, 0.5f, 1f);
        }
    }

    public void ToggleLiquidMovement()
    {
        if (_liquidMotion == null)
        {
            return;
        }

        _liquidMotion.enabled = !_liquidMotion.enabled;
        UpdateMovementLabel();
    }

    private void UpdateMovementLabel()
    {
        if (_movementLabel != null)
        {
            _movementLabel.text = IsLiquidMovementEnabled ? "Movement Off" : "Movement On";
        }
    }

    private void OnDisable()
    {
        _steam.Stop(true);
    }

    public void FillCup()
    {
        if (!_liquid.activeInHierarchy)
        {
            _label.text = "Empty Cup";
            _liquid.SetActive(true);
            _steam.Play(true);
        }
        else
        {
            _label.text = "Fill Cup";
            _steam.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _liquid.SetActive(false);
        }
    }
}
