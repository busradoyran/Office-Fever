using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyManager : MonoBehaviour
{
    public Image progressImage;
    public GameObject deskGameObject, buyGameObject;
    public CollectManager collectManager;
    public float progress;
    public float cost = 50;

    public void Buy()
    {
        progress = collectManager.moneyAmount / cost;
        progressImage.fillAmount = progress;

        if (progress >= 1)
        {
            buyGameObject.SetActive(false);
            deskGameObject.SetActive(true);
            this.enabled = false;
            collectManager.moneyAmount = collectManager.moneyAmount - (int)cost;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Buy();
        }
    }
}
