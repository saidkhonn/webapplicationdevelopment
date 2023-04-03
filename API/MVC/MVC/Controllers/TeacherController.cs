using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using MVC.Models;

namespace MVC.Controllers
{
    public class TeacherController : Controller
    {
        public const string baseUrl = "http://localhost:44145/";
        private readonly Uri clientBaseAddress = new Uri(baseUrl);
        private readonly HttpClient clnt;

        public TeacherController()
        {
            clnt = new HttpClient();
            clnt.BaseAddress = clientBaseAddress;
        }
        private void HeaderClearing()
        {
            // Clearing default headers
            clnt.DefaultRequestHeaders.Clear();

            // Define the request type of the data
            clnt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Teacher
        public async Task<ActionResult> Index()
        {
            List<Teacher> teachers = new List<Teacher>();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Teacher");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                teachers = JsonConvert.DeserializeObject<List<Teacher>>(responseMessage);
            }
            return View(teachers);
        }

        // GET: Teacher/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Teacher teacher = new Teacher();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync($"api/Teacher/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                teacher = JsonConvert.DeserializeObject<Teacher>(responseMessage);
            }
            return View(teacher);
        }

        // GET: Teacher/Create
        public async Task<ActionResult> CreateAsync()
        {
            List<Subject> subjects = new List<Subject>();
            HeaderClearing();
            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Subject");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                subjects = JsonConvert.DeserializeObject<List<Subject>>(responseMessage);
            }

            var viewModel = new TeacherSubjectViewModel
            {
                Teacher = new Teacher(),
                Subjects = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(subjects, "Id", "SubjectName", "Description")
            };
            return View(viewModel);
        }

        // POST: Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Teacher teacher)
        {
            teacher.Subject = new Subject { Id = teacher.TeacherSubjectId };
            if (ModelState.IsValid)
            {
                string createTeacherInfo = JsonConvert.SerializeObject(teacher);
                StringContent stringContentInfo = new StringContent(createTeacherInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/Teacher", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(teacher);
        }

        // GET: Teacher/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Teacher teacher = new Teacher();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync($"api/Teacher/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                teacher = JsonConvert.DeserializeObject<Teacher>(responseMessage);
            }

            List<Subject> subjects = new List<Subject>();
            httpResponseMessage = await clnt.GetAsync("api/Subject");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                subjects = JsonConvert.DeserializeObject<List<Subject>>(responseMessage);
            }
            var viewModel = new TeacherSubjectViewModel
            {
                Teacher = teacher,
                Subjects = new SelectList(subjects, "Id", "SubjectName", "Description")
            };
            return View(viewModel);
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Teacher teacher)
        {
            teacher.Subject = new Subject { Id = teacher.TeacherSubjectId };

            if (ModelState.IsValid)
            {
                string createTeacherInfo = JsonConvert.SerializeObject(teacher);
                StringContent stringContentInfo = new StringContent(createTeacherInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage editHttpResponseMessage = clnt.PutAsync(clnt.BaseAddress + $"api/Teacher/{id}", stringContentInfo).Result;
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(teacher);
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int id)
        {
            Teacher teacherInfo = new Teacher();
            HttpResponseMessage getTeacherHttpResponseMessage = clnt.GetAsync(clnt.BaseAddress + $"api/Teacher/{id}").Result;
            if (getTeacherHttpResponseMessage.IsSuccessStatusCode)
            {
                teacherInfo = getTeacherHttpResponseMessage.Content.ReadAsAsync<Teacher>().Result;
            }
            return View(teacherInfo);
        }

        // POST: Teacher/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Teacher teacher)
        {
            HttpResponseMessage deleteTeacherHttpResponseMessage = clnt.DeleteAsync(clnt.BaseAddress + $"api/Teacher/{id}").Result;
            if (deleteTeacherHttpResponseMessage.IsSuccessStatusCode)
            {
                //teacherInfo = getTeacherHttpResponseMessage.Content.ReadAsAsync<Teacher>().Result;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
