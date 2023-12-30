using UnityEngine;

public class InitializingMap : MonoBehaviour
{
    private Vector3 targetPosition => this.transform.position;
    private BackgroundLoop background;
    private BackgroundLoop Background
    {
        get
        {
            if(background == null)
                background = GetComponentInParent<BackgroundLoop>();
            return background;
        }
    }
    private bool isSpeed = false;

    private void OnEnable()
    {
        isSpeed = true;
    }
    private void OnDisable()
    {
        isSpeed = false;
        
    }

    void Update()
    {
        if (isSpeed)
        {
            if (this.targetPosition.x > -41f)
                this.transform.Translate(new Vector3(-1 * Background.Speed, 0f, 0f));
            else
            {
                Background.RemoveActiveMap(this);
                Background.CreateMap();
                this.gameObject.SetActive(false);
            }
        }
    }
}
