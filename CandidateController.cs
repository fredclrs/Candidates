using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using TestCandidates.Models;
using TestCandidates.Models.Daos;

namespace TestCandidates.Controllers
{
    public class CandidateController : Controller
    {        
        private CandidateDao _candidateDao;

        public CandidateController()
        {
            _candidateDao = new CandidateDao();
        }

        // GET: Candidate
        public IActionResult Index()
        {
            return View(_candidateDao.getAll());
        }

        // GET: Candidate/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _candidateDao.getAll() == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(m => m.IdCandidate == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }*/

        // GET: Candidate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidate/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Candidate candidate)
        {
            candidate.InsertDate = DateTime.Now.Date;
           _candidateDao.addCandidate(candidate);                
           return RedirectToAction(nameof(Index));
                        
        }

        // GET: Candidate/Edit/5
        public IActionResult Edit(int? id)
        {
            var candidate = _candidateDao.getCandidate(Convert.ToInt32(id));
            if (candidate == null)
            {
                return NotFound();
            }
            return View(candidate);
        }

        // POST: Candidate/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Candidate candidate)
        {
            try
            {
                var vcandidate = _candidateDao.getCandidate(candidate.IdCandidate);
                candidate.InsertDate = vcandidate.InsertDate;
                candidate.ModifyDate = DateTime.Now.Date;
                _candidateDao.editCandidate(candidate);
                return RedirectToAction("Index");
            }
            catch {
                return View();
            }            
        }

        // GET: Candidate/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = _candidateDao.getCandidate(Convert.ToInt32(id));
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Candidate candidate)
        {
            try
            {
                _candidateDao.deleteCandidate(candidate);
                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }
        
    }
}
