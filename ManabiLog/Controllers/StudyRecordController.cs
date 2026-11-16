
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManabiLog.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.VisualBasic;

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
        if (string.IsNullOrWhiteSpace(tagInput))
        {
            ModelState.AddModelError(
                "TagInput", "タグを入力してください"
            );
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
            if (id != studyrecord.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(studyrecord);
            }
            var dbStudyRecord = await _context.StudyRecord.Include(record => record.Tags).FirstOrDefaultAsync(record => record.Id == studyrecord.Id);
            if (dbStudyRecord == null)
            {
                return NotFound();
            }
            dbStudyRecord.Title = studyrecord.Title;
            dbStudyRecord.Content = studyrecord.Content;
            dbStudyRecord.Learned = studyrecord.Learned;
            dbStudyRecord.Memo = studyrecord.Memo;
            dbStudyRecord.UpdatedAt = DateTime.Now;
            foreach (var tag in studyrecord.Tags)
            {
                // 入力値のタグ1件のIDとDBのタグ（List）のIDが一致するものを取って来る。
                // それで、入力値のデータで上書き
                var dbTag = dbStudyRecord.Tags.FirstOrDefault(item => item.Id == tag.Id);
                if (dbTag == null)
                {
                    return BadRequest();
                }
                dbTag.Name = tag.Name;
            }
            _context.Update(dbStudyRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
}
