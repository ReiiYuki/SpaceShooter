using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnterGame : MonoBehaviour {

	public void Enter()
    {
        SceneManager.LoadScene(1);
    }
}
