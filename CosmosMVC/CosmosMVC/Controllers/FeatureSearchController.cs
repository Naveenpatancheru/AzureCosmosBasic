using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CosmosMVC.Models;

namespace CosmosMVC.Controllers
{
    public class FeatureSearchController : Controller
    {
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync()
        {
            var items = await DocumentDBRepository<FeatureSearchM>.GetItemsAsync(d => d.PageID=="0001");
            return View(items);
        }
        [ActionName("AddNewRecord")]
        public async Task<ActionResult> CreateAsync()
        {
            FeatureSearchM newFeatureSearch = new FeatureSearchM();
            //  newFeatureSearch.id = "9";
            newFeatureSearch.id = Guid.NewGuid().ToString("N");
            return View(newFeatureSearch);
        }

        [HttpPost]
        [ActionName("AddNewRecord")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind()] FeatureSearchM item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<FeatureSearchM>.CreateItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("DeleteRecord")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind(Include = "Id")] string id)
        {
            await DocumentDBRepository<FeatureSearchM>.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind] FeatureSearchM item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<FeatureSearchM>.UpdateItemAsync(item.PageID, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FeatureSearchM item = await DocumentDBRepository<FeatureSearchM>.GetItemAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            FeatureSearchM item = await DocumentDBRepository<FeatureSearchM>.GetItemAsync(id);
            return View(item);
        }
    }
}