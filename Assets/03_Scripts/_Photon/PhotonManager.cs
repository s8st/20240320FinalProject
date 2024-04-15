using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : /*MonoBehaviour*/ MonoBehaviourPunCallbacks
{
    //����
    private readonly string version = "1.0f";

    //����� ���̵�
    private string userId = "webglTester";

    private void Awake()
    {
        //���� ���� �����鿡�� �ڵ����� ���� �ε�
        PhotonNetwork.AutomaticallySyncScene = true;
        //���� ������ �������� ���� ���
        PhotonNetwork.GameVersion = version;
        //���� ���̵� �Ҵ�
        PhotonNetwork.NickName  = userId;

        //���� ������ ��� Ƚ�� ����. �ʴ� 30ȸ
        Debug.Log(PhotonNetwork.SendRate);
        //���� ����
        PhotonNetwork.ConnectUsingSettings();

    }

    //���� ������ ���� �� ȣ��Ǵ� �ݹ��Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); //�κ� �����ϸ� true, �ƴ϶�� false ,���⼭�� false
        PhotonNetwork.JoinLobby(); //�κ� ����
        
    }

    // �κ� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); //�κ� ���������� true, 
        PhotonNetwork.JoinRandomRoom();//���� ��ġ����ŷ ��� ����
    }

    //���� �� ���� �������� ��(�� ���� ��) �ݹ��Լ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // base.OnJoinRandomFailed(returnCode, message);
        Debug.Log($"JoinRandom Failed {returnCode} : {message}"); //returnCode : 32760
        //JoinRandom Failed 32760 : No match found
        //UnityEngine.Debug:Log(object)

        // �� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20; // �ִ� �÷��̾�
        ro.IsOpen = true; // �� ����
        ro.IsVisible = true; //�κ񿡼� �� ����

        //�� ����
        PhotonNetwork.CreateRoom("gogosing", ro);
        //PhotonNetwork.CreateRoom("My Room", ro);

    }

    // �� ���� �� ȣ��Ǵ� �ݹ��Լ�
    public override void OnCreatedRoom()
    {
        //base.OnCreatedRoom();
        Debug.Log("�游��� ����");
        Debug.Log($"�� �̸� : {PhotonNetwork.CurrentRoom.Name}");
    }

    // �� ���� �� ȣ��Ǵ� �ݹ��Լ�
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}"); // true , false
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}"); // �濡 ������ �÷��̾� ��

        // �濡 ������ ����� ���� Ȯ��
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName},{player.Value.ActorNumber}"); //����� ������, webglTester, 1
            //$ == String.Format()
        }


    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
