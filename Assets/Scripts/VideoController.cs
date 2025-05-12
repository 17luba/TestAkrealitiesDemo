using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoScreen; // Le Quad
    private GameObject uiPanel;

    void Start()
    {
        videoScreen.SetActive(false);
        uiPanel = GameObject.FindGameObjectWithTag("Panel");

        // 📌 Abonnement à l'événement de fin
        videoPlayer.loopPointReached += OnVideoFinished;

        // 📌 Abonnement à l’événement lorsque les données vidéo sont prêtes
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    public void PlayVideo()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);

        videoScreen.SetActive(true);
        videoPlayer.Prepare(); // On prépare avant de jouer
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        AdjustQuadSizeToVideo();

        videoPlayer.Play();
    }

    private void AdjustQuadSizeToVideo()
    {
        float width = videoPlayer.texture.width;
        float height = videoPlayer.texture.height;

        if (width == 0 || height == 0)
        {
            Debug.LogWarning("La texture vidéo n'est pas encore prête.");
            return;
        }

        float aspectRatio = width / height;

        // 👉 Taille "perçue" dans l'espace XR
        float heightInWorldUnits = 50f; // Ajuste cette valeur selon tes préférences
        float widthInWorldUnits = heightInWorldUnits * aspectRatio;

        videoScreen.transform.localScale = new Vector3(widthInWorldUnits, heightInWorldUnits, 1f);
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        videoScreen.SetActive(false);

        if (uiPanel != null)
            uiPanel.SetActive(true);
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        StopVideo();
    }
}
