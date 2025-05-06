using System.Collections.Generic;
using DG.Tweening;
using UI.Table.Row;
using UnityEngine;
using UnityEngine.Serialization;

public class TableController : MonoBehaviour
{
    public bool IsOpened { get; private set; }
    
    [SerializeField] private float _scalingTime;

    [Space, SerializeField] private Ease _scalingEase;
    
    [Space, SerializeField] private Transform _rowsContainer;

    [Space, SerializeField] private Row _rowPrefab;

    private Tween _scalingTween;

    private List<Row> _rows;
    
    public void Show()
    {
        if (_scalingTween is not null && _scalingTween.IsActive())
        {
            _scalingTween.Kill();
        }

        transform.localScale = Vector3.zero;

        gameObject.SetActive(true);

        _scalingTween = transform.DOScale(Vector3.one, _scalingTime).SetEase(_scalingEase);
        _scalingTween.Play();

        IsOpened = true;
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
        });
        _scalingTween.Play();
        
        IsOpened = false;
    }

    public void AddRow(RowData rowData)
    {
        var newRow = Instantiate(_rowPrefab, _rowsContainer);
        
        newRow.Initialize(rowData.StartT.ToString("F1"), rowData.EndT.ToString("F1"), rowData.Power.ToString("F1"), rowData.Time.ToString("F1"), rowData.OutsideT.ToString("F1"), rowData.Waternesy.ToString("F1"), rowData.DeltaT.ToString("F1"));
    }
}
