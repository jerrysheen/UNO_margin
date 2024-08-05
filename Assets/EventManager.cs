using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();
    private Dictionary<string, Action<object>> eventDictionaryWithParam = new Dictionary<string, Action<object>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
    public void TriggerEvent(GameEvent eventName)
    {
        string eventKey = eventName.ToString();
        if (eventDictionary.TryGetValue(eventKey, out Action thisEvent))
        {
            thisEvent.Invoke();
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
