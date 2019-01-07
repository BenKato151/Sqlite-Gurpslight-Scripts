using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesScripts : MonoBehaviour {

    public bool msgShow = false;

    #region Loading
    public void LoadSceneScript()
    {
        SceneManager.LoadScene("Loading");
        if (msgShow)
        {
            Debug.Log("Loaded Main Window");
        }
    }
    #endregion

    #region LoadInsertScenes

    public void LoadInsertAbwehrScene()
    {
        SceneManager.LoadScene("SqliteInsertAbwehr");
        if (msgShow)
        {
            Debug.Log("Loaded InsertAbwehr");
        }
    }

    public void LoadInsertAttributeScene()
    {
        SceneManager.LoadScene("SqliteInsertAttribute");
        if (msgShow)
        { 
            Debug.Log("Loaded InsertAttribute");
        }
    }

    public void LoadInsertAttributskostenScene()
    {
        SceneManager.LoadScene("SqliteInsertAttributskosten");
        if (msgShow)
        {
            Debug.Log("Loaded InsertAttributskosten");
        }
    }

    public void LoadInsertAverScene()
    {
        SceneManager.LoadScene("SqliteInsertAVerteidigung");
        if (msgShow)
        {
            Debug.Log("Loaded InsertAktiveVerteidigung");
        }
    }

    public void LoadInsertFertigkeitenScene()
    {
        SceneManager.LoadScene("SqliteInsertFertigkeiten");
        if (msgShow)
        {
            Debug.Log("Loaded InsertFertigkeiten");
        }
    }

    public void LoadInsertPlayerScene()
    {
        SceneManager.LoadScene("SqliteInsertPlayer");
        if (msgShow)
        {
            Debug.Log("Loaded InsertPlayer");
        }
    }

    public void LoadInsertRUScene()
    {
        SceneManager.LoadScene("SqliteInsertRuestungSchutz");
        if (msgShow)
        {
            Debug.Log("Loaded InsertRuestungUndSchutz");
        }
    }

    public void LoadInsertWaffenScene()
    {
        SceneManager.LoadScene("SqliteInsertWaffen");
        if (msgShow)
        {
            Debug.Log("Loaded InsertWaffen");
        }
    }
    #endregion

    #region LoadUpdateScenes
    public void LoadUpdateAbwehrScene()
    {
        SceneManager.LoadScene("SqliteUpdateAbwehr");
        if (msgShow)
        {
            Debug.Log("Loaded UpdateAbwehr");
        }
    }

    public void LoadUpdateAttributeScene()
    {
        SceneManager.LoadScene("SqliteUpdateAttribute");
        if (msgShow)
        {
            Debug.Log("Loaded UpdateAttribute");
        }
    }

    public void LoadUpdateAttributskostenScene()
    {
        SceneManager.LoadScene("SqliteUpdateAttributskosten");
        if (msgShow)
        {
            Debug.Log("Loaded Attributskosten");
        }
    }

    public void LoadUpdateAverScene()
    {
        SceneManager.LoadScene("SqliteUpdateAVerteidigung");
        if (msgShow)
        {
            Debug.Log("Loaded UpdateAktiveVerteidigung");
        }
    }

    public void LoadUpdateFertigkeitenScene()
    {
        SceneManager.LoadScene("SqliteUpdateFertigkeiten");
        if (msgShow)
        {
            Debug.Log("Loaded UpdateFertigkeiten");
        }
    }

    public void LoadUpdatePlayerScene()
    {
        SceneManager.LoadScene("SqliteUpdatePlayer");
        if (msgShow)
        {
            Debug.Log("Loaded UpdatePlayer");
        }
    }

    public void LoadUpdateRUScene()
    {
        SceneManager.LoadScene("SqliteUpdateRuestungSchutz");
        if (msgShow)
        {
            Debug.Log("Loaded RuestungUndSchutz");
        }
    }

    public void LoadUpdateWaffenScene()
    {
        SceneManager.LoadScene("SqliteUpdateWaffen");
        if (msgShow)
        {
            Debug.Log("Loaded UpdateWaffen");
        }
    }
    #endregion
}
