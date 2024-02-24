using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _currentValue;
    [SerializeField] private TextMeshProUGUI _currentInfo;

    private IDamagable _attached;
    private float _startHealthValue;
    public void Setup(IDamagable attached, string infoBar = "")
    {
        _attached = attached;
        _currentInfo.text = infoBar;
        _startHealthValue = attached.HealthPoint;

        _attached.DamageDone += OnApplyDamage;
        _attached.RestoreDone += OnRestored;
       UpdateBar();
    }

    private void OnDestroy()
    {
        _attached.DamageDone -= OnApplyDamage;
        _attached.RestoreDone -= OnRestored;
    }

 
    private void OnApplyDamage(DamageData damageData)
    {
        UpdateBar();
    }

    private void OnRestored(RestoreData restoreData)
    {
        UpdateBar();
    }
    private void UpdateBar()
    {
        _currentValue.text = _attached.HealthPoint.ToString();
        _progressBar.fillAmount = _attached.HealthPoint / _startHealthValue;
    }
}
