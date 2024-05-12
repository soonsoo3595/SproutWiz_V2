using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GriffonObject : MonoBehaviour, IPointerClickHandler
{
    private Vector2 minPosition;
    private Vector2 maxPosition;
    public float speed = 2.0f;

    private Vector3 targetPosition;

    private Image image;
    private Animator animator;

    private GriffonGame Master;

    private bool isActive;

    MainGame mainGame;

    void Start()
    {
        SetRandomTargetPosition();

        maxPosition = GridManager.Instance.GetWorldPosition(new GridPosition(4, 4));
        minPosition = GridManager.Instance.GetWorldPosition(new GridPosition(0, 0));

        image = GetComponent<Image>();
        animator = GetComponent<Animator>();

        isActive = true;

        mainGame = FindAnyObjectByType<MainGame>();
    }

    void Update()
    {
        if (mainGame.isPaused)
        {
            animator.speed = 0f;
            return;
        }
        else if(!mainGame.isPaused && animator.speed == 0f) 
        {
            animator.speed = 1f;
        }
            

        MoveToTargetPosition();

        // 목표 위치에 도달하면 새로운 위치 설정
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();

            if (targetPosition.x > transform.position.x) 
            {

                image.rectTransform.localScale = new Vector3(-1, 1, 1);
            }
            else if (targetPosition.x < transform.position.x) 
            {
                image.rectTransform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void SetMaster(GriffonGame master)
    {
        Master = master;
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        targetPosition = new Vector3(randomX, randomY, transform.position.z);
    }

    void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if(isActive)
        {
            EventManager.recordUpdate(RecordType.Griffon);
            GameManager.Instance.soundEffect.PlayOneShotSoundEffect("griffin");

            animator.SetTrigger("DeadTrigger");
            speed = 0.5f;

            // 점수 갱신
            EventManager.miniGameSuccess(EMinigameType.Griffon, -1);

            //Master.PlayEffect(transform.position);
            Master.ChatchGriffon(this.gameObject);

            if (GameManager.Instance.isTutorial)
            { 
                Master.TutorialSuccess(); 
            }

            isActive = false;

            StartCoroutine(DestroyAfterDelay(3f));
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }

}
