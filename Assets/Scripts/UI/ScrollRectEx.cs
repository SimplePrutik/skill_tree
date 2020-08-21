 using UnityEngine.EventSystems;
 using UnityEngine.UI;

 public class ScrollRectEx : ScrollRect
{
    //чтобы не скроллилось с мышкой
      public override void OnBeginDrag(PointerEventData eventData) { }
      public override void OnDrag(PointerEventData eventData) { }
      public override void OnEndDrag(PointerEventData eventData) { }
}