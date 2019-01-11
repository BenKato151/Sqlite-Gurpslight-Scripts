using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesScripts : MonoBehaviour {

    #region Loading
    public void LoadSceneScript()
    {
        SceneManager.LoadScene("Loading");
    }
    #endregion

    #region LoadInsertScenes

    public void LoadInsertAbwehrScene()
    {
        SceneManager.LoadScene("SqliteInsertAbwehr");
    }

    public void LoadInsertAttributeScene()
    {
        SceneManager.LoadScene("SqliteInsertAttribute");
    }

    public void LoadInsertAttributskostenScene()
    {
        SceneManager.LoadScene("SqliteInsertAttributskosten");
    }

    public void LoadInsertAverScene()
    {
        SceneManager.LoadScene("SqliteInsertAVerteidigung");
    }

    public void LoadInsertFertigkeitenScene()
    {
        SceneManager.LoadScene("SqliteInsertFertigkeiten");
    }

    public void LoadInsertPlayerScene()
    {
        SceneManager.LoadScene("SqliteInsertPlayer");
    }

    public void LoadInsertRUScene()
    {
        SceneManager.LoadScene("SqliteInsertRuestungSchutz");
    }

    public void LoadInsertWaffenScene()
    {
        SceneManager.LoadScene("SqliteInsertWaffen");
    }
    #endregion

    #region LoadUpdateScenes
    public void LoadUpdateAbwehrScene()
    {
        SceneManager.LoadScene("SqliteUpdateAbwehr");
    }

    public void LoadUpdateAttributeScene()
    {
        SceneManager.LoadScene("SqliteUpdateAttribute");
    }

    public void LoadUpdateAttributskostenScene()
    {
        SceneManager.LoadScene("SqliteUpdateAttributskosten");
    }

    public void LoadUpdateAverScene()
    {
        SceneManager.LoadScene("SqliteUpdateAVerteidigung");
    }

    public void LoadUpdateFertigkeitenScene()
    {
        SceneManager.LoadScene("SqliteUpdateFertigkeiten");
    }

    public void LoadUpdatePlayerScene()
    {
        SceneManager.LoadScene("SqliteUpdatePlayer");
    }

    public void LoadUpdateRUScene()
    {
        SceneManager.LoadScene("SqliteUpdateRuestungSchutz");
    }

    public void LoadUpdateWaffenScene()
    {
        SceneManager.LoadScene("SqliteUpdateWaffen");
    }
    #endregion

}
