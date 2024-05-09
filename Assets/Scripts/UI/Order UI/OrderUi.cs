using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class OrderUi : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Outline _orderOutline;
    [SerializeField] private Image _orderDoneEffect;
    [SerializeField] private Image _dishIcon;
    [SerializeField] private Image[] _ingrediantsIcon;
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private RectTransform _orderHolder;
    [SerializeField] private RectTransform[] _ingrediantsParent;

    private float _timer;
    private float _currentTime;
    private bool _startTime;

    private RecipeSO _recipe;

    public void Init(RecipeSO recipe)
    {
        _recipe = recipe;
        _dishIcon.sprite = recipe.recipeIcon;
        _timer = recipe.earnedSettings.timeInterval.z;
      
        int minutes = Mathf.FloorToInt(_timer / 60);
        int seconds = Mathf.FloorToInt(_timer % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //Holder Move to position 0
        _orderHolder.DOAnchorPosX(0f, 0.5f).SetEase(Ease.InOutCirc).OnComplete(() =>
        {
            //Show ingrediants using Dotween animation
            for (int i = 0; i < recipe.kitchenObjectSOList.Count; i++)
            {
                _ingrediantsParent[i].gameObject.SetActive(true);

                _ingrediantsIcon[i].sprite = recipe.kitchenObjectSOList[i].sprite;

                _ingrediantsParent[i].DOAnchorPosY(-97f, 0.3f)
                .SetEase(Ease.OutBack).SetDelay(0.3f * i);
            }
        });
    }

    public void OrderDone()
    {
        //Stop Time
        _startTime = false;

        //Calculate Earned Coins
        CalculateEarning();

        _orderDoneEffect.DOFade(0.80f, 0.15f).OnComplete(() =>
        {
            _orderDoneEffect.DOFade(0f, 0.15f).OnComplete(() =>
            {
                _orderHolder.DOAnchorPosY(1000f, 0.7f).SetEase(Ease.InOutCirc)
                .OnComplete(() =>
                {
                    //Remove order from list & Destroy it
                    OrdersUiManager.instance.RemoveOrder(this);
                });
            });
        });
    }


    bool m_startEffect = true;

    public void ActiveOrder(bool isActive = false)
    {
        if (isActive)
        {
            _canvasGroup.alpha = 1f;

            _orderOutline.enabled = true;
            StartCoroutine(Loop());

            StartTimer();
        }
        else
        {
            _orderOutline.enabled = false;
            StopCoroutine(Loop());
        }

        IEnumerator Loop()
        {
            float m_size = m_startEffect ? 4f : 2f;
            float m_currentSize = m_startEffect ? 2f : 4f;

            while (m_startEffect ? (m_currentSize < m_size) : (m_currentSize >= m_size))
            {
                yield return null;

                if (m_startEffect)
                    m_currentSize += 0.1f;
                else
                    m_currentSize -= 0.1f;

                _orderOutline.effectDistance = new Vector2(m_currentSize, m_currentSize);
            }

            m_startEffect = !m_startEffect;
            StartCoroutine(Loop());
        }
    }

    public void CalculateEarning()
    {
       
    }

    public void StartTimer()
    {
        _timer = _recipe.earnedSettings.timeInterval.z;

        _startTime = true;
    }

    private void Update()
    {
        if (_startTime)
        {
            _timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(_timer / 60);
            int seconds = Mathf.FloorToInt(_timer % 60);

            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
