using System;
using System.Linq;
using AspnetCoreSPATemplate.Domain;
using AspnetCoreSPATemplate.Domain.Models;
using AspnetCoreSPATemplate.Helpers.Classes;
using AspnetCoreSPATemplate.UnitOfWork;
using AspnetCoreSPATemplate.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IGenericRepository<Contact> _repository;

        public ContactController(ApplicationDbContext context)
        {
            var unitOfWork = new UnitOfWork.UnitOfWork(context);
            _repository = unitOfWork.GenericRepository<Contact>(); ;
        }

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

        [HttpGet]
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
