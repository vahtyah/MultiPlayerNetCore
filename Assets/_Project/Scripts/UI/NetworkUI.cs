using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button HostButton;   
    [SerializeField] private Button ClientButton;
    [SerializeField] private Button ServerButton;

    private void Start()
    {
        HostButton.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartHost();
            gameObject.SetActive(false);
        }));
        
        ClientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            gameObject.SetActive(false);
        });
        
        ServerButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            gameObject.SetActive(false);
        });
    }
}
