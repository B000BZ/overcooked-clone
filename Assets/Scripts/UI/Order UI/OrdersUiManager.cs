using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrdersUiManager : MonoBehaviour
{
    public static OrdersUiManager instance;

    public List<OrderUi> _orders = new List<OrderUi>();

    [SerializeField] private OrderUi _order;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RemoveOrder(_orders[0]);
    }

    public void DisplayOrder(RecipeSO recipe)
    {
        OrderUi m_order = Instantiate(_order, Vector2.zero, Quaternion.identity, transform);
        _orders.Add(m_order);

        m_order.Init(recipe);

        if (_orders.Count == 1)
            UpdateCurrentOrder();
    }

    public void OrderDone(OrderUi order)
    {
        order.OrderDone();
    }

    public void RemoveOrder(OrderUi orderUi)
    {
        _orders.Remove(orderUi);
        Destroy(orderUi.gameObject);

        UpdateCurrentOrder();
    }

    public void UpdateCurrentOrder()
    {
        _orders[0].ActiveOrder(true);
    }
}
