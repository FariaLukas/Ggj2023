using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private TextMeshProUGUI ActorName, Message;
    [SerializeField] private Image ActorLeft, ActorRight;
    [SerializeField] private Color SubColor = Color.black;
    [SerializeField] private float typingTime = .1f;
    [SerializeField] private AnimatePause Animate;

    private Queue<Sentence> _sentences = new Queue<Sentence>();
    private DialogueSO _currentDialogue;
    private Coroutine _typer;
    private Sentence _currentSentence;
    private WaitForSeconds _wait;
    public UnityEvent _onEndAnimationIn, _onEndAnimationOut;
    private UnityAction _onEndDialogue;

    private void Start()
    {
        _wait = new WaitForSeconds(typingTime);
        _onEndAnimationIn.AddListener(AnimateIn);
        _onEndAnimationOut.AddListener(AnimateOut);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DisplayNextSentence();
    }

    public void StartDialogue(DialogueSO dialogue, UnityAction onEnd = null)
    {
        if (IsPlaying() || dialogue == null) return;

        _sentences.Clear();

        dialogue.Dialogue.ForEach(d => _sentences.Enqueue(d));

        _currentDialogue = dialogue;
        _onEndDialogue = onEnd;

        SetActorsDisplayer();

        if (Message)
            Message.text = "";

        Animate.AnimateIn(_onEndAnimationIn);
    }

    private void DisplayNextSentence()
    {
        if (!CanAdvanceDialogue()) return;

        if (_typer != null)
        {
            StopCoroutine(_typer);
            _typer = null;
            if (Message)
                Message.text = _currentSentence.Message;
            return;
        }

        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        _currentSentence = _sentences.Dequeue();
        DisplaySentence();
    }

    private void DisplaySentence()
    {
        UpdateActorTalking();
        if (ActorName)
            ActorName.text = _currentSentence.Actor.ActorName;
        _typer = StartCoroutine(Typer());
    }

    private void SetActorsDisplayer()
    {
        List<DialogueActor> actorsQuantity = new List<DialogueActor>();
        foreach (var sentence in _currentDialogue.Dialogue)
        {
            if (sentence.Actor.LeftSide)
            {
                if (ActorLeft)
                {
                    if (!actorsQuantity.Contains(sentence.Actor))
                        actorsQuantity.Add(sentence.Actor);
                    ActorLeft.sprite = sentence.Actor.ActorSprite;
                    ActorLeft.gameObject.SetActive(sentence.Actor.ActorSprite != null);

                }
            }
            else
            {
                if (ActorRight)
                {
                    if (!actorsQuantity.Contains(sentence.Actor))
                        actorsQuantity.Add(sentence.Actor);
                    ActorRight.sprite = sentence.Actor.ActorSprite;
                    ActorRight.gameObject.SetActive(sentence.Actor.ActorSprite != null);
                }
            }
        }

        if (actorsQuantity.Count == 1)
        {
            bool isLeftActor = _currentDialogue.Dialogue[0].Actor.LeftSide;
            if (!isLeftActor)
                ActorLeft.gameObject.SetActive(false);
            else
                ActorRight.gameObject.SetActive(false);
        }

        UpdateActorTalking();
    }

    private void UpdateActorTalking()
    {
        if (ActorLeft)
            ActorLeft.color = IsActorTalking(ActorLeft);
        if (ActorRight)
            ActorRight.color = IsActorTalking(ActorRight);
    }

    private void EndDialogue()
    {
        Animate.AnimateOut(_onEndAnimationOut);
    }

    private IEnumerator Typer()
    {
        if (Message)
            Message.text = "";
        foreach (var letter in _currentSentence.Message.ToCharArray())
        {
            if (Message)
                Message.text += letter;
            yield return _wait;
        }

        _typer = null;
    }

    public bool IsPlaying()
    {
        return _currentDialogue != null;
    }

    private void AnimateIn()
    {
        DisplayNextSentence();
    }

    private void AnimateOut()
    {
        _currentDialogue = null;
        Message.text = "";
        _onEndDialogue?.Invoke();
        _onEndDialogue = null;
    }

    private bool CanAdvanceDialogue()
    {
        if (Time.timeScale == 0)
            return false;

        return IsPlaying();
    }

    private Color IsActorTalking(Image actor)
    {
        if (_currentSentence == null) return SubColor;

        return _currentSentence.Actor.ActorSprite ==
        actor.sprite ? Color.white : SubColor;
    }

    private void OnDestroy()
    {
        _onEndAnimationIn.RemoveListener(AnimateIn);
        _onEndAnimationOut.RemoveListener(AnimateOut);
    }
}
