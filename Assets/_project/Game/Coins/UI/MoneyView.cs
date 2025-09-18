using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyText;


        private Tween _scaleTween;

        public void UpdateCurrency(string currency)
        {
            _currencyText.text = currency;

            // Анимация масштаба текста
            _scaleTween?.Kill();
            _currencyText.rectTransform.localScale = Vector3.one;
            _scaleTween = _currencyText.rectTransform
                .DOScale(1.2f, 0.15f) // Увеличиваем
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _currencyText.rectTransform
                        .DOScale(1f, 0.15f) // Возвращаем обратно
                        .SetEase(Ease.InQuad);
                });
        }

        private void OnDestroy()
        {
            _scaleTween?.Kill();
        }
    }
}
