using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterManager : MonoBehaviour
{
    public List<GameObject> paperList = new List<GameObject>();
    public List<GameObject> collectedPaperList = new List<GameObject>();

    public GameObject paperPrefab;

    public Transform exitPoint;
    public Transform collectPoint;

    bool isWorking;
    bool isCollecting;

    int stackCount = 10;
    int paperLimit = 15;

    private void Start()
    {
        StartCoroutine(PrintPaper());
    }
    IEnumerator PrintPaper()
    {
        while (true)
        {
            float paperCount = paperList.Count;
            int rowCount = (int)paperCount / stackCount;

            if (isWorking)
            {
                GameObject temp = Instantiate(paperPrefab);
                temp.transform.position = new Vector3(exitPoint.position.x + (float)rowCount/3, (float)(paperCount % stackCount / 20 + 0.25f), exitPoint.position.z);
                paperList.Add(temp);

                if(paperList.Count >= 30)
                {
                    isWorking = false;
                }
            }
            else if(paperList.Count < 30)
            {
                isWorking = true;
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void RemoveLast()
    {
        if(paperList.Count > 0)
        {
            Destroy(paperList[paperList.Count - 1]);
            paperList.RemoveAt(paperList.Count - 1);
        }
    }

    IEnumerator CollectPaper()
    {
        while (isCollecting)
        {
            if (collectedPaperList.Count <= paperLimit && paperList.Count > 0)
            {
                GameObject temp = Instantiate(paperPrefab, collectPoint);
                temp.transform.position = new Vector3(collectPoint.position.x, ((float)collectedPaperList.Count / 20) + 0.75f, collectPoint.position.z);
                collectedPaperList.Add(temp);
                RemoveLast();
            }
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollecting = true;
            StartCoroutine(CollectPaper());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isCollecting = false;
    }
}
