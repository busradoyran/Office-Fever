using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public PaperToMoney paperToMoney;
    public bool isCollecting;
    public int moneyAmount;

    public void RemoveLast(GameObject money)
    {
        if (paperToMoney.moneyList.Count > 0)
        {
            paperToMoney.moneyList.Remove(money);
            Destroy(money);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            moneyAmount++;
            RemoveLast(other.gameObject);
        }
    }
}
