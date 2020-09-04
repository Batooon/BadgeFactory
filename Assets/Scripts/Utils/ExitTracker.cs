using UnityEngine;
using UnityEngine.Events;

public class ExitTracker : MonoBehaviour
{
    [SerializeField] private UnityEvent OnExitButtonPressed;
    [SerializeField] private UnityEvent OnExitPromtWindowRejected;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExitButtonPressed?.Invoke();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void DoNotExit()
    {
        OnExitPromtWindowRejected?.Invoke();
    }
}
