using UnityEngine;
using UnityEngine.UI;

public class EnterGameplayButton : MonoBehaviour
{
     [SerializeField] private Button button;

     private void Awake()
     {
          button.onClick.AddListener(ExitGameplay);
     }

     private void OnDestroy()
     {
          button.onClick.RemoveListener(ExitGameplay);
     }

     private void ExitGameplay()
     {
          GameManager.Instance.TrySetState(GameManager.GameState.Gameplay);
     }
}