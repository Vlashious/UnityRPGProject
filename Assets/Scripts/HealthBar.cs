using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HitPoint _hitPoint;
    [HideInInspector]
    public Player character;
    [SerializeField]
    private Image _meterImage;
    [SerializeField]
    private Text _hpText;
    private float maxHitPoints;
    void Start()
    {
        // maxHitPoints = character.maxHitPoints;
    }

    void Update()
    {
        if (character != null)
        {
            _meterImage.fillAmount = _hitPoint.value / character.maxHitPoints;

            _hpText.text = $"HP: {_meterImage.fillAmount * 100}";
        }
    }
}