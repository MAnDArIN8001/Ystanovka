using DG.Tweening;
using UnityEngine;

namespace UI.PopUp
{
    public class TimeEndPopUp : MonoBehaviour
    {
        [SerializeField] private float _scalingTime;

        [Space, SerializeField] private Ease _scalingEase;
        
        [Space, SerializeField] private TableController _table;

        private Vector3 _defaultScale = Vector3.one;

        private Tween _scalingTween;
        
        public void Show(float temperature)
        {
            if (_scalingTween is not null && _scalingTween.IsActive())
            {
                _scalingTween.Kill();
            }

            transform.localScale = Vector3.zero;

            gameObject.SetActive(true);

            _scalingTween = transform.DOScale(_defaultScale, _scalingTime).SetEase(_scalingEase);
            _scalingTween.Play();
        }

        public void Hide()
        {
            if (_scalingTween is not null && _scalingTween.IsActive())
            {
                _scalingTween.Kill();
            }

            _scalingTween = transform.DOScale(Vector3.zero, _scalingTime).SetEase(_scalingEase).OnComplete(() =>
            {
                gameObject.SetActive(false);
                
                _table.Show();
            });
            _scalingTween.Play();
        }
    }
}