using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region CONTROLLERS
    public CanvasController CanvasController;
    public PlayerController PlayerController;
    public ObjectFinder ObjectFinder;
    public RaycastController RaycastController;
    public InteractionController InteractionController;
    public WeaponController WeaponController;
    #endregion

    public IPlayerInputFacade PlayerInputFacade = new PlayerInputAdapter();

    private Factory<IGameState> _gameStateFactory = new Factory<IGameState>();
    private IGameState _currentState;

    #region
    private IGameState _gameplayState = new GameplayState();
    #endregion
    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        CanvasController.Initialize();
        ChangeState(GameStateName.GAMEPLAY);
    }

    private void Update()
    {
        _currentState.Tick();
    }

    private void FixedUpdate()
    {
        _currentState.FixedTick();
    }
    public void ChangeState(GameStateName state)
    {
        if (_currentState != null)
        {
            _currentState.OnStateExit();
        }
        _currentState = _gameStateFactory.Create(TranslateGameStateName(state));
        Debug.Log("State changed to: " + _currentState);
        _currentState.OnStateEnter();
    }

    private IGameState TranslateGameStateName(GameStateName gameStateName)
    {
        if (gameStateName == GameStateName.GAMEPLAY) return _gameplayState;

        Debug.LogError("CUSTOM ERROR: state name not recognized, returned null");
        return null;
    }


}

public class Factory<T>
{
    public T Create(T itemToCreate)
    {
        return itemToCreate;
    }
}