using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : /*MonoBehaviour*/ MonoBehaviourPunCallbacks
{
    //버전
    private readonly string version = "1.0f";

    //사용자 아이디
    private string userId = "webglTester";

    private void Awake()
    {
        //같은 룸의 유저들에게 자동으로 씬을 로딩
        PhotonNetwork.AutomaticallySyncScene = true;
        //같은 버전의 유저끼리 접속 허용
        PhotonNetwork.GameVersion = version;
        //유저 아이디 할당
        PhotonNetwork.NickName  = userId;

        //포톤 서버와 통신 횟수 설정. 초당 30회
        Debug.Log(PhotonNetwork.SendRate);
        //서버 접속
        PhotonNetwork.ConnectUsingSettings();

    }

    //포톤 서버에 접속 후 호출되는 콜백함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); //로비에 입장하면 true, 아니라면 false ,여기서는 false
        PhotonNetwork.JoinLobby(); //로비 입장
        
    }

    // 로비에 접속 후 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); //로비에 입장함으로 true, 
        PhotonNetwork.JoinRandomRoom();//랜덤 메치메이킹 기능 제공
    }

    //랜덤 방 입장 실패했을 때(방 없을 때) 콜백함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // base.OnJoinRandomFailed(returnCode, message);
        Debug.Log($"JoinRandom Failed {returnCode} : {message}"); //returnCode : 32760
        //JoinRandom Failed 32760 : No match found
        //UnityEngine.Debug:Log(object)

        // 방 속성 정의
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20; // 최대 플레이어
        ro.IsOpen = true; // 방 개방
        ro.IsVisible = true; //로비에서 방 공개

        //방 생성
        PhotonNetwork.CreateRoom("gogosing", ro);
        //PhotonNetwork.CreateRoom("My Room", ro);

    }

    // 방 생성 후 호출되는 콜백함수
    public override void OnCreatedRoom()
    {
        //base.OnCreatedRoom();
        Debug.Log("방만들기 성공");
        Debug.Log($"방 이름 : {PhotonNetwork.CurrentRoom.Name}");
    }

    // 방 입장 후 호출되는 콜백함수
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}"); // true , false
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}"); // 방에 입장한 플레이어 수

        // 방에 접속한 사용자 정보 확인
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName},{player.Value.ActorNumber}"); //별명과 고유값, webglTester, 1
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
