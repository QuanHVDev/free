using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CoinAnimCanvas : BaseUIElement
{
    Queue<GameObject> coinQueue = new();
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject diamond;
    [SerializeField] private Transform diamondSpawn;
    private MainUI mainUI;

    private void Start()
    {
        mainUI = UIRoot.Ins.Get<MainUI>();
        InitQueue();
    }

    private void InitQueue()
    {
        diamond.gameObject.SetActive(false);
        for (int i = 0; i < 20; i++)
        {
            var obj = Instantiate(diamond.gameObject, diamondSpawn);
            coinQueue.Enqueue(obj);
        }
    }

    public void RunAnim(int amount)
    {
        var queue = coinQueue;
        var duration = 0.1f;
        int price = 1;
        if (amount / price > coinQueue.Count)
        {
            price = amount / coinQueue.Count;
        }

        for (int i = 0; i < amount / price; i++)
        {
            if (queue.Count > 0)
            {
                //SFX.Instance.PlayCurrencySpawning();
                GameObject diamond = queue.Dequeue();
                diamond.transform.parent.position = spawnPos.position;
                diamond.transform.localPosition = new(0, 0, 0);
                diamond.transform.localScale = Vector3.one; //new(0.7f, 0.7f, 0.7f);
                diamond.SetActive(true);

                var randomPos = new Vector3(UnityEngine.Random.Range(-70, 70), UnityEngine.Random.Range(-85, -100), 0);
                var toPos = mainUI.GetDiamondIconTransform();

                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(diamond.transform.DOLocalMove(randomPos, UnityEngine.Random.Range(0.6f, 0.5f))
                        .SetEase(Ease.OutBounce))
                    //.AppendInterval(0.4f)
                    .Append(diamond.transform.DOMove(toPos.position, duration)
                        .SetEase(Ease.InOutSine)
                        .OnComplete(() =>
                        {
                            SFX.Instance.PlayCurrencyCounting();
                            diamond.SetActive(false);
                            diamond.transform.localPosition = new(0, 0, 0);
                            queue.Enqueue(diamond);
                            mainUI.GetDiamondIconTransform().DOPunchScale(Vector3.one * 0.1f, 0.2f)
                                .SetEase(Ease.Linear).OnComplete(() => { mainUI.GetDiamondIconTransform().DOScale(Vector3.one, 0.1f); });
                        }));
                duration += 0.1f;
            }
        }
    }

    public override void OnAwake()
    {
        
    }
}