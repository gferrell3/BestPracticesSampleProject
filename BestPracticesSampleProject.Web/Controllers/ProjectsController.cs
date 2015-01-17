﻿using BestPracticesSampleProject.Web.Models;
using BestPracticesSampleProject.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BestPracticesSampleProject.Web.Controllers
{
    [RoutePrefix("api/projects")]
    public class ProjectsController : ApiController
    {
        private readonly IProjectService service;

        public ProjectsController(IProjectService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retrieves all projects in department
        /// </summary>
        /// <param name="departmentId">The id of the department to get info for</param>
        /// <returns>The matching projects.</returns>
        [Route("")]
        public Task<IEnumerable<Project>> GetByDepartmentId([FromUri]int departmentId)
        {
            return service.ListByDepartmentIdAsync(departmentId);
        }

        [Route("{id}", Name = "GetProjectById")]
        public Task<Project> GetById(int id)
        {
            return service.GetByIdAsync(id);
        }

        public async Task<HttpResponseMessage> Post([FromBody]Project value)
        {
            var result = await service.CreateAsync(value);
            var response = Request.CreateResponse(HttpStatusCode.Created, result);
            response.Headers.Location = new Uri(Url.Route("GetProjectById", new { id = result.Id }), UriKind.Relative);
            return response;
        }

        public async Task Put(int id, [FromBody]Project value)
        {
            await service.UpdateAsync(value);
        }

        public Task Delete(int id)
        {
            return service.DeleteAsync(id);
        }
    }
}
