// closest()の練習
// クリックした要素の親要素を取得するメソッド

function getEl() {
  // クリックしたらその要素を取ってくる
  document.querySelectorAll("tr[data-detail-id]").forEach((row) =>
    row.addEventListener("click", (event) => {
      if (event.target.closest("a")) return;
      //  クリックしたら中身が取れる処理
      //  今見てる行のtdの値を取ってくる
      const text = event.target.innerText;
      console.log(text);
    }),
  );
}
getEl();

// カスタムデータ属性のCSSセレクタを使う練習
