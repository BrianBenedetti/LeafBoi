using UnityEngine;

// where T is the data type used to label
public class LabelledState<T> : State
{
    public T Label
    {
        get; 
    }

    public LabelledState (T stateName)
    {
        Label = stateName;
    }
}
