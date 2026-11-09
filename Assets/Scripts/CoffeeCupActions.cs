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

    private void Awake()
    {
        _options.SetActive(false);
        _steam.Stop(true);
        _liquid.SetActive(false);
    }

    private void OnDisable()
    {
        _steam.Stop(true);
    }

    public void FillCup()
    {
        if (!_liquid.activeInHierarchy)
        {
            _label.text = "Empty";
            _liquid.SetActive(true);
            _steam.Play(true);
        }
        else
        {
            _label.text = "Fill";
            _steam.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _liquid.SetActive(false);
        }
    }
}
