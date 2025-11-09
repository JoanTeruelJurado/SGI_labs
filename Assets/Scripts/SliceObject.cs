using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Transform StartSlicePoint;
    public Transform EndSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

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
            SetupSlocedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, CrossSectionMaterial);
            SetupSlocedComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSlocedComponent(GameObject slicedObject) {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
