using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGgame.Data;
using RPGgame.Dto.User;
using RPGgame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthenticationController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        // GET: AuthenticationController
        public async Task<ActionResult> Register(UserRegisterDto request)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            response = await _authRepository.Register(new User { userName = request.userName }, request.password);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        //// GET: AuthenticationController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AuthenticationController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AuthenticationController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AuthenticationController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AuthenticationController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AuthenticationController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AuthenticationController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
