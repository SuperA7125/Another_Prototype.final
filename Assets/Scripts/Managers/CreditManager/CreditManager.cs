using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public GameObject CreditPanel;

    private void Start()
    {
        CreditPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && CreditPanel.activeSelf)
        {
            CreditPanel.SetActive(false);
        }
    }
}
