using CleanArchitecture.Application.RepositoryService;
using CleanArchitecture.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApp.Controllers;

public class StudentController : Controller
{
    private readonly IStudentRepository studentRepository;

    public StudentController(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }
   public async Task<ActionResult<StudentVm>> Index(CancellationToken cancellationToken)
    {
        return View(await studentRepository.GetAllAsync(cancellationToken));
    }
    public async Task<ActionResult<StudentVm>> Create(long id,CancellationToken cancellationToken)
    {
        if (id==0)
        {
            return View(new StudentVm());
        }
        else
        {
            return View(await studentRepository.GetByIdAsync(id, cancellationToken));
        }
    }
    [HttpPost]
    public async Task<ActionResult<StudentVm>> Create(long id,StudentVm studentVm,CancellationToken cancellationToken)
    {
        if (id==0)
        {
            if (ModelState.IsValid)
            {
				await studentRepository.InsertAsync(studentVm, cancellationToken);
				return RedirectToAction("Index");
			}
        }
        else
        {
            await studentRepository.UpdateAsync(id, studentVm, cancellationToken);
            return RedirectToAction("Index");
        }
        return View(new StudentVm());
    }
    public async Task<ActionResult<StudentVm>> Delete(long id, CancellationToken cancellationToken)
    {
        if (id==0)
        {
            return RedirectToAction("Index");
        }
        else
        {
            await studentRepository.DeleteAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    public async Task<ActionResult<StudentVm>> Details(long id,CancellationToken cancellationToken)
    {
        if (id==0)
        {
            return View(new StudentVm());
        }
        else
        {
            return View(await studentRepository.GetByIdAsync(id,cancellationToken));
        }
    }
}
