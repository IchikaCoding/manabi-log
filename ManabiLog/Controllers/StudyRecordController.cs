
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManabiLog.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class StudyRecordController : Controller
{
    private readonly ManabiLogContext _context;

    public StudyRecordController(ManabiLogContext context)
    {
        _context = context;
    }

    // // ナビゲーションプロパティを使う
    // public async Task PracticeNavigation()
    // {
    //     // recordには何が入っていますか？
    //     StudyRecord record = await _context.StudyRecord.FirstAsync();
    //     var seedData = new JunctionTableModel
    //     {
    //         // IDは省略。なぜなら、外部キーだから？
    //         studyRecord = record,
    //         tag = new Tag { Name = "ナビゲーション練習" },
    //         AddedTagAt = DateTime.Now
    //     };
    //     _context.StudyRecordTags.Add(seedData);
    //     await _context.SaveChangesAsync();
    // }

    // // ナビゲーションプロパティを使って表示してみよう！
    // public async Task<IActionResult> PracticeShow()
    // {
    //     // 〇〇のリストを作成する
    //     var list = await _context.StudyRecordTags.Include(item => item.studyRecord).Include(item => item.tag).ToListAsync();
    //     // ! Listにデータ入れたら画面に表示できそう
    //     var logNameList = new List<ViewModel>();
    //     // 〇〇から一つずつデータ取得
    //     foreach (JunctionTableModel item in list)
    //     {
    //         // 1件ずつの〇〇からタイトルとか、タグの名前も取れそう
    //         var title = item.studyRecord.Title;
    //         var name = item.tag.Name;
    //         var viewModel = new ViewModel();
    //         viewModel.Title = title;
    //         viewModel.Name = name;
    //         logNameList.Add(viewModel);
    //     }
    //     return View(logNameList);
    // }

    public async Task CreatSeedData()
    {
        // ここにListのTagを作る
        Tag tag1 = new Tag()
        {
            Name = "DB"
        };
        Tag tag2 = new Tag()
        {
            Name = "さつまいも"
        }; ;
        var now = DateTime.Now;
        StudyRecord record = new StudyRecord
        {
            StudyDate = DateOnly.FromDateTime(DateTime.Today),
            Title = "いちか丼を学んだ",
            Tags = new List<Tag> { tag1, tag2 },
            CreatedAt = now,
            UpdatedAt = now
        };
        _context.StudyRecord.Add(record);
        await _context.SaveChangesAsync();
    }
    // GET: STUDYRECORDS
    public async Task<IActionResult> Index()
    {
        // DBからStudyRecordテーブルを取得します。
        // DBには保存されていないけど、ナビゲーションプロパティとして追加されているTagsも含めて取得します　という意味？
        return View(await _context.StudyRecord.Include(item => item.Tags).ToListAsync());
    }

    // GET: STUDYRECORDS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var studyrecord = await _context.StudyRecord
            .FirstOrDefaultAsync(m => m.Id == id);
        if (studyrecord == null)
        {
            return NotFound();
        }

        return View(studyrecord);
    }

    // GET: STUDYRECORDS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: STUDYRECORDS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,StudyDate,Title,Tags,Content,Learned,Memo,CreatedAt,UpdatedAt")] StudyRecord studyrecord, string? tagInput)
    {
        // コンパイラーには、`null` が入らないと確認できないらしい。だからreturnを書かないと` Name = tagInput`で警告になる
        if (string.IsNullOrWhiteSpace(tagInput))
        {
            ModelState.AddModelError(
                "TagInput", "タグを入力してください"
            );
            return View(studyrecord);
        }
        // ModelStateは入力値とかエラーとかが記録されている
        // IsValidは有効ですか？Yes👉️True
        if (ModelState.IsValid)
        {
            // ここでtagInput変数を使用したい
            // studyrecordのTagsのListに保存したい
            var tagObj = new Tag()
            {
                // どうしてここでNull参照の可能性が出てくるの？
                // 手前のif文で早期リターンしていなかったから。
                Name = tagInput
            };
            studyrecord.Tags = new List<Tag> { tagObj };
            // 作成時間と更新時間を入力
            // nowは2回使うから、最初に作っておくといい（天才✨️）
            var now = DateTime.Now;
            studyrecord.CreatedAt = now;
            studyrecord.UpdatedAt = now;

            _context.Add(studyrecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(studyrecord);
    }

    // GET: STUDYRECORDS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var studyrecord = await _context.StudyRecord.Include(record => record.Tags).FirstOrDefaultAsync(record => record.Id == id);
        if (studyrecord == null)
        {
            return NotFound();
        }
        return View(studyrecord);
    }

    // POST: STUDYRECORDS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    // TODO: どうやってIdをもらってきたの？
    public async Task<IActionResult> Edit(int? id, [Bind("Id,StudyDate,Title,Tags,Content,Learned,Memo,CreatedAt,UpdatedAt")] StudyRecord studyrecord)
    {
        try
        {
            // IDが無い時があるらしい
            if (id != studyrecord.Id)
            {
                return NotFound();
            }
            // StudyRecordがあるのかどうかを確認
            if (!ModelState.IsValid)
            {
                return View();
            }

            // DBのデータを取得する
            var dbStudyRecord = await _context.StudyRecord.Include(record => record.Tags).FirstOrDefaultAsync(record => record.Id == id);
            if (dbStudyRecord == null)
            {
                return NotFound();
            }
            // DBのデータを入力値で上書き
            dbStudyRecord.Title = studyrecord.Title;
            dbStudyRecord.Content = studyrecord.Content;
            dbStudyRecord.Learned = studyrecord.Learned;
            dbStudyRecord.Memo = studyrecord.Memo;
            dbStudyRecord.UpdatedAt = DateTime.Now;
            // タグはListだからforeachとかでぐるぐる回す、1件ずつ更新させる
            foreach (var tag in studyrecord.Tags)
            {
                // TODO: tagは必須項目だからこの確認が必要かわからない
                if (tag == null)
                {
                    return NotFound();
                }
                // tagのIDと一致するデータをDBから取得する
                // ! FirstOrDefaultは一致するデータがない場合null（シーケンスが消す既定値）を返す！
                Tag? dbTag = dbStudyRecord.Tags.FirstOrDefault(item => item.Id == tag.Id);
                if (dbTag == null)
                {
                    return NotFound();
                }
                // 取得したデータを入力値で上書き
                dbTag.Name = tag.Name;
            }
            // 上書きしたデータをDBに反映させる
            // _context.Add(dbStudyRecord);
            // ExecuteUpdate を使う方法（EF Core 7.0以降）もあるよ。
            // _context.Update(dbStudyRecord);をしない理由は、上書きしたプロパティだけ自動で更新したいから。
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            // ! もしかしたらTagを編集した場合、IDが一致するほかのタグまで更新されちゃうかも。
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudyRecordExists(studyrecord.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
    }

    // GET: STUDYRECORDS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var studyrecord = await _context.StudyRecord
            .FirstOrDefaultAsync(m => m.Id == id);
        if (studyrecord == null)
        {
            return NotFound();
        }

        return View(studyrecord);
    }

    // POST: STUDYRECORDS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var studyrecord = await _context.StudyRecord.FindAsync(id);
        if (studyrecord != null)
        {
            _context.StudyRecord.Remove(studyrecord);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StudyRecordExists(int? id)
    {
        return _context.StudyRecord.Any(e => e.Id == id);
    }
    // TagのIDでアクセスした時にDBからTagテーブルを参照して、データを渡す
    // Viewでデータを受け取ったらTagのViewにTagデータを表示する
    public async Task<IActionResult> Tag(int? id)
    {
        var recordFromDb = await _context.StudyRecord.Include(record => record.Tags).FirstOrDefaultAsync(record => record.Id == id);
        if (recordFromDb == null)
        {
            return NotFound();
        }
        // どうしてか、List<Tags>?だとTagsがないと言われます。どうして？
        var tagsFromDb = recordFromDb.Tags;
        return View();
    }
}
