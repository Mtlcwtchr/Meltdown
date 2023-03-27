using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
      private const string GameplaySceneName = "Scenes/GameplayScene";

      [SerializeField] private GameObject mainMenuUIRoot;

      [SerializeField] private UnityEvent OnGameplayStarted;
      [SerializeField] private UnityEvent OnGameplayEnd;
      
      private static GameManager _instance;
      public static GameManager Instance => _instance;

      private GameState _currentState;

      private void Awake()
      {
            _instance = this;
      }

      public void TrySetState(GameState state)
      {
            if (CanTransitState(_currentState, state))
            {
                  ProceedStateTransition(_currentState, _currentState = state);
            }
      }

      private void ProceedStateTransition(GameState from, GameState to)
      {
            switch (from)
            {
                  case GameState.Menu:
                        ExitMenu();
                        break;
                  case GameState.Gameplay:
                        ExitGameplay();
                        break;
                  
            }

            switch (to)
            {
                  case GameState.Menu:
                        EnterMenu();
                        break;
                  case GameState.Gameplay:
                        EnterGameplay();
                        break;
            }
      }

      private void EnterMenu()
      {
            mainMenuUIRoot.gameObject.SetActive(true);
      }

      private void ExitMenu()
      {
            mainMenuUIRoot.gameObject.SetActive(false);
      }

      private void EnterGameplay()
      {
            SceneManager.LoadSceneAsync(GameplaySceneName, LoadSceneMode.Additive);
            OnGameplayStarted?.Invoke();
      }

      private void ExitGameplay()
      {
            SceneManager.UnloadSceneAsync(GameplaySceneName);
            OnGameplayEnd?.Invoke();
      }

      private bool CanTransitState(GameState from, GameState to) =>
            from switch
            {
                  GameState.Menu => to == GameState.Gameplay,
                  GameState.Gameplay => to == GameState.Menu,
                  _ => false
            };
      
      public enum GameState
      {
            Menu,
            Gameplay
      }
}