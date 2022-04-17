using DecartesClassLibrary.Classes;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for binary data comparison
    /// It keeps the user-bound data in memory with making use of DiffStorageService (singleton)
    /// </summary>
    [ApiController, Route("v1/[controller]")]
    public class DiffController : Controller
    {
        private DiffStorageService storage;
        public DiffController(DiffStorageService _storage)
        {
            storage = _storage;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{userId}")]
        public JsonResult Compare(int userId)
        {
            //Compare binary portions for the UserId
            BinaryComparer bcLeft = new BinaryComparer();
            bcLeft.Data = storage.Find(userId, isRight: false);
            BinaryComparer bcRight = new BinaryComparer();
            bcRight.Data = storage.Find(userId, isRight: true);
            string result = bcLeft.CompareToJson(bcRight);
            return Json(result);
        }

        [HttpPut("{userId}/{locationStr}")]
        public JsonResult SetData(int userId, string locationStr, [FromForm]string dataString)
        {
            byte[] blob = Convert.FromBase64String(dataString);

            //Now save data to Singleton storage
            storage.Add(userId, blob, isRight: locationStr == "right");
            if (Response!=null) Response.StatusCode = 201;
            return new JsonResult("Success");
        }
    }
}
