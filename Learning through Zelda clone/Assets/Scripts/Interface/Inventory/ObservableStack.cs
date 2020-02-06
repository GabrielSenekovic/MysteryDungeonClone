using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public delegate void UpdateStackEvent();

public class ObservableStack<T> : Stack <T>
{
    public event UpdateStackEvent OnPush;
    public event UpdateStackEvent OnPop;
    public event UpdateStackEvent OnClear;

    public ObservableStack(ObservableStack<T> items) : base(items)
    {

    }
    public ObservableStack()
    {

    }

    public new void Push(T item)
    {
        Debug.Log("Pushing item");
        base.Push(item);
        if (OnPush != null)
        {
            OnPush();
        }
    }
    public new T Pop()
    {
        Debug.Log("Popping item");
        T item = base.Pop();
        if(OnPop != null)
        {
            OnPop();
        }
        return item;
    }
    public new void Clear()
    {
        Debug.Log("Clearing item");
        base.Clear();
        if(OnClear != null)
        {
            OnClear();
        }
    }
}
