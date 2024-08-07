using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    private Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();
    private Dictionary<string, Action<object>> eventDictionaryWithParam = new Dictionary<string, Action<object>>();
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 在场景中查找现有的 GameManager 实例
                instance = FindObjectOfType<EventManager>();
                
                // 如果找不到，创建一个新的 GameObject，并添加 GameManager 组件
                if (instance == null)
                {
                    GameObject go = new GameObject("EventManager");
                    instance = go.AddComponent<EventManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // 确保不销毁单例对象
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 确保只有一个单例实例存在
        }
    }

    // 无参数的事件注册
    public void StartListening(GameEvent eventName, Action listener)
    {
        string eventKey = eventName.ToString();
        if (eventDictionary.TryGetValue(eventKey, out Action thisEvent))
        {
            thisEvent += listener;
            eventDictionary[eventKey] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            eventDictionary.Add(eventKey, thisEvent);
        }
    }

    // 带参数的事件注册
    public void StartListening(GameEvent eventName, Action<object> listener)
    {
        string eventKey = eventName.ToString();
        if (eventDictionaryWithParam.TryGetValue(eventKey, out Action<object> thisEvent))
        {
            thisEvent += listener;
            eventDictionaryWithParam[eventKey] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            eventDictionaryWithParam.Add(eventKey, thisEvent);
        }
    }

    // 无参数的事件取消注册
    public void StopListening(GameEvent eventName, Action listener)
    {
        if (Instance == null) return;

        string eventKey = eventName.ToString();
        if (eventDictionary.TryGetValue(eventKey, out Action thisEvent))
        {
            thisEvent -= listener;
            if (thisEvent == null)
            {
                eventDictionary.Remove(eventKey);
            }
            else
            {
                eventDictionary[eventKey] = thisEvent;
            }
        }
    }

    // 带参数的事件取消注册
    public void StopListening(GameEvent eventName, Action<object> listener)
    {
        if (Instance == null) return;

        string eventKey = eventName.ToString();
        if (eventDictionaryWithParam.TryGetValue(eventKey, out Action<object> thisEvent))
        {
            thisEvent -= listener;
            if (thisEvent == null)
            {
                eventDictionaryWithParam.Remove(eventKey);
            }
            else
            {
                eventDictionaryWithParam[eventKey] = thisEvent;
            }
        }
    }

    // 触发无参数的事件
    public void TriggerEvent(GameEvent eventName, float delay = 0.0f)
    {
        Debug.Log("Trigger Event !!! : " + eventName);
        string eventKey = eventName.ToString();
        
        if (eventDictionary.TryGetValue(eventKey, out Action thisEvent))
        {
            DOVirtual.DelayedCall(delay, () =>
            {
                Debug.Log("Event triggered after delay: " + eventName);
                thisEvent.Invoke();
            });
        }
        else
        {
            Debug.LogWarning("Event not found: " + eventName);
        }
    }

    // 触发带参数的事件
    public void TriggerEvent(GameEvent eventName, object parameter)
    {
        string eventKey = eventName.ToString();
        if (eventDictionaryWithParam.TryGetValue(eventKey, out Action<object> thisEvent))
        {
            thisEvent.Invoke(parameter);
        }
    }
}
