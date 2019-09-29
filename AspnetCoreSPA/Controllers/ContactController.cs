using System;
using System.Linq;
using AspnetCoreSPATemplate.Domain;
using AspnetCoreSPATemplate.Domain.Models;
using AspnetCoreSPATemplate.Helpers.Classes;
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

        [HttpGet]
        [Route("search")]
        public IActionResult GetAll(Paging data)
        {
            try
            {
                var query = _repository.Search();

                ////build orderBy 
                //var orderBy = EntitiesUtils<Contract>.GenerateOrderBy(data.OrderBy, data.OrderType);
                //if (orderBy != null)
                //{
                //    query = orderBy(query);
                //}

                //get total
                var total = query.Count();
                var totalPages = total / data.Size;
                // paging
                query = query.Skip((data.Page - 1) * data.Size).Take(data.Size);
                //return
                return Ok(new ApiResult<Contact>
                {
                    Page =
                    {
                        Size = data.Size.ToString(),
                        TotalPages = totalPages.ToString(),
                        TotalElements = total.ToString(),
                        Number = data.Page.ToString()
                    },
                    Result = query
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
