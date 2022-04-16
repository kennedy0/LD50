using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTeardown : MonoBehaviour
{
    public GameObject ScreenFade;
    private static GameTeardown _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        ScreenFade.SetActive(false);
    }

    /// <summary>
    /// Tear down the game after it ends.
    /// </summary>
    public static IEnumerator TearDownGame(int rounds)
    {
        yield return _instance.FadeOut(rounds);
        _instance.Reload();
    }

    // ToDo - Remove after LD
    private IEnumerator FadeOut(int rounds)
    {
        yield return FallTiles();
        ScreenFade.SetActive(true);
        
        var image = ScreenFade.GetComponent<Image>();
        image.CrossFadeAlpha(0f, 0f, true);
        image.CrossFadeAlpha(1f, 3f, true);
        yield return new WaitForSeconds(3f);

        yield return TextBox.Find().ShowTextBox($"The fire went out.");
        yield return TextBox.Find().ShowTextBox($"You kept the fire lit for {rounds-1} rounds.");
        yield return new WaitForSeconds(3f);
    }

    private IEnumerator FallTiles()
    {
        CameraFollow.UnsetTarget();
        foreach (var cell in Board.Grid.AllCells())
        {
            var tile = cell.Tile;
            var bc = tile.gameObject.AddComponent<BoxCollider>();
            var rb = tile.gameObject.AddComponent<Rigidbody>();
            rb.AddTorque(Random.insideUnitSphere, ForceMode.Impulse);

            if (cell.Occupied)
            {
                cell.Actor.gameObject.AddComponent<BoxCollider>();
                cell.Actor.gameObject.AddComponent<Rigidbody>();
            }

            SoundEffects.SoundEffectsMaster.PlaySoft();
            yield return new WaitForSeconds(.01f);
        }
    }

    // ToDo - Remove after LD
    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
