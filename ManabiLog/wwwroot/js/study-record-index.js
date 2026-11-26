// ＝＝＝＝＝＝＝＝＝＝＝＝開く用＝＝＝＝＝＝＝＝＝＝＝＝
document.querySelectorAll("tr[data-detail-id]").forEach((row) => {
  row.addEventListener("click", (event) => {
    // TODO: どうしてここではevent.targetなのに、`const targetElId = row.dataset.detailId;`だとrowを使うの？
    // リンクが押されたら処理省略
    if (event.target.closest("a")) return;
    // event.target.dataset.detailIdをやってしまうとtdが取れる可能性がある
    // 指定した行のIDを取ってくるとすると、trから取得できる！
    const targetElId = row.dataset.detailId;

    const targetDetailEl = document.getElementById(`${targetElId}`);
    if (targetDetailEl) {
      targetDetailEl.hidden = false;
    }
  });
});
// ＝＝＝＝＝＝＝＝＝＝＝＝練習用＝＝＝＝＝＝＝＝＝＝＝＝
const elTest = document.getElementById("study-details-1");
console.log(elTest);

// closestの使い方とは？どの要素が押されたのか、自分自身を探す時だけ使うの？
console.log(elTest.closest("tr"));

// ＝＝＝＝＝＝＝＝＝＝＝＝閉じる用＝＝＝＝＝＝＝＝＝＝＝＝

// hiddenをつけること
// いまtargetDetailEl.hiddenがfalseならtrueに変える
document.querySelectorAll("tr[data-detail-id]").forEach((row) => {
  row.addEventListener("click", (event) => {
    // リンク押されたらイベント発生なし
    if (event.target.closest("a")) return;
    // 今指定している行のIDを取得する
    const rowId = row.dataset.detailId;
    // そのIDで要素を取得する
    const rowEl = document.getElementById(rowId);
    // 要素のhidden属性をつけたい👉️true
    if (rowEl) {
      // 表示されていたら閉じる
      if (!rowEl.hidden) {
        rowEl.hidden = true;
      } else {
        rowEl.hidden = false;
      }
    }
  });
});
