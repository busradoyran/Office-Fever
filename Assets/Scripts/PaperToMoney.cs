using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperToMoney : MonoBehaviour
{
    public PrinterManager printerManager;

    public List<GameObject> moneyList = new List<GameObject>();
    public List<GameObject> receivedPaperList = new List<GameObject>();

    public GameObject paper, money;
    public Transform paperPoint, moneyDropPoint;
    public bool isWorking;

    private void Start()
    {
        StartCoroutine(GenerateMoney());
    }
    IEnumerator GivePaper()
    {
        while (isWorking)
        {
            if (printerManager.collectedPaperList.Count > 0)
            {
                GetPaper();
                Destroy(printerManager.collectedPaperList[printerManager.collectedPaperList.Count - 1]);
                printerManager.collectedPaperList.RemoveAt(printerManager.collectedPaperList.Count - 1);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public void GetPaper()
    {
        GameObject temp = Instantiate(paper);
        temp.transform.position = new Vector3(paperPoint.position.x, ((float)receivedPaperList.Count / 20) + 0.95f, paperPoint.position.z);
        receivedPaperList.Add(temp);
    }
    IEnumerator GenerateMoney()
    {
        while (true)
        {
            if (receivedPaperList.Count > 0)
            {
                GameObject temp = Instantiate(money);
                temp.transform.position = new Vector3(moneyDropPoint.position.x, ((float)moneyList.Count / 40) + 0.25f, moneyDropPoint.position.z);
                moneyList.Add(temp);
                RemoveLast();
            }
            yield return new WaitForSeconds(2f);
        }
    }
    
    public void RemoveLast()
    {
        if (receivedPaperList.Count > 0)
        {
            Destroy(receivedPaperList[receivedPaperList.Count - 1]);
            receivedPaperList.RemoveAt(receivedPaperList.Count - 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isWorking = true;
            StartCoroutine(GivePaper());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isWorking = false;
        }
    }
}
