// // ＝＝＝＝＝＝＝＝＝＝＝＝開く用＝＝＝＝＝＝＝＝＝＝＝＝
// document.querySelectorAll("tr[data-detail-id]").forEach((row) => {
//   row.addEventListener("click", (event) => {
//     // TODO: どうしてここではevent.targetなのに、`const targetElId = row.dataset.detailId;`だとrowを使うの？
//     // リンクが押されたら処理省略
//     if (event.target.closest("a")) return;
//     // event.target.dataset.detailIdをやってしまうとtdが取れる可能性がある
//     // 指定した行のIDを取ってくるとすると、trから取得できる！
//     const targetElId = row.dataset.detailId;

//     const targetDetailEl = document.getElementById(`${targetElId}`);
//     if (targetDetailEl) {
//       targetDetailEl.hidden = false;
//     }
//   });
// });

// ＝＝＝＝＝＝＝＝＝＝＝＝閉じる用＝＝＝＝＝＝＝＝＝＝＝＝

// hiddenをつけること
// いまtargetDetailEl.hiddenがfalseならtrueに変える
document.querySelectorAll("tr[data-detail-id]").forEach((row) => {
  row.addEventListener("click", (event) => {
    // リンク押されたらイベント発生なし
    if (event.target.closest("a")) return;
    // 今指定している行のIDを取得する
    const detailRowId = row.dataset.detailId;
    // そのIDで要素を取得する
    const detailRowEl = document.getElementById(detailRowId);
    // 要素のhidden属性をつけたい👉️true
    if (detailRowEl) {
      // `!`は左右反転してくれるからif文を省略出来た✨️
      detailRowEl.hidden = !detailRowEl.hidden;
    }
  });
});
