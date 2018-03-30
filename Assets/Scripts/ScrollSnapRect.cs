using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnapRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [Tooltip("Xác định Trang ban đầu của list")]
    public int startingPage = 0;
    [Tooltip("Độ ngưỡng vuốt")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Độ ngưỡng")]
    public int fastSwipeThresholdDistance = 100;
    [Tooltip("Tốc độ chuyển trang")]
    public float decelerationRate = 10f;
    [Tooltip("Ảnh khi page ko được chọn")]
    public Sprite unselectedPage;
    [Tooltip("Ảnh khi page được chọn")]
    public Sprite selectedPage;
    [Tooltip("Container chứa ảnh")]
    public Transform pageSelectionIcons;
  

    private ScrollRect _scrollRectComponent;
    private RectTransform _scrollRectRect;
    private RectTransform _container;

    private bool _horizontal;

    // số trang trong panel
    private int _pageCount;
    private int _currentPage;

    // chuyển trang về vị trí nào
    private bool _lerp;
    private Vector2 _lerpTo;

    // điểm đến của trang
    private List<Vector2> _pagePositions = new List<Vector2>();


    private bool _dragging;
    private float _timeStamp;


    private bool _showPageSelection;
    private int _previousPageSelectionIndex;

    private List<Image> _pageSelectionImages;
    float temp = 0f;
    float temp2 = 0f;
    public static int StateInt;
    //------------------------------------------------------------------------
    void Start()
    {
        _scrollRectComponent = GetComponent<ScrollRect>();
        _scrollRectRect = GetComponent<RectTransform>();
        _container = _scrollRectComponent.content;
        _pageCount = _container.childCount;

        // vuốt lên hay xuống
        if (_scrollRectComponent.horizontal && !_scrollRectComponent.vertical)
        {
            _horizontal = true;
        }
        else if (!_scrollRectComponent.horizontal && _scrollRectComponent.vertical)
        {
            _horizontal = false;
        }
        else
        {
            Debug.LogWarning("Confusing setting of horizontal/vertical direction. Default set to horizontal.");
            _horizontal = true;
        }

        _lerp = false;

        // init
        SetPagePositions();
        SetPage(startingPage);
        InitPageSelection();
        SetPageSelection(startingPage);
    } 

    //------------------------------------------------------------------------
    void Update()
    {
        // Nếu di chuyển trang
        if (_lerp)
        {
            // prevent overshooting with values greater than 1
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, decelerate);
            // time to stop lerping?
            if (Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 0.25f)
            {
                // snap to target and stop lerping
                _container.anchoredPosition = _lerpTo;
                _lerp = false;
                // clear also any scrollrect move that may interfere with our lerping
                _scrollRectComponent.velocity = Vector2.zero;
            }

            // switches selection icon exactly to correct page
            if (_showPageSelection)
            {
                SetPageSelection(GetNearestPage());
            }
        }
        
    }

    //------------------------------------------------------------------------
    private void SetPagePositions()
    {
        int width = 0;
        int height = 0;
        int offsetX = 0;
        int offsetY = 0;
        int containerWidth = 0;
        int containerHeight = 0;

        if (_horizontal)
        {
            // screen width in pixels of scrollrect window
            width = (int)_scrollRectRect.rect.width;
            // center position of all pages
            offsetX = width / 2;
            // total width
            containerWidth = width * _pageCount;
            // limit fast swipe length - beyond this length it is fast swipe no more
           
        }
        else
        {
            height = (int)_scrollRectRect.rect.height;
            offsetY = height / 2;
            containerHeight = height * _pageCount;
            
        }

        // set width of container
        Vector2 newSize = new Vector2(containerWidth, containerHeight);
        _container.sizeDelta = newSize;
        Vector2 newPosition = new Vector2(containerWidth / 2, containerHeight / 2);
        _container.anchoredPosition = newPosition;

        // delete any previous settings
        _pagePositions.Clear();

        // iterate through all container childern and set their positions
        for (int i = 0; i < _pageCount; i++)
        {
            RectTransform child = _container.GetChild(i).GetComponent<RectTransform>();
            Vector2 childPosition;
            if (_horizontal)
            {
                childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
            }
            else
            {
                childPosition = new Vector2(0f, -(i * height - containerHeight / 2 + offsetY));
            }
            child.anchoredPosition = childPosition;
            _pagePositions.Add(-childPosition);
        }
    }

    //------------------------------------------------------------------------
    private void SetPage(int aPageIndex)
    {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
        _container.anchoredPosition = _pagePositions[aPageIndex];
        _currentPage = aPageIndex;
    }

    //------------------------------------------------------------------------
    private void LerpToPage(int aPageIndex)
    {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
        _lerpTo = _pagePositions[aPageIndex];
        _lerp = true;
        _currentPage = aPageIndex;
        Debug.Log("Index State: " + aPageIndex);
        StateInt = aPageIndex;
    }

    //------------------------------------------------------------------------
    private void InitPageSelection()
    {
        // page selection - only if defined sprites for selection icons
        _showPageSelection = unselectedPage != null && selectedPage != null;
        if (_showPageSelection)
        {
            // also container with selection images must be defined and must have exatly the same amount of items as pages container
            if (pageSelectionIcons == null || pageSelectionIcons.childCount != _pageCount)
            {
                Debug.LogWarning("Different count of pages and selection icons - will not show page selection");
                _showPageSelection = false;
            }
            else
            {
                _previousPageSelectionIndex = -1;
                _pageSelectionImages = new List<Image>();

                // cache all Image components into list
                for (int i = 0; i < pageSelectionIcons.childCount; i++)
                {
                    Image image = pageSelectionIcons.GetChild(i).GetComponent<Image>();
                    if (image == null)
                    {
                        Debug.LogWarning("Page selection icon at position " + i + " is missing Image component");
                    }
                    _pageSelectionImages.Add(image);
                }
            }
        }
    }

    //------------------------------------------------------------------------
    private void SetPageSelection(int aPageIndex)
    {
        // nothing to change
        if (_previousPageSelectionIndex == aPageIndex)
        {
            return;
        }

        // unselect old
        if (_previousPageSelectionIndex >= 0)
        {
            _pageSelectionImages[_previousPageSelectionIndex].sprite = unselectedPage;
            _pageSelectionImages[_previousPageSelectionIndex].SetNativeSize();
        }

        // select new
        _pageSelectionImages[aPageIndex].sprite = selectedPage;
        _pageSelectionImages[aPageIndex].SetNativeSize();

        _previousPageSelectionIndex = aPageIndex;
        
    }

    //------------------------------------------------------------------------
    private void NextScreen()
    {
        LerpToPage(_currentPage + 1);
        
    }

    //------------------------------------------------------------------------
    private void PreviousScreen()
    {
        LerpToPage(_currentPage - 1);
    }

  
    //------------------------------------------------------------------------
    private int GetNearestPage()
    {
        // based on distance from current position, find nearest page
        Vector2 currentPosition = _container.anchoredPosition;

        float distance = float.MaxValue;
        int nearestPage = _currentPage;

        for (int i = 0; i < _pagePositions.Count; i++)
        {
            float testDist = Vector2.SqrMagnitude(currentPosition - _pagePositions[i]);
            if (testDist < distance)
            {
                distance = testDist;
                nearestPage = i;
            }
        }

        return nearestPage;
    }
    
    //------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData aEventData)
    {
        temp = _container.anchoredPosition.x; // By MrChien: Get Pos temp2 when UpDrag
        _lerp = false;
        _dragging = true;
    }
    
    //------------------------------------------------------------------------
    public void OnEndDrag(PointerEventData aEventData)
    {
        temp2 = _container.anchoredPosition.x;     
        Debug.Log(temp+"------- "+temp2);
        if (temp > temp2)
        {
            NextScreen();
        }
        else
        {
            PreviousScreen();
        }

        _dragging = false;
    }

    //------------------------------------------------------------------------
    public void OnDrag(PointerEventData aEventData)
    {
        if (!_dragging)
        {
            _dragging = true;          
        }
        else
        {
            if (_showPageSelection)
            {
                SetPageSelection(GetNearestPage());
            }
        }
    }
    public void nextButtonInGUI()
    {
        NextScreen();
    }
    public void prevButtonInGUI()
    {
        PreviousScreen();
    }

}
