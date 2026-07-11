using UnityEngine;

public class ShowCard : MonoBehaviour
{
    [Header("配置の設定")]
    [SerializeField] private float objectSpacing = 2.0f; // オブジェクト間の基本の隙間
    [SerializeField] private float moveSpeed = 5.0f;     // 移動アニメーションの速度

    [Header("先頭オブジェクトのトランスフォーム")]
    [SerializeField] private Vector3 focusPosition = new Vector3(0f, 1f, 0f);     // X0, Y1
    [SerializeField] private Vector3 focusScale = new Vector3(2.4f, 3.2f, 1f);    // 拡大サイズ

    private enum CarouselState
    {
        WaitingForFocus, // 先頭を注目（拡大）させるのを待っている状態
        WaitingForShift  // 注目中。次エンターが押されたら透明化して後ろに送る状態
    }

    private CarouselState currentState = CarouselState.WaitingForFocus;
    private Transform focusedChild = null;

    void Update()
    {
        // 子オブジェクトがなければ処理しない
        if (transform.childCount == 0) return;

        // エンターキー（Returnキー）が押されたときの状態遷移
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleStateTransition();
        }

        // 毎フレーム、現在の状態に基づいて位置・拡大率・透明度をスムーズに補間
        UpdateObjectsTransform();
    }

    private void HandleStateTransition()
    {
        if (currentState == CarouselState.WaitingForFocus)
        {
            // 1回目のエンター：一番左（インデックス0）をターゲットにする
            focusedChild = transform.GetChild(0);
            currentState = CarouselState.WaitingForShift;
        }
        else if (currentState == CarouselState.WaitingForShift)
        {
            // 2回目のエンター：透明度を下げて、ヒエラルキーの一番最後（右端）に送る
            if (focusedChild != null)
            {
                SetAlpha(focusedChild, 0.0f); // 透明度を下げる（例: 20%に）
                focusedChild.SetAsLastSibling(); // 一番右（最後尾）へ移動
            }

            // 次のオブジェクトのために不透明度を戻しておく（列に並んでいるものは100%）
            // ※今回一番右にいったオブジェクトも、再び先頭に来る直前に100%に戻す、
            // もしくは列にいる間に徐々に 100% に戻す設計にしています。

            focusedChild = null;
            currentState = CarouselState.WaitingForFocus;
        }
    }

    private void UpdateObjectsTransform()
    {
        int childCount = transform.childCount;

        // 状態によってループの開始位置を変える
        // 注目中の場合、インデックス0（注目オブジェクト）は別処理にするため i=1 から回す
        int startIndex = (currentState == CarouselState.WaitingForShift) ? 1 : 0;

        // --- 1. 通常の列に並んでいるオブジェクトの整列（左に詰める処理） ---
        for (int i = startIndex; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // 列内での目標位置を計算（インデックスに応じて右にずらす）
            // currentState が WaitingForShift の時は、インデックス1が先頭（左端）にくるように調整
            int descendingOrder = (currentState == CarouselState.WaitingForShift) ? (i - 1) : i;
            Vector3 targetPos = new Vector3(descendingOrder * objectSpacing, 0, 0f);
            Vector3 targetScale = Vector3.one; // 通常サイズは (1, 1, 1)

            // スムーズに移動・縮小
            child.localPosition = Vector3.Lerp(child.localPosition, targetPos, Time.deltaTime * moveSpeed);
            //child.localScale = Vector3.Lerp(child.localScale, targetScale, Time.deltaTime * moveSpeed);

            // 列に戻ったオブジェクトの透明度を徐々に100%に戻す
            //SetAlpha(child, Mathf.MoveTowards(GetAlpha(child), 1.0f, Time.deltaTime * moveSpeed));
        }

        // --- 2. 注目（拡大）している先頭オブジェクトの処理 ---
        if (currentState == CarouselState.WaitingForShift && focusedChild != null)
        {
            // 指定された X0, Y1 の座標、および拡大サイズへスムーズに変化
            focusedChild.localPosition = Vector3.Lerp(focusedChild.localPosition, focusPosition, Time.deltaTime * moveSpeed);
            focusedChild.localScale = Vector3.Lerp(focusedChild.localScale, focusScale, Time.deltaTime * moveSpeed);
            SetAlpha(focusedChild, Mathf.MoveTowards(GetAlpha(focusedChild), 1.0f, Time.deltaTime * moveSpeed));
        }
    }

    // 2D SpriteRendererの透明度（Alpha）を変更するヘルパー関数
    private void SetAlpha(Transform target, float alpha)
    {
        if (target.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }

    // 2D SpriteRendererの現在の透明度を取得するヘルパー関数
    private float GetAlpha(Transform target)
    {
        if (target.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            return spriteRenderer.color.a;
        }
        return 1.0f;
    }
}
