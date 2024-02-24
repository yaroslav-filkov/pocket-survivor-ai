using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class UITextInterpreter : MonoBehaviour
{
    [SerializeField] private string localiztion_id;
    [SerializeField] private bool _takeIdFromTextComponent = false;
    private TMP_Text _wordTmp;

    private void Awake()
    {
        if (TryGetComponent(out TMP_Text text_tmp))
        {
            _wordTmp = text_tmp;
        }

        if (_takeIdFromTextComponent)
        {
            localiztion_id = _wordTmp.text;
        }
    }
    private void Start()
    {
        LocalizationService.EventChangeLanguage += TranslateText;
        TranslateText();
    }
    private void OnDestroy()
    {
        LocalizationService.EventChangeLanguage -= TranslateText;
    }
    private void TranslateText()
    {
         _wordTmp.text = LocalizationService.GetTextTranslation(localiztion_id);
    }
}