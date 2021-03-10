using UnityEngine;

namespace Scenes.SampleShooting
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 2;
        [SerializeField] private float lifeTime = 10;

        private float _totalTime;
        private void OnEnable()
        {
            _totalTime = 0;
        }

        private void Update()
        {
            _totalTime += Time.deltaTime;

            transform.localPosition += transform.localRotation * Vector3.up * speed;
        }
    }
}
