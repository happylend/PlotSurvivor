using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;

    [SerializeField]
    private Transform damageShowPrefab;
    public static Transform _damageShowPrefab;

    private float disappearTime;
    private Color textColor;
    
    void Awake()
    {
        if (textMesh == null)  { textMesh = transform.GetComponent<TextMeshPro>(); }


    }

    void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_damageShowPrefab == null) { _damageShowPrefab = damageShowPrefab; }
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 20f;
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

        disappearTime -= Time.deltaTime;
        if (disappearTime <= 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Setup(int damageAmout, bool isCriticalHit)
    {
        textMesh.SetText(damageAmout.ToString());
        if(!isCriticalHit)
        {
            textMesh.fontSize = 8;
            textColor = new Color(120, 120, 120);
        }
        else
        {
            textMesh.fontSize = 12;
            textColor = Color.red;
        }

        textMesh.color = textColor;
        disappearTime = 1f;
    }

    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTranform = Instantiate(_damageShowPrefab, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTranform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);

        return damagePopup;
    }
}
