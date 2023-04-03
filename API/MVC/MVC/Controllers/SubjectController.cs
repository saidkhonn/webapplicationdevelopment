using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using System;
using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class SubjectController : Controller
    {
        public const string baseUrl = "http://localhost:44145/";
        private readonly Uri clntBaseAddress = new Uri(baseUrl);
        private readonly HttpClient clnt;

        public SubjectController()
        {
            clnt = new HttpClient();
            clnt.BaseAddress = clntBaseAddress;
        }
        private void HeaderClearing()
        {
            // Clearing default headers
            clnt.DefaultRequestHeaders.Clear();

            // Define the request type of the data
            clnt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Subject
        public async Task<ActionResult> Index()
        {
            List<Subject> subjects = new List<Subject>();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Subject");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                subjects = JsonConvert.DeserializeObject<List<Subject>>(responseMessage);
            }
            return View(subjects);
        }

        // GET: Subject/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Subject subject = new Subject();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync($"api/Subject/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                subject = JsonConvert.DeserializeObject<Subject>(responseMessage);
            }
            return View(subject);
        }

        // GET: Subject/Create
        public async Task<ActionResult> CreateAsync()
        {
            Subject subject = new Subject();
            HeaderClearing();
            return View(subject);
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                string createSubjectInfo = JsonConvert.SerializeObject(subject);
                StringContent stringContentInfo = new StringContent(createSubjectInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/Subject", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Subject subject = new Subject();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync($"api/Subject/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                subject = JsonConvert.DeserializeObject<Subject>(responseMessage);
            }

            return View(subject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Subject subject)
        {
            if (ModelState.IsValid)
            {
                string createSubjectInfo = JsonConvert.SerializeObject(subject);
                StringContent stringContentInfo = new StringContent(createSubjectInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage editHttpResponseMessage = clnt.PutAsync(clnt.BaseAddress + $"api/Subject/{id}", stringContentInfo).Result;
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(subject);
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int id)
        {
            Subject subjectInfo = new Subject();
            HttpResponseMessage getSubjectHttpResponseMessage = clnt.GetAsync(clnt.BaseAddress + $"api/Subject/{id}").Result;
            if (getSubjectHttpResponseMessage.IsSuccessStatusCode)
            {
                subjectInfo = getSubjectHttpResponseMessage.Content.ReadAsAsync<Subject>().Result;
            }
            return View(subjectInfo);
        }

        // POST: Subject/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Subject subject)
        {
            HttpResponseMessage deleteSubjectHttpResponseMessage = clnt.DeleteAsync(clnt.BaseAddress + $"api/Subject/{id}").Result;
            if (deleteSubjectHttpResponseMessage.IsSuccessStatusCode)
            {
                //subjectInfo = getSubjectHttpResponseMessage.Content.ReadAsAsync<Subject>().Result;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
