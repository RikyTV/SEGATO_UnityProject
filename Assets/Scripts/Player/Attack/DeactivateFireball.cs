using UnityEngine;

public class DeactivateFireball : MonoBehaviour {
    private void Deactivate() {
        transform.parent.gameObject.SetActive(false);
    }
}