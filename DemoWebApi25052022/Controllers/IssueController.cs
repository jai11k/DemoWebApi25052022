using AutoMapper;
using Demo.Db;
using Demo.Models;
using Demo.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DemoWebApi25052022.Controllers
{
        [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IssueDbContext _context;
        private readonly IMapper _mapper;

        public IssueController(IssueDbContext context,IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<IssueViewModel>> Get()
        {
            var IssuesModels= await _context.Issues.ToListAsync();

            var listIssuesViewModels = new List<IssueViewModel>();

            foreach (var issue in IssuesModels)
            {
                var issueViewModel=_mapper.Map<IssueViewModel>(issue);
                listIssuesViewModels.Add(issueViewModel);

            }
            return listIssuesViewModels;
        }
        

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IssueViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpGet("search/{title}")]
        [ProducesResponseType(typeof(IssueViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var issue = await _context.Issues.SingleOrDefaultAsync(c => c.Title == title);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(IssueViewModel issue)
        {
            var issueModel = _mapper.Map<Issue>(issue);
            await _context.Issues.AddAsync(issueModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = issueModel.Id }, issue);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Issue issue)
        {
            if (id != issue.Id) return BadRequest();

            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var issueToDelete = await _context.Issues.FindAsync(id);
            if (issueToDelete == null) return NotFound();

            _context.Issues.Remove(issueToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
