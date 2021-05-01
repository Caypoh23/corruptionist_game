using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float cashAmount;
    [SerializeField] private Tag cashTag;
    [SerializeField] private GameObject cashObject;

    public void OnMouseDown()
    {
        cashObject.SetActive(false);
        //TODO: сделать так чтоб цифра денег спавнилась 1 раз а не 100500 раз пока игрок кликает на тригер
        // можно сделать через бул или установить количество клика на 1 раз используя int
        ObjectPooler.Instance.SpawnFromPool(cashTag, transform.position, transform.rotation);
    }
}