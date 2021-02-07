using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public enum HideDirection
{
    Top,
    Bottom
}

//Developer: Antoshka
[RequireComponent(typeof(RectTransform))]
public class PopupAnimations : MonoBehaviour
{
    [SerializeField] private HideDirection _hideDirection;
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;
    [SerializeField] private UnityEvent OnActivated;
    [SerializeField] private UnityEvent OnDeactivated;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = transform as RectTransform;
        SetInitialPosition();
    }

    public void Show()
    {
        OnActivated?.Invoke();
        _rectTransform.DOAnchorPosY(0f, _duration).SetDelay(_delay);
    }

    public void Hide()
    {
        OnDeactivated?.Invoke();
        _rectTransform.DOAnchorPosY(Compensation() * _rectTransform.rect.height, _duration).SetDelay(_delay);
    }

    private void SetInitialPosition()
    {
        _rectTransform.DOAnchorPosY(Compensation() * _rectTransform.rect.height, 0f);
    }

    private float Compensation()
    {
        switch (_hideDirection)
        {
            case HideDirection.Bottom:
                return -1;
            case HideDirection.Top:
                return 1;
        }
        return 0;
    }

    /*
    [SerializeField] private Direction direction;
    [SerializeField] private CanvasType canvasType;
    [SerializeField] private float timeForHiding;
    [SerializeField] private float offset = 50;
    [SerializeField] private UnityEvent OnActivated;
    [SerializeField] private UnityEvent OnDeactivated;
    private Vector3 startPoint;
    private RectTransform rectTransform;
    private Vector2 topRightCoord;
    private Vector2 bottomLeftCoord;

    private void Start()
    {
        rectTransform = transform as RectTransform;
        startPoint = rectTransform.localPosition;
        Camera camera = null;

        if (canvasType == CanvasType.CAMERATYPE)
            camera = Camera.main;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, new Vector2(Screen.width, Screen.height), camera, out topRightCoord);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, new Vector2(0, 0), camera, out bottomLeftCoord);
        Hide();
    }

    public void Show()
    {
        rectTransform.DOLocalMove(startPoint, timeForHiding);
    }

    public void Hide()
    {
        switch (direction)
        {
            case Direction.LEFT:
                rectTransform.DOLocalMove(new Vector3(EndPosition(Direction.LEFT).x, rectTransform.localPosition.y, 0), timeForHiding);
                break;
            case Direction.RIGHT:
                rectTransform.DOLocalMove(new Vector3(EndPosition(Direction.RIGHT).x, rectTransform.localPosition.y, 0), timeForHiding);
                break;
            case Direction.TOP:
                rectTransform.DOLocalMove(new Vector3(rectTransform.localPosition.x, EndPosition(Direction.TOP).y, 0), timeForHiding);
                break;
            case Direction.BOTTOM:
                rectTransform.DOLocalMove(new Vector3(rectTransform.localPosition.x, EndPosition(Direction.BOTTOM).y, 0), timeForHiding);
                break;
        }
    }

    private Vector3 NegativeCompensation()
    {
        return new Vector2((-rectTransform.sizeDelta.x - offset) + rectTransform.sizeDelta.x * rectTransform.pivot.x,
                        (-rectTransform.sizeDelta.y - offset) + rectTransform.sizeDelta.y * rectTransform.pivot.y);
    }

    private Vector3 PositiveCompensation()
    {
        return new Vector2((rectTransform.sizeDelta.x * rectTransform.pivot.x) + offset,
                                (rectTransform.sizeDelta.y * rectTransform.pivot.y) + offset);
    }

    private Vector2 EndPosition(Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                return ((Vector3)bottomLeftCoord + rectTransform.localPosition) + NegativeCompensation();
            case Direction.RIGHT:
                return (Vector3)topRightCoord + rectTransform.localPosition + PositiveCompensation();
            case Direction.TOP:
                return ((Vector3)topRightCoord + rectTransform.localPosition) + PositiveCompensation();
            case Direction.BOTTOM:
                return ((Vector3)bottomLeftCoord + rectTransform.localPosition) + NegativeCompensation();
        }

        return startPoint;
    }*/
    /*[SerializeField] private float _duration;
    [SerializeField] private float _delay;
    [SerializeField] private HideDirection _hideDirection;
    [SerializeField] private float _offset;
    [SerializeField] private Camera _camera;
    [SerializeField] private UnityEvent OnActivated;
    [SerializeField] private UnityEvent OnDeactivated;
    private RectTransform _rectTransform;
    private Vector2 _startPoint;
    private Vector2 _topRightCoord;
    private Vector2 _bottomLeftCoord;

    private void Start()
    {
        gameObject.SetActive(false);
        _rectTransform = transform as RectTransform;
        _startPoint = _rectTransform.localPosition;
        //_offset = _rectTransform.rect.height * _rectTransform.pivot.y;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform,
            new Vector2(Screen.width, Screen.height), _camera, out _topRightCoord);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, new Vector2(0, 0), _camera,
            out _bottomLeftCoord);

        SetPanelsPosition();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        OnActivated?.Invoke();
        _rectTransform.DOLocalMove(_startPoint, _duration);
    }

    public void Deactivate()
    {
        switch (_hideDirection)
        {
            case HideDirection.Top:
                _rectTransform.DOLocalMove(
                        new Vector3(_rectTransform.localPosition.x, EndPosition(HideDirection.Top).y, 0), _duration)
                    .OnComplete(() => gameObject.SetActive(false));
                break;
            case HideDirection.Bottom:
                _rectTransform.DOLocalMove(
                        new Vector3(_rectTransform.localPosition.x, EndPosition(HideDirection.Bottom).y, 0), _duration)
                    .OnComplete(() => gameObject.SetActive(false));
                break;
        }
    }

    private void SetPanelsPosition()
    {
        switch (_hideDirection)
        {
            case HideDirection.Top:
                _rectTransform.localPosition =
                    new Vector3(_rectTransform.localPosition.x, EndPosition(HideDirection.Top).y, 0);
                break;
            case HideDirection.Bottom:
                _rectTransform.localPosition =
                    new Vector3(_rectTransform.localPosition.x, EndPosition(HideDirection.Bottom).y, 0);
                break;
        }
        gameObject.SetActive(false);
    }

    private Vector2 EndPosition(HideDirection hideDirection)
    {
        switch (hideDirection)
        {
            case HideDirection.Top:
                return ((Vector3) _topRightCoord + _rectTransform.localPosition) + PositiveCompensation();
            case HideDirection.Bottom:
                return ((Vector3)_bottomLeftCoord + _rectTransform.localPosition) + NegativeCompensation();
        }

        return _startPoint;
    }

    private Vector3 PositiveCompensation()
    {
        return new Vector2((_rectTransform.sizeDelta.x * _rectTransform.pivot.x) + _offset,
            (_rectTransform.sizeDelta.y * _rectTransform.pivot.y) + _offset);
    }

    private Vector3 NegativeCompensation()
    {
        return new Vector2(
            (-_rectTransform.sizeDelta.x - _offset) + _rectTransform.sizeDelta.x * _rectTransform.pivot.x,
            (-_rectTransform.sizeDelta.y - _offset) + _rectTransform.sizeDelta.y * _rectTransform.pivot.y);
    }*/
}

public enum Direction { DEFAULT, RIGHT, LEFT, TOP, BOTTOM }
public enum CanvasType { OVERLAY, CAMERATYPE }

public class HideBeyondScreenComponent : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private CanvasType canvasType;
    [SerializeField] private float timeForHiding = 1;
    [SerializeField] private float offset = 50;
    private Vector3 startPoint;
    private RectTransform rectTransform;
    private Vector2 topRightCoord;
    private Vector2 bottomLeftCoord;

    private void Start()
    {
        rectTransform = transform as RectTransform;
        startPoint = rectTransform.localPosition;
        Camera camera = null;

        if (canvasType == CanvasType.CAMERATYPE)
            camera = Camera.main;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, new Vector2(Screen.width, Screen.height), camera, out topRightCoord);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, new Vector2(0, 0), camera, out bottomLeftCoord);
        Hide();
    }

    public void Show()
    {
        rectTransform.DOLocalMove(startPoint, timeForHiding);
    }

    public void Hide()
    {
        switch (direction)
        {
            case Direction.LEFT:
                rectTransform.DOLocalMove(new Vector3(EndPosition(Direction.LEFT).x, rectTransform.localPosition.y, 0), timeForHiding);
                break;
            case Direction.RIGHT:
                rectTransform.DOLocalMove(new Vector3(EndPosition(Direction.RIGHT).x, rectTransform.localPosition.y, 0), timeForHiding);
                break;
            case Direction.TOP:
                rectTransform.DOLocalMove(new Vector3(rectTransform.localPosition.x, EndPosition(Direction.TOP).y, 0), timeForHiding);
                break;
            case Direction.BOTTOM:
                rectTransform.DOLocalMove(new Vector3(rectTransform.localPosition.x, EndPosition(Direction.BOTTOM).y, 0), timeForHiding);
                break;
        }
    }

    private Vector3 NegativeCompensation()
    {
        return new Vector2((-rectTransform.sizeDelta.x - offset) + rectTransform.sizeDelta.x * rectTransform.pivot.x,
                        (-rectTransform.sizeDelta.y - offset) + rectTransform.sizeDelta.y * rectTransform.pivot.y);
    }

    private Vector3 PositiveCompensation()
    {
        return new Vector2((rectTransform.sizeDelta.x * rectTransform.pivot.x) + offset,
                                (rectTransform.sizeDelta.y * rectTransform.pivot.y) + offset);
    }

    private Vector2 EndPosition(Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                return ((Vector3)bottomLeftCoord + rectTransform.localPosition) + NegativeCompensation();
            case Direction.RIGHT:
                return (Vector3)topRightCoord + rectTransform.localPosition + PositiveCompensation();
            case Direction.TOP:
                return ((Vector3)topRightCoord + rectTransform.localPosition) + PositiveCompensation();
            case Direction.BOTTOM:
                return ((Vector3)bottomLeftCoord + rectTransform.localPosition) + NegativeCompensation();
        }

        return startPoint;
    }
}
