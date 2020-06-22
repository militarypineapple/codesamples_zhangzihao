using UnityEngine;
using UnityEngine.Events; // 1

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private GameEvent gameEvent; // 2
    [SerializeField]
    private UnityEvent response; // 3



    private void OnEnable() // 4
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable() // 5
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised() // 6
    {
        //debug tool to know all gameobjects that have event listener for the event invoked
        //if you want to get all gameobjs related to a event just invoke that event, and see in logger
        Debug.Log("invoking event:" + gameEvent + " from " + gameObject,gameObject);

        response.Invoke();
    }
}