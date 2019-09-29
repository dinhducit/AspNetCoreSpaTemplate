using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AspnetCoreSPATemplate.Domain;
using AspnetCoreSPATemplate.Domain.Models;
using AspnetCoreSPATemplate.Helpers;
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
        public IActionResult GetAll([FromQuery]Paging queryParams)
        {
            try
            {
                var query = _repository.GetAll();
                // filter
                if (!string.IsNullOrWhiteSpace(queryParams.Pattern))
                {
                    var filter = BuildFilterExpression(queryParams);
                    if (filter == null)
                    {
                        return BadRequest();
                    }
                    query = query.Where(filter);
                }
                // order by 
                if (!string.IsNullOrWhiteSpace(queryParams.Sort))
                {
                    var sortSplit = queryParams.Sort.Split(',');
                    if (sortSplit.Length == 2)
                    {
                        var orderBy = ExpressionHelpers<Contact>.GenerateOrderBy(sortSplit[0], sortSplit[1]);
                        if (orderBy != null)
                        {
                            query = orderBy(query);
                        }
                    }
                }
                // calculate total pages and total elements
                var totalElements = query.Count();
                var totalPages = (int) Math.Ceiling((double) totalElements / queryParams.Size);
                // paging
                query = query.Skip(queryParams.Page * queryParams.Size).Take(queryParams.Size);
                // result
                var result = new ApiResult<Contact>
                {
                    Page = new Page
                    {
                        Size = queryParams.Size.ToString(),
                        TotalPages = totalPages.ToString(),
                        TotalElements = totalElements.ToString(),
                        Number = queryParams.Page.ToString()
                    },
                    Result = query
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw ex;
            }
        }

        private static Expression<Func<Contact, bool>> BuildFilterExpression(Paging queryParams)
        {
            var filters = new List<Filter>();
            // search with first name
            var firstNameFilter = new Filter
            {
                PropertyName = nameof(Contact.FirstName),
                Value = queryParams.Pattern,
                RelationalOperator = Enums.RelationalOperator.TextLike,
                LogicalOperator = Enums.LogicalOperator.Or
            };
            filters.Add(firstNameFilter);
            // search with last name
            var lastNameFilter = new Filter
            {
                PropertyName = nameof(Contact.Email),
                Value = queryParams.Pattern,
                RelationalOperator = Enums.RelationalOperator.TextLike,
                LogicalOperator = Enums.LogicalOperator.Or
            };
            filters.Add(lastNameFilter);
            // search with last name
            var emailFilter = new Filter
            {
                PropertyName = nameof(Contact.LastName),
                Value = queryParams.Pattern,
                RelationalOperator = Enums.RelationalOperator.TextLike,
                LogicalOperator = Enums.LogicalOperator.Or
            };
            filters.Add(emailFilter);
            // search with last name
            var phoneFilter = new Filter
            {
                PropertyName = nameof(Contact.PhoneNumber1),
                Value = queryParams.Pattern,
                RelationalOperator = Enums.RelationalOperator.TextLike,
                LogicalOperator = Enums.LogicalOperator.Or
            };
            filters.Add(phoneFilter);
            Expression<Func<Contact, bool>> filter = ExpressionHelpers<Contact>.GenerateFilter(filters);
            return filter;
        }
    }
}
