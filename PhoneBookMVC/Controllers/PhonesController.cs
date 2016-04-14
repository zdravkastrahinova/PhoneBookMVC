using PhoneBookMVC.Filters;
using PhoneBookMVC.Models;
using PhoneBookMVC.Services.ModelServices;
using PhoneBookMVC.ViewModels.PhonesVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace PhoneBookMVC.Controllers
{
    public class PhonesController : BaseController
    {
        public ActionResult List()
        {
            PhonesService phonesService = new PhonesService();
            PhonesListVM model = new PhonesListVM();
            TryUpdateModel(model);

            model.Phones = phonesService.GetAll().Where(p => p.ContactID == model.ContactID.Value).ToList();
            model.Contact = phonesService.GetContact(model.ContactID.Value);

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            PhonesService phonesService = new PhonesService();
            PhonesEditVM model = new PhonesEditVM();
            TryUpdateModel(model);

            Phone phone;
            if (!id.HasValue)
            {
                phone = new Phone();
            }
            else
            {
                phone = phonesService.GetByID(id.Value);
                if (phone == null)
                {
                    if (phonesService.GetContact(model.ContactID) == null)
                    {
                        return this.RedirectToAction<ContactsController>(c => c.List());
                    }

                    return this.RedirectToAction(c => c.List(), new { ContactID = model.ContactID });
                }
                model.ContactID = phone.ContactID;
            }

            model.ID = phone.ID;
            model.PhoneNumber = phone.PhoneNumber;
            model.PhoneType = phone.PhoneType;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            PhonesService phonesService = new PhonesService();
            PhonesEditVM model = new PhonesEditVM();
            TryUpdateModel(model);

            if (phonesService.GetContact(model.ContactID) == null)
            {
                return this.RedirectToAction<ContactsController>(c => c.List());
            }

            Phone phone;
            if (model.ID == 0)
            {
                phone = new Phone();
            }
            else
            {
                phone = phonesService.GetByID(model.ID);
                if (phone == null)
                {
                    return this.RedirectToAction(c => c.List(), new { ContactID = model.ContactID });
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            phone.ID = model.ID;
            phone.PhoneNumber = model.PhoneNumber;
            phone.PhoneType = model.PhoneType;
            phone.ContactID = model.ContactID;
            phonesService.Save(phone);

            return this.RedirectToAction(c => c.List(), new { ContactID = model.ContactID });
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return this.RedirectToAction<ContactsController>(c => c.List());
            }

            PhonesService phonesService = new PhonesService();
            int contactID = phonesService.GetContactID(id.Value);
            phonesService.Delete(id.Value);

            return this.RedirectToAction(c => c.List(), new { ContactID = contactID });
        }
    }
}