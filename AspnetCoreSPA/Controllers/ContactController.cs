using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Domain.Models;
using AspnetCoreSPATemplate.Helpers.Classes;
using AspnetCoreSPATemplate.Repositories;
using AspnetCoreSPATemplate.Repositories.Impl;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private UnitOfWork _unitOfWork;
        private IContactRepository _repository;

        public ContactController()
        {
            _unitOfWork = new UnitOfWork();
            _repository = _unitOfWork.ContactRepository;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //[HttpPost]
        //[Route("search")]
        //public IActionResult GetAll(Paging data)
        //{
        //    try
        //    {
        //        var query = _repository.Search();

        //        //build orderBy 
        //        var orderBy = EntitiesUtils<Contract>.GenerateOrderBy(data.OrderBy, data.OrderType);
        //        if (orderBy != null)
        //        {
        //            query = orderBy(query);
        //        }

        //        //get total
        //        var total = query.Count();
        //        // paging
        //        query = query.Skip((data.CurrentPage - 1) * data.PageSize).Take(data.PageSize);
        //        //return
        //        return Ok(new ApiResult<Contact>
        //        {
        //            Count = total,
        //            List = query
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO: handle exception
        //        throw ex;
        //    }
        //}

        [HttpPost]
        [Route("search")]
        public IActionResult GetAll()
        {
            try
            {
                var query = _repository.Search();

                //get total
                var total = query.Count();
                //return
                return Ok(new ApiResult<Contact>
                {
                    Count = total,
                    List = query
                });
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw ex;
            }
        }
    }
}
