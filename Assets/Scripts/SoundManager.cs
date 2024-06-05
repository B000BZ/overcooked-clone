using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipsListSO audioClipsListSO;
    private float volume = 1f;

  

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ClearCounter.OnPlayerItemPickup += ClearCounter_OnPlayerItemPickup;
        ClearCounter.OnPlayerItemDrop += ClearCounter_OnPlayerItemDrop;
        ContainerCounter.OnPlayerItemPickup += ContainerCounterOnPlayerItemPickup;
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManagerOnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        CuttingCounter.OnPlayerItemPickup += CuttingCounter_OnPlayerItemPickup;
        CuttingCounter.OnPlayerItemDrop += CuttingCounter_OnPlayerItemDrop;
        StoveCounter.OnPlayerItemPickup += StoveCounter_OnPlayerItemPickup;
        StoveCounter.OnPlayerItemDrop += StoveCounter_OnPlayerItemDrop;
        TrashCounter.OnPlayerItemDrop += TrashCounter_OnPlayerItemDrop;
        PlatesCounter.OnPlayerItemPickup += PlatesCounter_OnPlayerItemPickup;
    }

    private void PlatesCounter_OnPlayerItemPickup(object sender, System.EventArgs e)
    {
        PlatesCounter platesCounter = sender as PlatesCounter;
        PlaySound(audioClipsListSO.itemPickup, platesCounter.transform.position);
    }

    private void TrashCounter_OnPlayerItemDrop(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipsListSO.trash, trashCounter.transform.position);

           
    }

    private void StoveCounter_OnPlayerItemPickup(object sender, System.EventArgs e)
    {
        StoveCounter stoveCounter = sender as StoveCounter;
        PlaySound(audioClipsListSO.itemPickup, stoveCounter.transform.position);

    }

    private void StoveCounter_OnPlayerItemDrop(object sender, System.EventArgs e)
    {
        StoveCounter stoveCounter = sender as StoveCounter;
        PlaySound(audioClipsListSO.itemDrop, stoveCounter.transform.position);
    }

    private void ClearCounter_OnPlayerItemPickup(object sender, System.EventArgs e)
    {
        ClearCounter clearCounter = sender as ClearCounter;
        PlaySound(audioClipsListSO.itemPickup, clearCounter.transform.position);
    }

    private void ClearCounter_OnPlayerItemDrop(object sender, System.EventArgs e)
    {
        ClearCounter clearCounter = sender as ClearCounter;
        PlaySound(audioClipsListSO.itemDrop, clearCounter.transform.position);
    }

    private void CuttingCounter_OnPlayerItemPickup(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipsListSO.itemPickup, cuttingCounter.transform.position);
    }

    private void CuttingCounter_OnPlayerItemDrop(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipsListSO.itemDrop, cuttingCounter.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipsListSO.chop, cuttingCounter.transform.position);
    }

    private void ContainerCounterOnPlayerItemPickup(object sender, System.EventArgs e)
    {
        ContainerCounter containerCounter = sender as ContainerCounter;
        PlaySound(audioClipsListSO.itemPickup, containerCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFail(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsListSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManagerOnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsListSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volume);
    }

    public void PlayFootstepsSound(Vector3 position)
    {
        PlaySound(audioClipsListSO.footsteps, position);
    }

    public void ChangeVolume(float volume)
    {
        this.volume = volume;
    }
}
    