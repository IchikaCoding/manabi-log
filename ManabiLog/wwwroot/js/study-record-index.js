// JSのコード理解してみる
document.querySelectorAll("tr[data-detail-id]").forEach((row) => {
  row.addEventListener("click", (event) => {
    // リンクが押されたら処理省略
    if (event.target.closest("a")) return;
    // 詳細行を開く
    // 詳細行のIDを取得（data-detail-id属性の値を取得）
    // event.targetのdata-detail-idの属性の値を知りたい！
    // 押された要素を取ってくる、その要素のdata-detail-idの属性の値を取ってくる

    // const detailRowId = document.getElementById("");
    // // 詳細行を取得(行)
    // const detailRow = document.getElementById($`event.target.value`);
    // その子のhidden属性をfalseにする
    event.target.hidden = false;
  });
});
