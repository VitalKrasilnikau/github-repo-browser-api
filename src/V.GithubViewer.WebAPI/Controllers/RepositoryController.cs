using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using V.GithubViewer.WebAPI.Model;

namespace V.GithubViewer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("*")]
    public class RepositoryController
    {
        [HttpGet]
        public async Task<IEnumerable<Repository>> GetAsync()
        {
            return Enumerable.Repeat(
                new Repository {
                    Name = "mvvmdialogservice",
                    Title = "Dialog service example",
                    Url = "https://mycompany01test01.herokuapp.com/",
                    ImgSrc = "https://images.contentful.com/v3n26e09qg2r/3jODViyHscyEeouo0a8qG0/462737f1a79fff9f220435793d6cc4e3/Get_some_Give_some-Hero-1336x600.svg",
                    Description = "<ul>This repository uses the following frameworks and components: <li><a target='_blank' href='https://angular.io/'>Angular 4</a> and TypeScript for UI coding</li><li><a target='_blank' href='https://material.angular.io/'>Material design</a> library for styling</li><li><a target='_blank' href='https://ace.c9.io/'>Ace editor</a> component for file viewer</li></ul>"     
                }, 10);
        }
    }
}