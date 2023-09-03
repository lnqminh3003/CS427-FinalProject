using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MatchController : MonoBehaviourPunCallbacks
{
    public Transform startPos;
    public Transform defeatPos;
    public TMP_Text CounterNumber;
    public TMP_Text Command;
    public Image EnergyBar;
    public TMP_Text batteryCount;
    public TMP_Text generatorCount;
    public Interactable[] generators;

    private int currentGeneratorCount;
    private bool isGameOver = false;

    public float maxEnergy;
    public float energy
    {
        get
        {
            return _energy;
        }
    }
    private float _energy;

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameplayManager.instance.startPosition = startPos;
        GameplayManager.instance.defeatedRoom = defeatPos;
        currentGeneratorCount = 1;
        generatorCount.text = $"{currentGeneratorCount}/{generators.Length} generators activated.";
        _energy = maxEnergy;
        SetBatteryCount(0);

        for (int i = 0; i < GameplayManager.instance.playerList.Count; ++i)
        {
            if (GameplayManager.instance.playerList[i] == null) continue;
            GameplayManager.instance.playerList[i].gameObject.transform.position
                = startPos.position;
        }

        ModalWindowPanel.Instance.ShowModal("Survival game", null, "Welcome to the survival game! Collect the batteries and " +
            "recharge all of the generators before the energy bar runs out. Beware of the monsters also!", "Okay");
    }

    [PunRPC]
    void CommenceMatch()
    {
        ModalWindowPanel.Instance.ShowModal("Survival game", null, "Welcome to the survival game! Collect the batteries and " +
            "recharge all of the generators before the energy bar runs out. Beware of the monsters also!", "Okay");
    }

    public void SetBatteryCount(int count)
    {
        batteryCount.text = "x" + count;
    }

    [PunRPC]
    void SetEnergy(float amount)
    {
        _energy = Mathf.Clamp(amount, 0, maxEnergy);
        if (_energy <= 0) GameplayManager.instance.isGameOver = true;
        Vector3 scale = EnergyBar.GetComponent<RectTransform>().localScale;
        scale.x = _energy / maxEnergy;
        EnergyBar.GetComponent<RectTransform>().localScale = scale;
    }

    [PunRPC]
    public void ActivateGenerator()
    {
        currentGeneratorCount++;
        generatorCount.text = $"{currentGeneratorCount}/{generators.Length} generators activated.";
    }

    private void Update()
    {
        if (GameplayManager.instance.isGameOver) return;
        if (!PhotonNetwork.IsMasterClient) return;
        if (isGameOver) return;
        _energy -= 1f * Time.deltaTime;
        CheckGameStatus();
        if (_energy <= 0)
        {
            isGameOver = true;
            GameplayManager.instance.WinGame("The players have been defeated! Power's out permanently!");
        }
        photonView.RPC("SetEnergy", RpcTarget.All, _energy);
    }

    private void CheckGameStatus()
    {
        foreach (Interactable inter in generators)
        {
            if (!inter.isActivated) return;
        }
        isGameOver = true;
        GameplayManager.instance.WinGame("Players won! The office has been lit up completely!");
    }
}
