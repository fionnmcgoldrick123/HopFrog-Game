using UnityEngine;

public class BackGroundScript : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    public float _parallaxEffectMultiplier = 0.5f;
    void Start()
    {
        if(_cameraTransform == null)
        {
            _cameraTransform = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(_cameraTransform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
