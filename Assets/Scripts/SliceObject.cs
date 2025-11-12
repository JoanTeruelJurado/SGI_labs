using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Transform StartSlicePoint;
    public Transform EndSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

    public AudioSource audio;
    public Material CrossSectionMaterial;
    public float cutForce = 2000.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hashit = Physics.Linecast(StartSlicePoint.position, EndSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hashit) {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target) {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(EndSlicePoint.position - StartSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(EndSlicePoint.position, planeNormal);

        if(hull != null) {
            GameObject upperHull = hull.CreateUpperHull(target, CrossSectionMaterial);
            SetupSlocedComponent(upperHull, target.layer);

            GameObject lowerHull = hull.CreateLowerHull(target, CrossSectionMaterial);
            SetupSlocedComponent(lowerHull, target.layer);
            
            if (GameController.Instance != null) GameController.Instance.addScore(100);
            audio.Play();
            Destroy(target);
        }
    }

    public void SetupSlocedComponent(GameObject slicedObject, int layer) {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
        slicedObject.layer = layer;
        Destroy(slicedObject, 10);
    }
}
