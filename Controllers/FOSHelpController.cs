using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using FOSHelp.Business;
using FOSHelp.DataAccess;
using FOSHelp.DataAccess.Repositories;
using FOSHelp.DataAccess.Repositories.Contracts;
using FOSHelp.Entities;
using System.Linq;
namespace FOSHelp.Controllers
{
    public class FOSHelpController : ApiController
    {
        
        //private FOSHelpBL BL { get; set; }
        //private ITourRepository _repository;

        private IUnitOfWork _helpUoW;

        public FOSHelpController()
        {
            _helpUoW = new FOSHelpUOW();
        }
        public FOSHelpController(IUnitOfWork uow)
        {
            _helpUoW = uow;
        }
        // GET api/foshelp
        [AcceptVerbs("GET")]
        public IEnumerable<Tour> GetToursByRoute(string route)
        {
            var items = _helpUoW.TourRepo.FindWhere(t => t.Route == route);
            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return items;
        }

        [AcceptVerbs("GET")]
        public IEnumerable<TourModule> GetAllModules()
        {
            var entities = _helpUoW.ModuleRepo.FindAll();
            return entities;
        }

        [AcceptVerbs("GET")]
        public TourModule GetModuleByName(string name)
        {
            var entity = _helpUoW.ModuleRepo.FindWhere(m => m.ModuleName == name.Trim()).FirstOrDefault();
            
            return entity;
        }

        [AcceptVerbs("POST")]
        public bool SaveModule(TourModule module)
        {
            try
            {
                _helpUoW.ModuleRepo.Add(module);
                _helpUoW.Save();            
            }
            catch (Exception)
            {
                return false;                
            }
            //var response = Request.CreateResponse(HttpStatusCode.Created, module);
            //var uri = Url.Link("DefaultApi", new { id = module.ModuleId });
            //if (uri != null) response.Headers.Location = new Uri(uri);
            return true;
        }

        [AcceptVerbs("POST")]
        public void UpdateModule(int id, TourModule moduleToUpdate)
        {
            moduleToUpdate.ModuleId = id;
            _helpUoW.ModuleRepo.Update(moduleToUpdate);            
        }
    }
}
