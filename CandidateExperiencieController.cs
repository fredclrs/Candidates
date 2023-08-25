using Microsoft.AspNetCore.Mvc;
using TestCandidates.Models;
using TestCandidates.Models.Daos;

namespace TestCandidates.Controllers
{
    public class CandidateExperiencieController : Controller
    {
        private  CandidateExperiencieDao _candidateExDao;
        private CandidateDao _candidateDao;
        private static int _idCandidate;

        public CandidateExperiencieController()
        {            
            _candidateDao = new CandidateDao();
            _candidateExDao = new CandidateExperiencieDao();
        }

        // GET: CandidateExperiencie
        public IActionResult Index(int? id)
        {
            if (id != null) {
                _idCandidate = Convert.ToInt32(id);
            }                        
            return View(_candidateExDao.getAll(Convert.ToInt32(_idCandidate)));
        }
        
        // GET: CandidateExperiencie/Create
        public IActionResult Create()
        {           
            return View();
        }

        // POST: CandidateExperiencie/Create        
        [HttpPost, ActionName("Create")]       
        public IActionResult Create(CandidateExperiencie candidateExperiencie)
        {
            try
            {
                candidateExperiencie.InsertDate = DateTime.Now.Date;
                candidateExperiencie.IdCandidate = _idCandidate;
                _candidateExDao.addExperiencie(candidateExperiencie);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                return View(ex.Message);
            }
            
        }

        // GET: CandidateExperiencie/Edit/5
        public IActionResult Edit(int? id)
        {
           if (id == null)
            {
                return NotFound();
            }

            var candidateExperiencie = _candidateExDao.GetCandidateExperiencie(Convert.ToInt32(id));
            if (candidateExperiencie == null)
            {
                return NotFound();
            }            
            return View(candidateExperiencie);
        }

        // POST: CandidateExperiencie/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CandidateExperiencie candidateExperiencie)
        {          
             try
             {
                var candidateExp = _candidateExDao.GetCandidateExperiencie(candidateExperiencie.IdCandidateExperiencie);
                candidateExperiencie.InsertDate = candidateExp.InsertDate;
                candidateExperiencie.IdCandidate = candidateExp.IdCandidate;
                candidateExperiencie.ModifyDate = DateTime.Now.Date;
                _candidateExDao.editExperiencie(candidateExperiencie);
                return RedirectToAction("Index");
             }
             catch 
             {
                return View();
             }                                        
        }

        // GET: CandidateExperiencie/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateExperiencie = _candidateExDao.GetCandidateExperiencie(Convert.ToInt32(id));
            if (candidateExperiencie == null)
            {
                return NotFound();
            }

            return View(candidateExperiencie);
        }

        // POST: CandidateExperiencie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CandidateExperiencie candidateExperiencie)
        {

            _candidateExDao.deleteExperiencie(candidateExperiencie);
            return RedirectToAction(nameof(Index));
        }        
    }
}
