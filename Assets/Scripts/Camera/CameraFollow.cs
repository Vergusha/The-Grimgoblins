using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;
    [Header("Camera Settings")]
    public float smoothSpeed = 5f;
    [Header("Dead Zone Settings")]
    public Vector2 deadZoneSize = new Vector2(2f, 2f);

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 cameraPos = transform.position;
        Vector3 targetPos = target.position;

        // Рассчитываем смещение между камерой и игроком
        Vector2 offset = new Vector2(targetPos.x - cameraPos.x, targetPos.y - cameraPos.y);

        // Проверяем, вышел ли игрок за пределы dead zone
        Vector3 desiredPosition = cameraPos;
        bool needMove = false;
        if (Mathf.Abs(offset.x) > deadZoneSize.x / 2)
        {
            desiredPosition.x = targetPos.x - Mathf.Sign(offset.x) * deadZoneSize.x / 2;
            needMove = true;
        }
        if (Mathf.Abs(offset.y) > deadZoneSize.y / 2)
        {
            desiredPosition.y = targetPos.y - Mathf.Sign(offset.y) * deadZoneSize.y / 2;
            needMove = true;
        }
        // Плавное движение только если нужно
        if (needMove)
            transform.position = Vector3.Lerp(cameraPos, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // В редакторе рисуем область dead zone
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(deadZoneSize.x, deadZoneSize.y, 0));
    }
}
