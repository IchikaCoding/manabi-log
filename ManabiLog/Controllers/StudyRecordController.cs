
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManabiLog.Models;

public class StudyRecordController : Controller
{
    private readonly ManabiLogContext _context;

    public StudyRecordController(ManabiLogContext context)
    {
        _context = context;
    }

    // ナビゲーションプロパティを使う
    public async Task PracticeNavigation()
    {
        // recordには何が入っていますか？
        StudyRecord record = await _context.StudyRecord.FirstAsync();
        var seedData = new JunctionTableModel
        {
            // IDは省略。なぜなら、外部キーだから？
            studyRecord = record,
            tag = new Tag { Name = "ナビゲーション練習" },
            AddedTagAt = DateTime.Now
        };
        _context.StudyRecordTags.Add(seedData);
        await _context.SaveChangesAsync();
    }

    // ナビゲーションプロパティを使って表示してみよう！
    public async Task<IActionResult> PracticeShow()
    {
        // 〇〇のリストを作成する
        var list = await _context.StudyRecordTags.Include(item => item.studyRecord).Include(item => item.tag).ToListAsync();
        // ! Listにデータ入れたら画面に表示できそう
        var array = new List<ViewModel>();
        // 〇〇から一つずつデータ取得
        foreach (JunctionTableModel item in list)
        {
            // 1件ずつの〇〇からタイトルとか、タグの名前も取れそう
            var title = item.studyRecord.Title;
            var name = item.tag.Name;
            var viewModel = new ViewModel();
            viewModel.Title = title;
            viewModel.Name = name;
            array.Add(viewModel);
        }
        return View(array);
    }

    // GET: STUDYRECORDS
    public async Task<IActionResult> Index()
    {
        return View(await _context.StudyRecord.ToListAsync());
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
    public async Task<IActionResult> Create([Bind("Id,StudyDate,Title,Tags,Content,Learned,Memo,CreatedAt,UpdatedAt")] StudyRecord studyrecord)
    {
        if (ModelState.IsValid)
        {
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

        var studyrecord = await _context.StudyRecord.FindAsync(id);
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
    public async Task<IActionResult> Edit(int? id, [Bind("Id,StudyDate,Title,Tags,Content,Learned,Memo,CreatedAt,UpdatedAt")] StudyRecord studyrecord)
    {
        if (id != studyrecord.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(studyrecord);
                await _context.SaveChangesAsync();
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
            return RedirectToAction(nameof(Index));
        }
        return View(studyrecord);
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
