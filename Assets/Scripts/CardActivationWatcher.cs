using UnityEngine;
using UnityEngine.Events;

public class CardActivationWatcher : MonoBehaviour
{
    [System.Serializable]
    public class ActivationEvent
    {
        public GameObject targetObject;
        public UnityEvent onActivated;
    }

    public ActivationEvent[] objectsToWatch;

    private bool[] wasActive;

    void Start()
    {
        if (objectsToWatch.Length > 0)
        {
            wasActive = new bool[objectsToWatch.Length];
            for (int i = 0; i < objectsToWatch.Length; i++)
            {
                // Initialize the wasActive array with current active states
                wasActive[i] = objectsToWatch[i].targetObject.activeSelf;
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < objectsToWatch.Length; i++)
        {
            bool currentlyActive = objectsToWatch[i].targetObject.activeSelf;
            if (!wasActive[i] && currentlyActive)
            {
                // Object was inactive and is now active
                objectsToWatch[i].onActivated.Invoke();
            }
            // Update the wasActive status
            wasActive[i] = currentlyActive;
        }
    }
}