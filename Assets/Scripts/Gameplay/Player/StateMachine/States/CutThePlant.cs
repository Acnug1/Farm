using System.Collections;
using UnityEngine;

public class CutThePlant : State
{
    private const string PlayerCutConfigErrorMessage = "PlayerCutConfig is null";

    [Tooltip("—сылка на ScriptableObject: PlayerCutConfig")]
    [SerializeField] private PlayerCutConfig _playerCutConfig;

    private CutState _state = CutState.Ready;
    private Coroutine _cut;
    private float _cutDelay;

    private enum CutState
    {
        Ready,
        WaitingCutEnd
    }

    protected override void Awake()
    {
        Debug.Assert(_playerCutConfig != null, PlayerCutConfigErrorMessage);

        _cutDelay = _playerCutConfig.CutDelay;
    }

    protected override void OnStateExit()
    {
        PlayerAnimatorController.ResetCut();
    }

    protected override void Update()
    {
        TryCutThePlant();
    }

    private void TryCutThePlant()
    {
        if (_state == CutState.Ready)
        {
            if (_cut != null)
                StopCoroutine(_cut);

            _cut = StartCoroutine(Cut(_cutDelay));
        }
    }

    private IEnumerator Cut(float cutDelay)
    {
        PlayerAnimatorController.Cut();
        ChangeCutState(CutState.WaitingCutEnd);
        yield return new WaitForSeconds(cutDelay);
        ChangeCutState(CutState.Ready);
    }

    private void ChangeCutState(CutState changedState)
    {
        if (_state != changedState)
            _state = changedState;
    }
}
