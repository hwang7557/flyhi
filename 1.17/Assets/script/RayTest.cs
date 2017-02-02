using UnityEngine;
using System.Collections;

public class RayTest : MonoBehaviour
{

    [Range(0.5f, 2.0f)]
    public float distance = 2.0f;

    public GameObject Carobj;
    private RaycastHit[] rayForward;
    private RaycastHit[] rayLeft;
    private RaycastHit[] rayRight;


    //public LayerMask maskValue = (-1);
    public LayerMask maskValue = (-1) - ((1 << LayerMask.NameToLayer("Enemy")));

    void Update()
    {
        Ray ray = new Ray();
        ray.origin = this.transform.position;
        ray.direction = transform.forward;

        this.rayForward = Physics.RaycastAll(
            ray,
            this.distance,
            this.maskValue.value);

        ray.direction = this.transform.forward;

        this.rayLeft = Physics.RaycastAll(
            ray,
            this.distance,
            this.maskValue.value      /// : 특정 마스크만 지정하고 싶을때 -1 이면 everything
        );

        //this.rayHits = Physics.RaycastAll(ray, this.distance); 

        ray.direction = this.transform.right;

        this.rayRight = Physics.RaycastAll(
            ray,
            this.distance,
            this.maskValue.value      /// : 특정 마스크만 지정하고 싶을때 -1 이면 everything
        );

        RayCheck();

    }

    void RayCheck()
    {
        if (this.rayForward != null && this.rayForward.Length > 0)    /// : null 비교 안해서 추가
        {
            for (int i = 0; i < this.rayForward.Length; i++)
            {
                if (this.rayForward[i].collider != null)
                {
                    RotateCar();
                    return;
                }
            }
        }


        if (this.rayLeft != null && this.rayLeft.Length > 0)    /// : null 비교 안해서 추가
        {
            for (int i = 0; i < this.rayLeft.Length; i++)
            {
                if (this.rayLeft[i].collider != null)
                {
                    Carobj.transform.Rotate(Vector3.up, 1.0f);
                    return;
                }
            }
        }

        if (this.rayRight != null && this.rayRight.Length > 0)    /// : null 비교 안해서 추가
        {
            for (int i = 0; i < this.rayRight.Length; i++)
            {
                if (this.rayRight[i].collider != null)
                {
                    Carobj.transform.Rotate(Vector3.up, -1.0f);
                    return;
                }
            }
        }
    }

    void RotateCar()
    {
        bool isLeftRotate = false;

        if (this.rayLeft != null && this.rayLeft.Length > 0)    /// : null 비교 안해서 추가
        {
            for (int i = 0; i < this.rayLeft.Length; i++)
            {
                if (this.rayLeft[i].collider != null)
                {
                    isLeftRotate = false;
                }
            }
        }
        if (this.rayRight != null && this.rayRight.Length > 0)    /// : null 비교 안해서 추가
        {
            for (int i = 0; i < this.rayRight.Length; i++)
            {
                if (this.rayRight[i].collider != null)
                {
                    isLeftRotate = true;
                }
            }
        }

        if (isLeftRotate)
        {
            Vector3 reflect =                                       /// : 반사 벡터 구하기
                        Vector3.Reflect(transform.forward,
                    rayRight[0].normal);

            float angle = ContAngle(transform.forward, reflect);

            float CalAngle = Mathf.Lerp(Carobj.transform.rotation.y, angle, 0.2f);

            Carobj.transform.Rotate(Vector3.up, CalAngle);

        }
        else
        {
            Vector3 reflect =                                       /// : 반사 벡터 구하기
                        Vector3.Reflect(transform.forward,
                   rayLeft[0].normal);

            float angle = ContAngle(transform.forward, reflect);

            float CalAngle = Mathf.Lerp(Carobj.transform.rotation.y, angle, 0.2f);

            Carobj.transform.Rotate(Vector3.up, CalAngle);
        }
    }

    void OnDrawGizmos()
    {
        if (rayForward != null && rayForward.Length > 0)    /// : null 비교 안해서 추가
        {
            for (int i = 0; i < rayForward.Length; i++)
            {
                if (rayForward[i].collider != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(
                        rayForward[i].point, 1.0f
                        );

                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(
                        transform.position,
                        transform.position +
                        transform.forward * rayForward[i].distance
                        );

                    //Gizmos.color = Color.yellow;
                    //Gizmos.DrawLine(rayHits[i].point,
                    //    rayHits[i].point + rayHits[i].normal          /// : 노말 벡터 
                    //    );

                    //Gizmos.color = new Color(1.0f, 0.0f, 1.0f);
                    //Vector3 reflect =                                       /// : 반사 벡터 구하기
                    //    Vector3.Reflect(transform.forward,
                    //    rayHits[i].normal);

                    //Life1 = ContAngle(transform.forward, reflect);

                    //Gizmos.DrawLine(rayHits[i].point,
                    //    rayHits[i].point + reflect);               /// : 반사됬을때의 벡터
                }
                else
                {
                    Gizmos.DrawLine(transform.position,
                        transform.position +
                        transform.forward * distance
                        );
                }

            }

        }
    }


    public float ContAngle(Vector3 fwd, Vector3 targetDir)
    {
        float angle = Vector3.Angle(fwd, targetDir);

        if (AngleDir(fwd, targetDir, Vector3.up) == -1)
        {
            //angle = 360.0f - angle;
            //if (angle > 359.9999f)
            //    angle -= 360.0f;
            return -angle;
        }
        else
            return angle;
    }

    public int AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0)
            return 1;
        else if (dir < 0.0)
            return -1;
        else
            return 0;
    }
}
