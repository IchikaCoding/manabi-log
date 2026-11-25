// JSのコード理解してみる
document.querySelectorAll("tr[data-detail-id]").forEach((row) => {
  row.addEventListener("click", (event) => {
    // TODO: どうしてここではevent.targetなのに、`const targetElId = row.dataset.detailId;`だとrowを使うの？
    // リンクが押されたら処理省略
    if (event.target.closest("a")) return;
    // const el = event.target;
    // const id = el.data - detail - id.value();
    // クリックした要素：data-detail-id="study-detail-1"だとする
    // study-detail-1がわかるなら、document.getElementById("study-detail-1");でhiddenの要素を取得することができる！
    // その要素からhidden = false;を実行する
    const targetElId = row.dataset.detailId;

    const targetDetailEl = document.getElementById(`${targetElId}`);
    if (targetDetailEl) {
      targetDetailEl.hidden = false;
    }
    // 詳細行を開く
    // 詳細行のIDを取得（data-detail-id属性の値を取得）
    // event.targetのdata-detail-idの属性の値を知りたい！
    // 押された要素を取ってくる、その要素のdata-detail-idの属性の値を取ってくる

    // const detailRowId = document.getElementById("");
    // // 詳細行を取得(行)
    // const detailRow = document.getElementById($`event.target.value`);
    // その子のhidden属性をfalseにする
  });
});
// ＝＝＝＝＝＝＝＝＝＝＝＝練習用＝＝＝＝＝＝＝＝＝＝＝＝
const elTest = document.getElementById("study-details-1");
console.log(elTest);

// closestの使い方とは？どの要素が押されたのか、自分自身を探す時だけ使うの？
console.log(elTest.closest("tr"));
