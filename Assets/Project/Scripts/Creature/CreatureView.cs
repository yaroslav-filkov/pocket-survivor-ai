using UnityEngine;
using UnityEngine.UI;

public class CreatureView : MonoBehaviour
{
    [SerializeField] private Creature _model;
    [SerializeField] private HealthBarView _healthBarView;
    [SerializeField] private Image _image;
    private void Start()
    {
        _healthBarView.Setup(_model, LocalizationService.GetTextTranslation(_model.RemoteConfigDto.Name));
        _image.sprite = _model.Configuration.Sprite;
    }
}
